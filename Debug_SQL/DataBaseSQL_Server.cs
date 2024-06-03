using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;           // Necessárias (as duas) para uso do SQL Server no ASP.NET Core

namespace Debug_SQL
{
    internal class DataBaseSQL_Server
    {
        // ATENÇÃO: o caracter "\" deve ser indicado com duas barras invertidas para não ser compreendido como uma instrução dentro da string
        static public string conexaoSQL = "";  // "Data Source=SERVIDOR; Initial Catalog=BANCO; Integrated Security=true";
        static public string configConexao = "debug_sql.inf";

        static public SqlCommand SQL_Server_Command = null;
        static public SqlConnection SQL_Server_Connect = null;
        static public int timeConnection = 30;
        static public string msgSQL_Server = "";
        static public int reccountSQL_Server = 0;                     // Quantidade de registros (linhas na lista - 1)
        static public int recnoSQL_Server = 0;                        // Registro corrente
        static public int colunasSQL_Server = 0;                      // Quantidade de campos na última instrução SELECT ou SHOW
        static public string colunasSelect = "";                      // Nome de cada coluna resultando do último SELECT
        static public bool eofSQL_Server = true;                      // Indica, em um loop, se o fim da lista foi atingido

        static public List<string> browseSQL = new List<string>();    // Lista com o resultado do SELECT ou SHOW


        static public string StringConexao()
        {
            string servInf = "";
            string bancoInf = "";
            string userInf = "";
            string senhaInf = "";
            string dominioInf = "";
            string senhaDecrip = "";
            string arquivoBDados = Path.Combine(FormDebug.localArquivos, configConexao);

            StreamReader leitor = null;
            Crypt crypt = new Crypt(CryptProvider.DES);

            if (File.Exists(arquivoBDados))
            {
                leitor = new StreamReader(arquivoBDados);
                servInf = leitor.ReadLine();
                userInf = leitor.ReadLine();
                senhaInf = leitor.ReadLine();
                dominioInf = leitor.ReadLine();
                leitor.Close();

                senhaDecrip = crypt.Decrypt(senhaInf);
                bancoInf = dominioInf.Trim();
                if (!bancoInf.Contains("dbsaurus_"))
                {
                    bancoInf = "dbsaurus_" + bancoInf;
                }

                conexaoSQL = "Data Source = "     + servInf.Trim()     + "; " +
                             "Initial Catalog = " + bancoInf           + "; " +
                             "User ID = "         + userInf.Trim()     + "; " +
                             "Password = "        + senhaDecrip.Trim() + "; " +
                             "Integrated Security = False; Connect Timeout = " + timeConnection.ToString() + ";";
            }

            return conexaoSQL;
        }

        // Retorna a classe de comandos ou NULL se não houver conexão com o SQL Server
        static public bool SQL_Server_Open()
        {
            return SQL_Server_Open(false);
        }

        static public bool SQL_Server_Open(bool Silencioso)
        {
            if (conexaoSQL.Length == 0)
            {
                StringConexao();
                if (conexaoSQL.Length == 0) return false;
            }

            return SQL_Server_Open(conexaoSQL, false);
        }

        static public bool SQL_Server_Open(string stringConexao, bool Silencioso)
        {
            bool retornoComando = false;
           
            try
            {
                SQL_Server_Connect = new SqlConnection(stringConexao);
                if (SQL_Server_Connect.State != ConnectionState.Open)
                {
                    SQL_Server_Connect.Open();
                }
                retornoComando = (SQL_Server_Connect.State == ConnectionState.Open);
            }
            catch (Exception ex)
            {
                msgSQL_Server = ex.Message;
#if DEBUG
                msgSQL_Server += " [" + stringConexao + "]";
#endif
            }

            if (!retornoComando && !Silencioso)
            {
                Rotinas.Mensagem(msgSQL_Server, "Erro");
            }

            return retornoComando;
        }

        static public string[,] SQL_Server_Browse()    /* Alimenta uma matriz de duas dimensões com registros e conteúdos de campos */
        {
            /*      ____________________
                    EXEMPLO DE APLICAÇÃO
                    ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯     
                    string[,] tabelaFinal = DataBaseSQL_Server.SQL_Server_Browse();

                    for (int recnoLoop = 0; recnoLoop <= DataBaseSQL_Server.SQL_Server_Reccount(); recnoLoop++)
                    {
                        for (int colunaLoop = 1; colunaLoop <= DataBaseSQL_Server.SQL_Server_Colunas(); colunaLoop++)
                        {
                            Console.WriteLine("Recno/coluna: " + recnoLoop  + 
                                              "/"              + colunaLoop + 
                                              " = "            + tabelaFinal[recnoLoop, colunaLoop]);
                        }
                    }
            */

            if (colunasSQL_Server == 0 || reccountSQL_Server == 0)
            {
                return null;
            }

            string[,] browseRetorno = new string[reccountSQL_Server + 2, colunasSQL_Server + 2];
            string[] campoSelect;
            int indiceLista = 0;

            foreach (string linhaLista in browseSQL)
            {
                campoSelect = linhaLista.Split(new char[] { '»' });

                for (int colunaLoop = 0; colunaLoop < campoSelect.Length; colunaLoop++)
                {
                    browseRetorno[indiceLista, colunaLoop + 1] = campoSelect[colunaLoop];
                }

                indiceLista++;
            }

            return browseRetorno;
        }

