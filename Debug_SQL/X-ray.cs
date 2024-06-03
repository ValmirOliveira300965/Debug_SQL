using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Debug_SQL
{
    internal class X_ray
    {
        public bool conectouSQL = false;
        static public int qtdeLinhas = 0;
        static public string tabelaTmp = string.Empty;
        static public string mensagemObject = string.Empty;

        static public string[] parteProcura;
        static public string[] colunasDGV = new string[300];

        public string comandoSQL;

        public string Xray_Apontamento(string linhaOriginal, string logSQL_Server)
        {
            string returnLineDebug = linhaOriginal;
            string linhaSelect = "";
            tabelaTmp = "";

            if (logSQL_Server.Length > 0)
            {
                returnLineDebug = returnLineDebug + ">>" + logSQL_Server + (char)13 + (char)10;
                return returnLineDebug;
            }

            if (DataBaseSQL_Server.SQL_Server_Open(true))
            {
                if(DataBaseSQL_Server.SQL_Server_Comando("SELECT * FROM tbX_ray ORDER BY id desc"))
                {
                    if(DataBaseSQL_Server.SQL_Server_Reccount() == qtdeLinhas)
                    {
                        DataBaseSQL_Server.SQL_Server_Close();
                        return returnLineDebug;
                    }
                    qtdeLinhas = DataBaseSQL_Server.SQL_Server_Reccount();

                    returnLineDebug = "";
                    while (!DataBaseSQL_Server.SQL_Server_Eof())
                    {
                        linhaSelect = DataBaseSQL_Server.SQL_Server_LeCampo("valor_string").Trim();

                        if (linhaSelect.Length > 0)
                        {
                            if (linhaSelect.Substring(0, 1) == "[" && linhaSelect.IndexOf("]") == (linhaSelect.Length - 1))
                            {
                                int tamDefTab = linhaSelect.Length - 2;
                                tabelaTmp     = linhaSelect.Substring(1, tamDefTab);
                                linhaSelect   = "[PREVIEW]";
                            }

                            returnLineDebug = returnLineDebug + linhaSelect;
                            if (double.Parse(DataBaseSQL_Server.SQL_Server_LeCampo("valor_numero")) != 0.0000)
                            {
                                returnLineDebug = returnLineDebug + " (" + TiraZerosNaoSignificativos(DataBaseSQL_Server.SQL_Server_LeCampo("valor_numero")) + ")";
                            }
                            returnLineDebug = returnLineDebug + (char)13 + (char)10;
                        }

                        DataBaseSQL_Server.SQL_Server_Skip();
                    }
                }
                DataBaseSQL_Server.SQL_Server_Close();
            }

            return returnLineDebug;
        }

        /// <summary>
        /// Lista com as PROCEDURES e os respectivos conteúdos puros (sem formatação RTF), separados por "»"
        /// </summary>
        /// <param name="frasesPesquisar"></param>
        /// <returns></returns>
        public List<string> Xray_Pesquisa(string frasesPesquisar) 
        {
            int matrizArray = 0;
            string compLIKE = "";
            string parteWhere = "";
            string divSelect = frasesPesquisar + "+";

            List<string> proceduresSQL = new List<string>();
            proceduresSQL.Clear();

            // Tem áspas dentro do texto a ser pesquisado, então deve ser considerada as palavras como um todo, ou seja, os espaços são relevantes
            if (divSelect.Contains((char)34))
            {
                // LÓGICA EMPREGADA: Substituir os espaços existentes entre as áspas pelo caracter "—" (ASCII = 151)
                //                   A seguir, depois de por o "+" no lugar dos espaços, voltar os "—"
                string frase1 = divSelect.Substring(0, divSelect.IndexOf((char)34));
                string frase2 = divSelect.Substring(divSelect.IndexOf((char)34) + 1, (divSelect.Length - divSelect.IndexOf((char)34)) - 1);
                string frase3 = frase2.Replace((char)34, (char)94);
                frase2 = frase2.Substring(0, frase2.IndexOf((char)34));
                frase3 = frase3.Substring(frase3.IndexOf((char)94) + 1, (frase3.Length - frase3.IndexOf((char)94)) - 1);
                frase2 = frase2.Replace(" ", "—");
                divSelect = frase1 + (frase1.Length > 0 ? " " : "") + frase2 + (frase3.Length > 0 ? " " : "") + frase3;
            }

            while (divSelect.Contains(" "))
            {
                divSelect = divSelect.Replace(" ", "+");
            }

            parteProcura = divSelect.Split(new char[] { '+' });   // Será usada no método "listBoxProcedures_Click"
            foreach (string par in parteProcura)
            {
                parteWhere = par;

                if (par.Contains("—"))
                {
                    parteProcura[matrizArray] = par.Replace("—", " ");
                    parteWhere = parteProcura[matrizArray];
                }

                if (parteWhere.Length > 0)
                {
                    if (compLIKE.Length > 0)
                    {
                        compLIKE = compLIKE + " AND ";
                    }
                    compLIKE = compLIKE + "cfg_create LIKE '%" + parteWhere.ToLower() + "%' ";
                }

                matrizArray++;
            }

            if (DataBaseSQL_Server.SQL_Server_Open(true))
            {
                comandoSQL = "SELECT distinct cfg_descprog,cfg_create from config_prog where " + compLIKE + " order by cfg_descprog";
                string linhaReg = "";

                if (DataBaseSQL_Server.SQL_Server_Comando(comandoSQL))
                {
                    while (!DataBaseSQL_Server.SQL_Server_Eof())
                    {
                        linhaReg = DataBaseSQL_Server.SQL_Server_LeCampo("cfg_create");
                        linhaReg = linhaReg.Replace((char)92, (char)47);    // Antes de tudo, tirar todas as barras invertidas
                        linhaReg = linhaReg.Replace((char)124, (char)47);   // Tirar todas os pipes

                        // <PROCEDURE> » <Conteúdo puro da procedure> »
                        proceduresSQL.Add
                            (
                                 (DataBaseSQL_Server.SQL_Server_Reccount() == 1 
                                  ? linhaReg
                                  : DataBaseSQL_Server.SQL_Server_LeCampo("cfg_descprog") + "»" + linhaReg + "»")
                             );

                        DataBaseSQL_Server.SQL_Server_Skip();
                    }
                }
                else
                {
                    proceduresSQL.Add(DataBaseSQL_Server.SQL_Server_Mensagem() + (char)13 + (char)10 + (char)13 + (char)10 + ">> " + comandoSQL);
                }

                DataBaseSQL_Server.SQL_Server_Close();
            }

            return proceduresSQL;
        }

        public string UltimoComandoSql()
        {
            return comandoSQL;
        }

        static public bool AtualizarListaObject_Definition(string proceduresSQL)
        {
            bool retornoUpdate = false;

            if (DataBaseSQL_Server.SQL_Server_Open(true))
            {
                string comandoSQL = "SELECT definition FROM sys.sql_modules WHERE object_id = OBJECT_ID('" + proceduresSQL + "')";
                string linhaReg = "";

                if (DataBaseSQL_Server.SQL_Server_Comando(comandoSQL))
                {
                    if (DataBaseSQL_Server.SQL_Server_Reccount() > 0)
                    {
                        linhaReg = DataBaseSQL_Server.SQL_Server_LeCampo("definition");

                        if (linhaReg.Length > 30)
                        {
                            comandoSQL = "UPDATE config_prog SET cfg_create = N'" + linhaReg + "' WHERE cfg_descprog='" + proceduresSQL + "'";
                            retornoUpdate = DataBaseSQL_Server.SQL_Server_Comando(comandoSQL);
                            if (!retornoUpdate)
                            {
                                mensagemObject = "Erro ao inserir dados"; // DataBaseSQL_Server.SQL_Server_Mensagem();
                            }
                        }
                        else
                        {
                            mensagemObject = "Conteúdo com suspeita de falta de integridade";
                        }
                    }
                    else
                    {
                        mensagemObject = "PROCEDURE não identificada";
                    }
                }
                else
                {
                    mensagemObject = DataBaseSQL_Server.SQL_Server_Mensagem();
                }

                DataBaseSQL_Server.SQL_Server_Close();
            }

            return retornoUpdate;
        }

        static public List<string> AtualizarListaObject_Definition()
        {
            List<string> proceduresSQL = new List<string>();
            proceduresSQL.Clear();

            if (DataBaseSQL_Server.SQL_Server_Open(true))
            {
                string comandoSQL = "SELECT distinct cfg_descprog from config_prog order by cfg_descprog DESC";
                
                if (DataBaseSQL_Server.SQL_Server_Comando(comandoSQL))
                {
                    while (!DataBaseSQL_Server.SQL_Server_Eof())
                    {
                        proceduresSQL.Add(DataBaseSQL_Server.SQL_Server_LeCampo("cfg_descprog"));
                        DataBaseSQL_Server.SQL_Server_Skip();
                    }
                }
                else
                {
                    proceduresSQL.Add(DataBaseSQL_Server.SQL_Server_Mensagem() + (char)13 + (char)10 + (char)13 + (char)10 + ">> " + comandoSQL);
                }
                DataBaseSQL_Server.SQL_Server_Close();
            }

            return proceduresSQL;
        }

        public string TiraZerosNaoSignificativos(string numeroComZerosNaoSignificativos)
        {
            string returnStrNumber = numeroComZerosNaoSignificativos.Trim();

            if(returnStrNumber.Contains("."))
            {
                returnStrNumber = returnStrNumber.Replace(".", ",");
            }
            if (returnStrNumber.Contains(","))
            {
                while (returnStrNumber.Substring(returnStrNumber.Length-1, 1)=="0")
                {
                    returnStrNumber = returnStrNumber.Substring(0, returnStrNumber.Length-1);
                }
                if (returnStrNumber.Length > 1)
                {
                    if (returnStrNumber.Substring(returnStrNumber.Length - 1, 1) == ",")
                    {
                        returnStrNumber = returnStrNumber.Substring(0, returnStrNumber.Length - 1);
                    }
                }
            }

            return returnStrNumber;
        }

        public string PovoarDataGridView(string comandoSelect, DataGridView dataGridViewSelect)
        {
            return PovoarDataGridView(comandoSelect, dataGridViewSelect, "", 0, "", false);
        }

        public string PovoarDataGridView(string comandoSelect, DataGridView dataGridViewSelect, string filtro, int tipoFiltro, string especificarBanco, bool naoVisivelZero)
        {
            if(filtro.Trim().Length > 1)
            {
                if(filtro.Trim().Substring(0,2)=="--")
                {
                    filtro = "";
                }
            }
            if (filtro.Contains("~"))
            {
                filtro = filtro + "~";
            }

            dataGridViewSelect.ClearSelection();
            while (dataGridViewSelect.Rows.Count > 1)
            {
                dataGridViewSelect.Rows.RemoveAt(0);
            }

            while (dataGridViewSelect.Columns.Count > 0)
            {
                dataGridViewSelect.Columns.RemoveAt(0);
            }
            dataGridViewSelect.DataSource = null;
            dataGridViewSelect.Rows.Clear();
            dataGridViewSelect.Refresh();

            string retornoMensagem = "";
            string comandSelect = comandoSelect.Trim();

            if(!comandoSelect.Contains(" "))
            {
                comandSelect = "SELECT * FROM " + comandSelect;
            }

            int[] tamMaxColuna = new int[300];

            bool addGrid = (tipoFiltro == 0);
            int loopColuna = 0;
            int indexLinha = 0;
            int indexColuna = -1;
            string campoTipo = "";

            for(loopColuna = 1; loopColuna < 290; loopColuna++)
            {
                tamMaxColuna[loopColuna] = 10;
            }

            bool conexaoSQL = false;
            if (especificarBanco.Length > 0)     conexaoSQL = DataBaseSQL_Server.SQL_Server_Open(especificarBanco, true);
            else                                 conexaoSQL = DataBaseSQL_Server.SQL_Server_Open(true);

            if (conexaoSQL)
            {
                bool alertarErro = (comandoSelect.TrimStart(' ').ToUpper().Substring(0, 6) == "SELECT");

                DataBaseSQL_Server.timeConnection = 1800;

                if (DataBaseSQL_Server.SQL_Server_Comando(comandSelect, alertarErro))
                {
                    if(!alertarErro)
                    {
                        retornoMensagem = DataBaseSQL_Server.SQL_Server_Mensagem();
                        DataBaseSQL_Server.SQL_Server_Close();
                        return retornoMensagem;
                    }

                    string listaColunas = DataBaseSQL_Server.colunasSelect;

                    string[] explodeCols  = listaColunas.Split(new char[] { '»' });
                    indexColuna = -1;

                    foreach (string nomeCampo in explodeCols)
                    {
                        if (nomeCampo.Length > 0 && indexColuna < 290)
                        {
                            indexColuna++;
                            colunasDGV[indexColuna] = nomeCampo;
                            if (colunasDGV[indexColuna].ToString().Trim().Length > tamMaxColuna[indexColuna])
                            {
                                tamMaxColuna[indexColuna] = colunasDGV[indexColuna].ToString().Trim().Length;
                            }

                            // Definindo as colunas da tabela
                            dataGridViewSelect.Columns.Add(colunasDGV[indexColuna], colunasDGV[indexColuna]);
                        }
                    }

                    dataGridViewSelect.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
                    dataGridViewSelect.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridViewSelect.EnableHeadersVisualStyles = false;

                    string[] linhaDados = new string[indexColuna + 1];
                    string[] checkNULL  = new string[indexColuna + 1];

                    while (!DataBaseSQL_Server.SQL_Server_Eof())
                    {
                        for (loopColuna = 0; loopColuna <= indexColuna; loopColuna++)
                        {
                            // Conteúdo de cada coluna
                            campoTipo = DataBaseSQL_Server.SQL_Server_LeCampo(colunasDGV[loopColuna]);
                            linhaDados[loopColuna] = campoTipo;

                            if (campoTipo == "NULL") checkNULL[loopColuna] = "NULL";
                            else checkNULL[loopColuna] = "Ok";

                            if (loopColuna > 0 && linhaDados[loopColuna].ToString().Trim().Length > tamMaxColuna[loopColuna])
                            {
                                tamMaxColuna[loopColuna] = linhaDados[loopColuna].ToString().Trim().Length;
                            }
                        }

                        if (tipoFiltro == 1)  // Teste do arquivo texto do Sped - apresentar somente os registros indicados?
                        {
                            addGrid = (filtro.Length == 0);

                            if (!addGrid)
                            {
                                if (filtro.Contains("~"))
                                {
                                    addGrid = false;

                                    string[] arrayFiltro = filtro.Split(new char[] { '~' });
                                    foreach (string str in arrayFiltro)
                                    {
                                        if (str.Length > 0)
                                        {
                                            addGrid = (linhaDados[1].ToString().Substring(0, str.Length) == str);
                                        }
                                        if (addGrid) break;
                                    }
                                }
                                else
                                {
                                    addGrid = (linhaDados[1].ToString().Substring(0, filtro.Length) == filtro);
                                }
                            }
                        }

                        //atribui a linha ao datagridview
                        if (addGrid)
                        {
                            dataGridViewSelect.Rows.Add(linhaDados);
                        }

                        indexLinha = DataBaseSQL_Server.SQL_Server_Recno() - 1;
                        try
                        {
                            for (loopColuna = 0; loopColuna <= indexColuna; loopColuna++)
                            {
                                if (checkNULL[loopColuna] == "NULL")
                                {
                                    dataGridViewSelect.Rows[indexLinha].Cells[loopColuna].Style.Font = new Font(dataGridViewSelect.Font, FontStyle.Italic);
                                    dataGridViewSelect.Rows[indexLinha].Cells[loopColuna].Style.ForeColor = Color.DarkSlateGray;
                                }
                            }
                        } 
                        catch (Exception err)
                        {
                            Rotinas.Mensagem(err.Message + "\n - loopColuna=" + loopColuna.ToString() + ", indexColuna=" + indexColuna.ToString());
                        }

                        DataBaseSQL_Server.SQL_Server_Skip();
                    }

                    dataGridViewSelect.BackgroundColor = Color.Beige;
                    dataGridViewSelect.RowHeadersVisible = false;   // Retira a coluna de controles (que fica a esquerda do DataGridView)

                    // Ajustar a largura de cada coluna, se necessário
                    for (loopColuna = 1; loopColuna <= indexColuna; loopColuna++)
                    {
                        if (tamMaxColuna[loopColuna] > 10)
                        {
                            dataGridViewSelect.Columns[loopColuna].Width = (tamMaxColuna[loopColuna] * 7);
                        }
                    }
                }
                else
                {
                    retornoMensagem = DataBaseSQL_Server.SQL_Server_Mensagem();
                    dataGridViewSelect.Visible = false;
                }

                DataBaseSQL_Server.SQL_Server_Close();
            }

            return retornoMensagem;
        }

    }
}
