using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug_SQL
{
    internal class Rotinas
    {
        static System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();

        static public bool Mensagem(string textoMensagem, bool botoesSimNao)
        {
            return (MessageBox.Show(textoMensagem, "Saurus | Debug SQL Server", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        static public void Mensagem(string textoMensagem)
        {
            Mensagem(textoMensagem, "Informação");
        }
        static public void Mensagem(string textoMensagem, string tipoAlerta)
        {
            if (tipoAlerta.Substring(0, 1).ToUpper() == "E")
            {
                MessageBox.Show(textoMensagem, "Saurus | Debug SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(textoMensagem, "Saurus | Debug SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