        static public bool SQL_Server_Comando(string comandoSQL)
        {
            return SQL_Server_Comando(comandoSQL, false);
        }

        static public bool SQL_Server_Comando(string comandoSQL, bool apontarNULL)
        {
            bool retornoComando = false;
            string testeAt = "???" + comandoSQL;
            object conteudoCampo = "";
            SqlDataReader dadosSQL = null;

            if (SQL_Server_Connect != null)
            {
                if (SQL_Server_Connect.State == ConnectionState.Open)
                {
                    try
                    {

                        SQL_Server_Command = new SqlCommand(comandoSQL, SQL_Server_Connect);

                        SQL_Server_Command.CommandTimeout = timeConnection;

                        if (testeAt.ToUpper().Contains("SELECT"))
                        {
                            browseSQL.Clear();
                            reccountSQL_Server = 0;
                            recnoSQL_Server = 0;
                            colunasSQL_Server = 0;
                            eofSQL_Server = true;

                            dadosSQL = SQL_Server_Command.ExecuteReader();
                            if (dadosSQL != null)
                            {
                                colunasSQL_Server = dadosSQL.FieldCount;

                                msgSQL_Server = "";
                                for (int colBrowse = 0; colBrowse < dadosSQL.FieldCount; colBrowse++)
                                {
                                    msgSQL_Server = msgSQL_Server + dadosSQL.GetName(colBrowse) + "»";
                                }
                                colunasSelect = msgSQL_Server;
                                browseSQL.Add(msgSQL_Server);    // O índice 0 conterá os nomes dos campos

                                while (dadosSQL.Read())
                                {
                                    msgSQL_Server = "";
                                    for (int colBrowse = 0; colBrowse < colunasSQL_Server; colBrowse++)
                                    {
                                        conteudoCampo = dadosSQL.GetValue(colBrowse);
                                        if (apontarNULL)
                                        {
                                            if(conteudoCampo == DBNull.Value)
                                            {
                                                conteudoCampo = "NULL";
                                            }
                                        }
                                        msgSQL_Server = msgSQL_Server + conteudoCampo.ToString() + "»";
                                    }
                                    browseSQL.Add(msgSQL_Server);    // Cada índice corresponde a um registro
                                    reccountSQL_Server++;
                                }

                                dadosSQL.Close();

                                if (reccountSQL_Server > 0)
                                {
                                    recnoSQL_Server = 1;
                                    eofSQL_Server = false;
                                }

                                msgSQL_Server = "Registros lidos: " + reccountSQL_Server.ToString();

                                retornoComando = true;
                            }
                        }
                        else
                        {
                            int statusInstr = SQL_Server_Command.ExecuteNonQuery();
                            retornoComando = (statusInstr != 0);
                            msgSQL_Server = "Registros afetados: " + statusInstr.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        msgSQL_Server  = ex.Message;
                        //msgSQL_Server = msgSQL_Server + " [" + comandoSQL + "]";
                        retornoComando = false;
                    }
                }
            }

            return retornoComando;
        }

        static public void SQL_Server_Skip()                       // Movimenta o índice da lista um registro para frente
        {
            SQL_Server_Skip(1);
        }
        static public void SQL_Server_Skip(int movimentoRecno)     // Movimenta o índice da lista para frente ou para trás (parâmetro negativo)
        {
            if ((recnoSQL_Server + movimentoRecno) > reccountSQL_Server || (recnoSQL_Server + movimentoRecno) <= 0)
            {
                if ((recnoSQL_Server + movimentoRecno) > reccountSQL_Server)
                {
                    eofSQL_Server = true;
                }
                return;
            }

            eofSQL_Server = false;
            recnoSQL_Server = recnoSQL_Server + movimentoRecno;
        }

        static public string SQL_Server_LeCampo(string nomeCampo)
        {
            if (reccountSQL_Server == 0 || recnoSQL_Server == 0)
            {
                return "";
            }

            int indexCampo = 0;
            string conteudoCampo = "";
            string linhaDados = browseSQL.ElementAt(0);                 // Le o cabeçalho (lista dos nomes dos campos)

            string[] campoSelect = linhaDados.Split(new char[] { '»' });
            foreach (string descricaoCampo in campoSelect)
            {
                if (descricaoCampo == nomeCampo)
                {
                    linhaDados = browseSQL.ElementAt(recnoSQL_Server);
                    campoSelect = linhaDados.Split(new char[] { '»' });
                    conteudoCampo = campoSelect[indexCampo];
                    break;
                }
                indexCampo++;
            }

            return conteudoCampo;
        }

        static public void SQL_Server_Close()
        {
            browseSQL.Clear();
            reccountSQL_Server = 0;
            recnoSQL_Server = 0;
            colunasSQL_Server = 0;
            eofSQL_Server = true;

            if (SQL_Server_Connect != null)
            {
                if (SQL_Server_Connect.State == ConnectionState.Open)
                {
                    SQL_Server_Connect.Close();
                }
            }
        }

        static public bool SQL_Server_Eof()
        {
            return eofSQL_Server;
        }

        static public int SQL_Server_Colunas()
        {
            return colunasSQL_Server;
        }

        static public int SQL_Server_Reccount()
        {
            return reccountSQL_Server;
        }

        static public int SQL_Server_Recno()
        {
            return recnoSQL_Server;
        }

        static public string SQL_Server_Mensagem()
        {
            return msgSQL_Server;
        }
    }
}
