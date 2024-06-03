using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug_SQL
{
    internal class Acompanhamento
    {
        static public string RunAcompanhamento(string textoAcompanhamento)
        {
            if (DataBaseSQL_Server.SQL_Server_Open(true))
            {
                DataBaseSQL_Server.SQL_Server_Comando(textoAcompanhamento);
            }
            string retornoRun = DataBaseSQL_Server.SQL_Server_Mensagem();
            DataBaseSQL_Server.SQL_Server_Close();

            return retornoRun;
        }
    }
}
