using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug_SQL
{
    internal class ConverteRTF
    {
        /// <summary>
        /// Retorna o nome do arquivo RTF que será gerado depois da conversão do texto enviado por parâmetro
        /// </summary>
        /// <param name="textoRTF"></param>
        /// <returns></returns>
        static public string ApresentaPROCEDURES(string textoRTF)
        {
            string retornoTextoRTF = "";

            string aChv = "{";
            string fChv = "}";
            string barInv = "|"; barInv = barInv.Replace((char)124, (char)92);

            string textoArqTmp = aChv + barInv + "rtf1" + barInv + "ansi" + barInv + "ansicpg1252" + barInv + "deff0" +
                                 aChv + barInv + "fonttbl" + aChv + barInv + "f0" + barInv + "fnil" + barInv + "fcharset0 Courier New;" + fChv + fChv +
                                 aChv + barInv + "colortbl ;" +                                                 // Preto     -> \cf0 (padrão)
                                        barInv + "red255"     + barInv + "green0"   + barInv + "blue0; "   +    // Vermelho  -> \cf1
                                        barInv + "red0"       + barInv + "green0"   + barInv + "blue255; " +    // Azul      -> \cf2
                                        barInv + "red100"     + barInv + "green100" + barInv + "blue100; " +    // Cinza     -> \cf3
                                        barInv + "red0"       + barInv + "green128" + barInv + "blue0; "   +    // Verde     -> \cf4
                                        barInv + "red255"     + barInv + "green255" + barInv + "blue0; "   +    // Amarelo   -> \cf5
                                        barInv + "red163"     + barInv + "green73"  + barInv + "blue164; " +    // Lilaz     -> \cf6
                                 fChv +
                                 barInv + "viewkind4" + barInv + "uc1" + barInv + "pard" + barInv + "lang1046" + barInv + "f0" + barInv + "fs20" +

                                 textoRTF +

                                 barInv + "par" + fChv;
            try
            {
                StreamWriter escritor = null;
                escritor = new StreamWriter("procurar.rtf");
                escritor.WriteLine(textoArqTmp);
                escritor.WriteLine();
                escritor.Close();

                retornoTextoRTF = "procurar.rtf";
            }
            catch (Exception ex) 
            {
                retornoTextoRTF = "Erro ao converter texto em RTF: " + ex.Message.ToString();
            }

            return retornoTextoRTF;
        }


        /// <summary>
        /// OBS.: É necessário o segundo parâmetro para que haja o realce das palavras buscadas no RTF
        /// </summary>
        /// <param name="textoNormal"></param>
        /// <param name="parteProcura"></param>
        /// <returns></returns>
        static public string InsereControlesRTF(string textoNormal, string[] parteProcura, bool mesmaLinha)
        {
            string linhaReg = textoNormal;

            int tamanhoPesq = 0;
            int varreLinha = 0;
            int tamanhoLinha = 0;
            char checkLetra = ' ';
            byte codigoAscII = 0;
            string pontoAlter = "";
            string pontoCorte = "";
            string linhaAjustada = "";

            string[] reservadaSqlServerAzul =
            {
                    "PROCEDURE", "FETCH", "PUBLIC", "ALTER", "FILE", "FOR", "AS", "FROM", "BEGIN", "FULL", "FUNCTION", "GROUP", "INT", "DECIMAL", "DATETIME",
                    "RETURN", "GROUP", "BY", "CASE", "IDENTITY", "IF", "COLUMN", "INSERT", "INTO", "SET", "TABLE", "EXIT", "CREATE", "INT", "CASE", "BIT", "CURSOR",
                    "THEN", "TO", "TOP", "DATABASE", "TRANSACTION", "OF", "TRUNCATE", "DECLARE", "OFF", "DELETE", "PROC", "RETURNS", "END", "SELECT", "LOCAL",
                    "ON", "UNION", "OPEN", "DESC", "DISTINCT", "OPENXML", "DOUBLE", "OPTION", "DROP", "VALUES", "ORDER", "UNIQUEIDENTIFIER", "XML", "FAST_FORWARD",
                    "ELSE", "VIEW", "END", "OVER", "WHEN", "WHERE", "EXCEPT", "PLAN", "WHILE", "EXEC", "WITH", "EXECUTE", "NVARCHAR", "NOLOCK", "ROLLBACK",
                    "TRY", "OUTPUT", "OFF", "ON", "DECIMAL", "NOCHECK", "CONSTRAINT", "ALL", "IDENTITY_INSERT", "SCOPE_IDENTITY", "CATCH", "UPDATE", "TRAN",
                    "COMMIT", "CLOSE", "DEALLOCATE", "THROW", "VARCHAR", "ISOLATION", "LEVEL", "READ", "UNCOMMITTED", "NOCOUNT", "COALESCE", "CHAR",
                    "OBJETO", "INTEGER", "TEXT", "GUID"
            };
            string[] reservadaSqlServerCinza =
            {
                    "EXISTS", "ALL", "AND", "ANY", "BETWEEN", "RIGHT", "IN", "INNER", "IS", "JOIN", "LEFT", "LIKE", "NOT", "NULL", "OR", "REPLICATE", 
                    "ISNULL", "NULLIF"
            };
            string[] reservadaSqlServerLilaz =
            {
                    "CONTAINS", "CONVERT", "CONCAT", "SUM", "IIF", "CAST", "REPLACE", "COUNT", "GETDATE", "ERROR_NUMBER", "ERROR_MESSAGE", "CHARINDEX",
                    "TRANCOUNT", "DATEPART", "MONTH", "DAY", "YEAR", "LEN", "@@TRANCOUNT", "@@FETCH_STATUS", "@@ERROR", "@@IDENTITY", "@@VERSION",
                    "DATEADD"
            };
            string[] addReserv = { "[", "]", "(", ")", " ", ",", "^" };
           
            linhaReg = linhaReg.Replace((char)10, (char)94);    // Troca os salto de linha (10) por um "chapeuzinho"
            linhaReg = linhaReg.Replace("^", "  ºpar ");        // Troca o "chapeuzinho" pela sequencia "º" (não utilizadas no texto)
            linhaReg = linhaReg.Replace((char)186, (char)92);   // Troca o caracter "º" pela barra invertida
            linhaReg = linhaReg.Replace((char)9, (char)94);     // Troca os TABs por um "chapelzinho"
            linhaReg = linhaReg.Replace("^", "   ");            // Troca o "chapeuzinho" por três espaços
            linhaReg = " " + linhaReg + " ";

            tamanhoLinha = linhaReg.Length;
            linhaAjustada = linhaReg;

            string[,] caracteresAscII = new string[100, 2];
            int checkAscII = (-1);

            // Identifica as palavras reservadas
            foreach (string resAdd in addReserv)
            {
                // cf2 = ^cfA
                // cf3 = ^cfB
                // cf6 = ^cfC
                // cf0 = ^cfX

                // Pinta de azul as palavras reservadas do SQL Server - COMANDOS
                foreach (string reserv in reservadaSqlServerAzul)
                {
                    linhaAjustada = linhaAjustada.Replace(" " + reserv + resAdd, "^cfA  " + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace(" " + reserv.ToLower() + resAdd, "^cfA  " + reserv + "^cfX " + resAdd);

                    linhaAjustada = linhaAjustada.Replace("," + reserv + resAdd, "^cfA ," + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace("(" + reserv + resAdd, "^cfA (" + reserv + "^cfX " + resAdd);
                }

                // Pinta de cinza as palavras reservadas do SQL Server - INSTRUÇÔES
                foreach (string reserv in reservadaSqlServerCinza)
                {
                    linhaAjustada = linhaAjustada.Replace(" " + reserv + resAdd, "^cfB  " + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace(" " + reserv.ToLower() + resAdd, "^cfB  " + reserv + "^cfX " + resAdd);

                    linhaAjustada = linhaAjustada.Replace("," + reserv + resAdd, "^cfB ," + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace("(" + reserv + resAdd, "^cfB (" + reserv + "^cfX " + resAdd);
                }

                // Pinta de lilaz as palavras reservadas do SQL Server - FUNÇÕES
                foreach (string reserv in reservadaSqlServerLilaz)
                {
                    linhaAjustada = linhaAjustada.Replace(" " + reserv + resAdd, "^cfC  " + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace(" " + reserv.ToLower() + resAdd, "^cfC  " + reserv + "^cfX " + resAdd);

                    linhaAjustada = linhaAjustada.Replace("," + reserv + resAdd, "^cfC ," + reserv + "^cfX " + resAdd);
                    linhaAjustada = linhaAjustada.Replace("(" + reserv + resAdd, "^cfC (" + reserv + "^cfX " + resAdd);
                }
            }

            // Identifica as expressões declaradas (entre áspas simples)
            linhaAjustada = linhaAjustada.Replace("'",   "€");
            while (linhaAjustada.Contains("€"))
            {
                varreLinha = linhaAjustada.IndexOf("€") + 1;
                pontoAlter = "";
                pontoCorte = "";

                while (linhaAjustada.Substring(varreLinha, 1) != "€" && varreLinha < linhaAjustada.Length)
                {
                    if ("^cfA^cfB^cfC^cfX".Contains(linhaAjustada.Substring(varreLinha, 4)))   // Desfaz a coloração azul (feita pelo bloco acima)
                    {
                        pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 4);
                        varreLinha = varreLinha + 4;
                    }
                    pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 1);
                    pontoCorte = pontoCorte + linhaAjustada.Substring(varreLinha, 1);
                    varreLinha++;
                }
                if (linhaAjustada.Contains("€" + pontoAlter + "€"))
                {
                    linhaAjustada = linhaAjustada.Replace("€" + pontoAlter + "€", "^cfD '" + pontoCorte + "' ^cfX ");
                }
                else break;
            }

            // Identifica os comentários com tracinhos
            while (linhaAjustada.Contains(" --"))
            {
                varreLinha = linhaAjustada.IndexOf(" --") + 1;
                pontoAlter = "";
                pontoCorte = "";
                try
                {
                    while (linhaAjustada.Substring(varreLinha + 1, 4) != "\\par" && varreLinha < linhaAjustada.Length)
                    {
                        if("^cfA^cfB^cfC^cfD^cfX".Contains(linhaAjustada.Substring(varreLinha, 4)))   // Desfaz a coloração azul (feita pelo bloco acima)
                        {
                            pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 4);
                            varreLinha = varreLinha + 4;
                        }
                        pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 1);
                        pontoCorte = pontoCorte + linhaAjustada.Substring(varreLinha, 1);
                        varreLinha++;
                    }
                    if (pontoCorte.Length > 0)
                    {
                        linhaAjustada = linhaAjustada.Replace(pontoAlter, "^cf4" + pontoCorte + "^cf0 ");
                    }
                    else break;
                }
                catch { break; }
            }

            // Identifica os comentários com barra+asterísco
            while (linhaAjustada.Contains(" /*"))
            {
                varreLinha = linhaAjustada.IndexOf(" /*") + 1;
                pontoAlter = "";
                pontoCorte = "";
                while (linhaAjustada.Substring(varreLinha, 2) != "*/" && varreLinha < linhaAjustada.Length)
                {
                    if ("^cfA^cfB^cfC^cfD^cfX".Contains(linhaAjustada.Substring(varreLinha, 4)))   // Desfaz a coloração azul (feita pelo bloco acima)
                    {
                        pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 4);
                        varreLinha = varreLinha + 4;
                    }
                    pontoAlter = pontoAlter + linhaAjustada.Substring(varreLinha, 1);
                    pontoCorte = pontoCorte + linhaAjustada.Substring(varreLinha, 1);
                    varreLinha++;
                }
                pontoCorte = pontoCorte + "*/";
                linhaAjustada = linhaAjustada.Replace(pontoAlter, "^cf4" + pontoCorte + "^cf0 ");
            }

            // Traz as cores reais das palavras reservadas
            if (linhaAjustada.Contains("^cfA")) linhaAjustada = linhaAjustada.Replace("^cfA", "^cf2");
            if (linhaAjustada.Contains("^cfB")) linhaAjustada = linhaAjustada.Replace("^cfB", "^cf3");
            if (linhaAjustada.Contains("^cfC")) linhaAjustada = linhaAjustada.Replace("^cfC", "^cf6");
            if (linhaAjustada.Contains("^cfD")) linhaAjustada = linhaAjustada.Replace("^cfD", "^cf1");
            if (linhaAjustada.Contains("^cfX")) linhaAjustada = linhaAjustada.Replace("^cfX", "^cf0");

            // Faz a troca dos caracteres especiais pelo seu correspondente em ASCII
            // Passo 1: Criar um array com os caracteres encontrados
            varreLinha = 0;
            while (varreLinha < linhaAjustada.Length)
            {
                checkLetra = char.Parse(linhaAjustada.Substring(varreLinha, 1));
                codigoAscII = (byte)checkLetra;

                if (codigoAscII > 188 && codigoAscII < 255)
                {
                    tamanhoPesq = 0;
                    while (tamanhoPesq <= checkAscII)
                    {
                        if (caracteresAscII[tamanhoPesq, 0] == linhaAjustada.Substring(varreLinha, 1))
                        {
                            break;
                        }
                        tamanhoPesq++;
                    }
                    if (tamanhoPesq > checkAscII)
                    {
                        checkAscII = tamanhoPesq;
                        caracteresAscII[tamanhoPesq, 0] = linhaAjustada.Substring(varreLinha, 1);
                        caracteresAscII[tamanhoPesq, 1] = "^'" + Convert.ToString(codigoAscII, 16);
                    }
                }
                varreLinha++;
            }

            // Passo 2: Fazer o replace dos caracteres especiais
            tamanhoPesq = 0;
            while (tamanhoPesq <= checkAscII)
            {
                linhaAjustada = linhaAjustada.Replace(caracteresAscII[tamanhoPesq, 0], caracteresAscII[tamanhoPesq, 1]);
                tamanhoPesq++;
            }

            // Destaca a(s) palavra(s) procurada(s)
            if (!mesmaLinha)
            {
                foreach (string par in parteProcura)
                {
                    tamanhoPesq = par.Length;
                    if (tamanhoPesq > 0)
                    {
                        varreLinha = 0;
                        while (varreLinha < ((tamanhoLinha - tamanhoPesq) + 2))
                        {
                            try
                            {
                                if (linhaReg.Substring(varreLinha, tamanhoPesq).ToLower() == par.ToLower())
                                {
                                    pontoCorte = linhaReg.Substring(varreLinha, tamanhoPesq);
                                    linhaAjustada = linhaAjustada.Replace(pontoCorte, "^highlight5^fs24^b " + pontoCorte + "^b0^fs20^highlight0 ");
                                    varreLinha += tamanhoLinha;
                                }
                                varreLinha++;
                            }
                            catch { break; }
                        }
                    }
                }
            }

            return linhaAjustada.Replace((char)94, (char)92);   // Troca todos os "chapeizinhos" pela barra invertida
        }

        static public string ConverteASCII(string caracterConv)
        {
            if(caracterConv.Substring(0,1) != "Ã")
            {
                return caracterConv;
            }
            char simboloConv = char.Parse(caracterConv.Substring(1, 1));
            byte codigoAscII = (byte)simboloConv;
            int varCodigoAsc = (int)codigoAscII;
            if(varCodigoAsc < 128 || varCodigoAsc > 188)
            {
                return caracterConv;
            }
            varCodigoAsc += 64;
            char caracterAsc = (char)varCodigoAsc;
            return caracterAsc.ToString();
        }
    }
}
