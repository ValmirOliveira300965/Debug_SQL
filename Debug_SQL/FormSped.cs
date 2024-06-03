using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;                                   // Necessário para manipulação de arquivos

namespace Debug_SQL
{
    public partial class FormSped : Form
    {
        static public string origemModulo  = "";
        static public string textoQuadro   = "";
        static public string servidorSQL   = "prod-202401.database.saurus.net.br,14333";
        static public string senhaSQL      = "201o$@UruS202401";
        static public string comandoSQL    = "";
        static public string arquivoCmdSQL = Path.Combine(FormDebug.localArquivos, "debug_comando.sql");

        static public bool cliqueOk = true;

        public FormSped(string parLinha, string linhaSped)
        {
            InitializeComponent();

            origemModulo = linhaSped;

            if (origemModulo == "VARRER")
            {
                this.Text = parLinha;

                if (File.Exists(arquivoCmdSQL))
                {
                    string linhaTxt = "";
                    textoQuadro = "";

                    StreamReader leitor = new StreamReader(arquivoCmdSQL);
                    while ((linhaTxt = leitor.ReadLine()) != null)
                    {
                        textoQuadro = textoQuadro + linhaTxt + (char)13 + (char)10;
                    }
                    leitor.Close();

                    textBoxRegistro.Text = textoQuadro.Substring(0,textoQuadro.Length - 4);
                }

                linhaSped = "Ok!";
            }
            else
            {
                this.btInterromper.Visible = false;
                this.button1.Text = "Ok";
                this.button1.Location = new Point(button1.Location.X + 80, button1.Location.Y);
            }

            if (linhaSped.Contains("|"))
            {
                int numLinha = 0;

                string[] arrayLinhas = linhaSped.Split(new char[] { '|' });

                this.Text = "Linha " + parLinha + ", Registro " + arrayLinhas[1];
                textBoxRegistro.Text = "";

                foreach (string cadaLinha in arrayLinhas)
                {
                    if (numLinha > 0 && numLinha < (arrayLinhas.Length - 1))
                    {
                        textBoxRegistro.Text += (numLinha < 10 ? "[0" : "[") + numLinha.ToString() + "] " + cadaLinha + (char)13 + (char)10;
                    }
                    numLinha++;
                }
            }
        }

        private void frmSped_Load(object sender, EventArgs e)
        {
        }

        private void textBoxRegistro_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (origemModulo == "VARRER")
            {
                SalvarEdicao();
            }

            cliqueOk = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (origemModulo == "VARRER")
            {
                SalvarEdicao();
            }

            cliqueOk = false;
            this.Close();
        }

        private void SalvarEdicao()
        {
            textoQuadro = textBoxRegistro.Text;
            comandoSQL = textoQuadro.Trim().Replace((char)10, (char)94);    // Troca os salto de linha (10) por um "chapeuzinho"
            comandoSQL = comandoSQL.Replace((char)13, (char)32);            // Troca os salto de linha (13) por um espaço
            comandoSQL = comandoSQL.Replace((char)9, (char)32);             // Troca os TABs (9) por um espaço

            string checkComando = "";
            if      (comandoSQL.ToUpper().Contains("SELECT")) checkComando = "SELECT";
            else if (comandoSQL.ToUpper().Contains("UPDATE")) checkComando = "UPDATE";
            else if (comandoSQL.ToUpper().Contains("INSERT")) checkComando = "INSERT";
            else if (comandoSQL.ToUpper().Contains("DROP"))   checkComando = "DROP";
            else
            {
                Rotinas.Mensagem("Problema: O FormSped não conseguiu identificar o comando do SQL Server.");
            }

            if (comandoSQL.ToUpper().Contains(checkComando))
            {
                servidorSQL = comandoSQL.Substring(0, comandoSQL.ToUpper().IndexOf(checkComando));
                comandoSQL = comandoSQL.Substring(servidorSQL.Length, comandoSQL.Length - servidorSQL.Length).Trim();

                string[] arrayServ = servidorSQL.Split(new char[] { '^' });
                foreach (string linhaSelect in arrayServ)
                {
                    if (linhaSelect.Length > 2)
                    {
                        if (linhaSelect.Substring(0, 2) != "--")
                        {
                            servidorSQL = linhaSelect.Trim();
                            break;
                        }
                    }
                }

                if (servidorSQL.Contains(";"))
                {
                    senhaSQL = servidorSQL.Substring(servidorSQL.IndexOf(";") + 1, servidorSQL.Length - (servidorSQL.IndexOf(";") + 1));
                    servidorSQL = servidorSQL.Substring(0, servidorSQL.IndexOf(";")).Trim();
                }
            }

            comandoSQL = comandoSQL.Replace((char)94, (char)32);    // Troca os "chapeizinhos" por espaços
            while (comandoSQL.Contains("  "))
            {
                comandoSQL = comandoSQL.Replace("  ", " ");         // Retira os espaços excedentes
            }

            StreamWriter escritor = null;
            escritor = new StreamWriter(arquivoCmdSQL);
            escritor.WriteLine(textoQuadro.ToString());
            escritor.WriteLine();
            escritor.Close();
        }
    }
}
