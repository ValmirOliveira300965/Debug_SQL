using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug_SQL
{
    internal class CriarX_ray
    {

        static public bool CheckTabelaXray()
        {
            bool tabelaXrayExiste = true;

            if(!DataBaseSQL_Server.SQL_Server_Open())
            {
                return true;
            }

            tabelaXrayExiste = DataBaseSQL_Server.SQL_Server_Comando("SELECT id FROM tbX_ray");
            if(!tabelaXrayExiste)
            {
                if (!DataBaseSQL_Server.SQL_Server_Comando("CREATE TABLE tbX_ray (id int identity not for replication primary key, " +
                                                                                 "momento datetime not null, " +
                                                                                 "valor_string text not null default '', " +
                                                                                 "valor_numero decimal(12,4) not null)"))
                {
                    Rotinas.Mensagem(DataBaseSQL_Server.SQL_Server_Mensagem());
                    DataBaseSQL_Server.SQL_Server_Close();
                    return true;
                }
            }

            DataBaseSQL_Server.SQL_Server_Close();

            return tabelaXrayExiste;
        }
    }
}
