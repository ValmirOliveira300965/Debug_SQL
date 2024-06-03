using System;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;                                   // Necessário para manipulação de arquivos
using System.Drawing;
using System.Linq;                                 // Específico para leitura de arquivos binários

/*
       C:\Valmir\C#\Debug_SQL\Debug_SQL\bin\Debug\Debug_SQL.exe 
       C:\Valmir\C#\Debug_SQL\Debug_SQL\bin\Debug\Debug_SQL.exe Reiniciar

       C:\Valmir\C#\Debug_SQL\Debug_SQL\bin\Debug\Debug_SQL.exe Teste1   -> Executa o comando "SELECT  * FROM ff_teste(0)"
       C:\Valmir\C#\Debug_SQL\Debug_SQL\bin\Debug\Debug_SQL.exe Teste2   -> Varre o domínio a procura de bancos com determinada filtragem

*/

namespace Debug_SQL
{
    public partial class FormDebug : Form
    {
        static public string localArquivos  = @"C:\Debug_SQL";
        static public string configScreen   = "debug_screen.inf";
        static public string configAcompanh = "debug_acomp.inf";
        public bool conexaoSQL_Server = false;
        public bool captouAcompFora = false;
        public bool captouAcompanham = false;
        public bool reiniciarXray = false;
        public string informacaoXray = "";
        public string DataTableTmp = "";
        public string logSQL_Server = "";
        static public string quadroAcompanhamento = "";

        static public string arquivoConfig = Path.Combine(localArquivos, configScreen);
        static public string arquivoAcomp  = Path.Combine(localArquivos, configAcompanh);
        static public string arquivoBDados = Path.Combine(localArquivos, DataBaseSQL_Server.configConexao);

        public List<string> proceduresSQL = new List<string>();

        // Para testes...
        static string numeroTeste = "3";
        static public List<string> dominioSaurus = new List<string>();
        static public string dominioDebug = "dbsaurus_testesvalmirol";
        static public int testeContador = (-1);
        static public string servInf = "";
        static public string senhaDecrip = "";
        static public string parteFinal = "";
        static public bool rodandoQuerys = false;

        static public string[] arqsAcompanhamento;
        static public string arquivoAcompanhamento = "";

        Crypt crypt = new Crypt(CryptProvider.DES);
        StreamWriter escritor = null;
        X_ray x_Ray = new X_ray();

        public FormDebug()
        {
            InitializeComponent();
        }

        // Evento de carregamento do formulário
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime diaDebug = DataDebug();

            if (DateTime.Today == diaDebug)
            {
                // numeroTeste = "1";

                Program.startProces = "TESTE"+ numeroTeste;
                string arquivoTesteExec = Path.Combine(localArquivos, "exec_teste"+ numeroTeste + ".ok");
                if (File.Exists(arquivoTesteExec))
                {
                    File.Delete(arquivoTesteExec);
                }
            }
            else if(Program.startProces.Length < 5)
            {
                Program.startProces = "NORMAL";
            }

            if (Program.startProces.Substring(0, 5) == "TESTE")
            {
                arquivoConfig = Path.Combine(localArquivos, "debug_" + Program.startProces + ".inf");
            }
            else
            {
                if (Process.GetProcessesByName("Debug_SQL").Length > 1)
                {
                    if (Rotinas.Mensagem("Esse aplicativo já está em execução nesse computador! Abortar essa tentativa de execução?", true))
                    {
                        this.Close();
                    }
                }
            }

            if (!Directory.Exists(localArquivos))
            {
                Directory.CreateDirectory(localArquivos);
            }

            StreamReader leitor = null;

            // Local exato onde estava a janela do aplicativo quando o processamento foi encerrado
            if (File.Exists(arquivoConfig))
            {
                leitor = new StreamReader(arquivoConfig);
                int posX = int.Parse(leitor.ReadLine());
                int posY = int.Parse(leitor.ReadLine());
                leitor.Close();

                if (posX > 0 && posY > 0)
                {
                    this.Location = new System.Drawing.Point(posX, posY);
                }
            }

            try
            {
                this.richTextBoxAcompanhamento.LoadFile("procedure.rtf");
            }
            catch { }

            if (Program.startProces.Substring(0, 5) == "TESTE")
            {
                this.tbTeste.Visible      = false;
                this.btnRunTeste.Visible  = true;
                this.panelGrid.Height     = 458;
                this.tbTeste.Top          = 404;
                this.btnRunTeste.Top      = 400;
                dataGridViewSelect.Height = 450;
                textBoxComunicacao.Height = 450;
                CheckHabilitarBotoes(false);
                btnExcluir.Visible = false;
                arqAcompanhamentos.Visible = false;
                testeEspecial(true);
                return;
            }
            else
            {
                this.btnRunTeste.Visible = false;
            }

            this.MaximizeBox = false;
            this.textBoxComunicacao.ReadOnly = true;
            this.btnConectar.Enabled = false;
            this.dataGridViewSelect.Visible = false;
            this.timerXray.Enabled = false;
            this.tbTeste.Visible = false;

            png2Nuvem.Location = new System.Drawing.Point((this.Width - png2Nuvem.Size.Width)   / 2,
                                                          (this.Height - png2Nuvem.Size.Height) / 2);

            // Configuração da conexão com SQL Server utilizada pela última vez
            if (File.Exists(arquivoBDados))
            {
                leitor = new StreamReader(arquivoBDados);
                tbServidor.Text = leitor.ReadLine();
                tbUsuario.Text  = leitor.ReadLine();
                string senhaInf = leitor.ReadLine();
                tbDominio.Text  = leitor.ReadLine();
                leitor.Close();

                if(senhaInf.Trim().Length == 0)
                {
                    tbSenha.Text = "201o$@UruS";
                    if(tbServidor.Text.Contains("prod-"))
                    {
                        tbSenha.Text = tbSenha.Text + tbServidor.Text.Substring(5, 6);
                    }
                }
                else
                {
                    tbSenha.Text = crypt.Decrypt(senhaInf);
                }

                CheckHabilitarConexao();
            }

            if (conexaoSQL_Server)
            {
                CheckHabilitarBotoes(true);
            }
            else
            {
                btnCriarDebug.Enabled = false;
                richTextBoxAcompanhamento.Text = string.Empty;
                CheckHabilitarBotoes(false);
            }

            // Última digitação no acompanhamento
            if (File.Exists(arquivoAcomp))
            {
                captouAcompFora = true;
                captouAcompanham = false;

                arquivoAcompanhamentoDrive("Ler", false);

                textBoxComunicacao.Text = quadroAcompanhamento;
            }

            // Independentemente de conectado ou não com o SQL Server, o "operador" deve estar habilitado
            arqAcompanhamentos.Enabled = true;
            btnAcompanhamento.Enabled = true;
            textBoxComunicacao.Enabled = true;
            textBoxComunicacao.Visible = true;
            btnExcluir.Visible = false;
        }

        private void tbServidor_TextChanged(object sender, EventArgs e)
        {
            CheckHabilitarConexao();
        }
        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {
            CheckHabilitarConexao();
        }
        private void tbSenha_TextChanged(object sender, EventArgs e)
        {
            CheckHabilitarConexao();
        }
        private void tbDominio_TextChanged(object sender, EventArgs e)
        {
            CheckHabilitarConexao();
        }
        private void CheckHabilitarConexao()
        {
            if (tbServidor.Text.Trim().Length > 0 &&
                tbUsuario.Text.Trim().Length  > 0 &&
                tbDominio.Text.Trim().Length  > 0)
            {
                btnConectar.Enabled = true;
            }
            else
            {
                btnConectar.Enabled = false;
            }
        }

        private void CheckHabilitarBotoes(bool habilitarBotoes)
        {
            if (habilitarBotoes)
            {
                this.richTextBoxAcompanhamento.Visible = false;
                this.textBoxComunicacao.Visible = true;

                this.tbProcurar.Enabled = true;
                this.btnProcurar.Enabled = true;
                this.chBxMesmaLinha.Enabled = true;
                this.chBxNumerarLinhas.Enabled = true;
                this.btnReiniciarDebug.Enabled = true;
                this.btnWaypoins.Enabled = true;
                this.btnAcompanhamento.Enabled = true;
            }
            else
            {
                this.richTextBoxAcompanhamento.Visible = true;
                this.textBoxComunicacao.Visible = false;

                this.tbProcurar.Enabled = false;
                this.btnProcurar.Enabled = false;
                this.chBxMesmaLinha.Enabled = false;
                this.chBxNumerarLinhas.Enabled = false;
                this.btnReiniciarDebug.Enabled = false;
                this.btnWaypoins.Enabled = false;
                this.btnAcompanhamento.Enabled = false;
                this.btnRun.Enabled = false;

                this.panelAcompanhamento.Visible = false;
                this.listBoxAcompanhamento.Visible = false;
                this.labelNomeArq.Visible = false;
                this.tbNovoArquivo.Visible = false;
                this.btnNovoArquivo.Visible = false;
            }

            listBoxProcedures.Visible = false;
        }

        private void btnCopiaSenha_Click(object sender, EventArgs e)
        {
            //string senhaInf = crypt.Encrypt(tbSenha.Text.Trim());
            string senhaInf = tbSenha.Text.Trim();

            if (senhaInf.Length == 0)
            {
                senhaInf = "201o$@UruS";
                if (tbServidor.Text.Contains("prod-"))
                {
                    senhaInf = senhaInf + tbServidor.Text.Substring(5, 6);
                }
                tbSenha.Text = senhaInf;
            }

            Clipboard.SetText(senhaInf);
            if (!conexaoSQL_Server)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            processarXray(false);

            if (tbSenha.Text.Trim().Length == 0)
            {
                tbSenha.Text = "201o$@UruS";
                if (tbServidor.Text.Contains("prod-"))
                {
                    tbSenha.Text = tbSenha.Text + tbServidor.Text.Substring(5, 6);
                }
            }

            string senhaInf = crypt.Encrypt(tbSenha.Text.Trim());

            StreamWriter escritor = null;
            escritor = new StreamWriter(arquivoBDados);
            escritor.WriteLine(tbServidor.Text.Trim());
            escritor.WriteLine(tbUsuario.Text.Trim());
            escritor.WriteLine(senhaInf);
            escritor.WriteLine(tbDominio.Text.Trim());
            escritor.WriteLine();
            escritor.Close();

            DataBaseSQL_Server.StringConexao();

            Processando("Conectando...");
            conexaoSQL_Server = DataBaseSQL_Server.SQL_Server_Open();
            Processando();

            if (conexaoSQL_Server)
            {
                DataBaseSQL_Server.SQL_Server_Close();
                CheckHabilitarBotoes(true);
                btnCriarDebug.Enabled = true;

                if (!CriarX_ray.CheckTabelaXray())
                {
                    btnCriarDebug_Click(sender, e);
                }
            }
        }

        private void Processando()
        {
            Processando("");
        }
        private void Processando(string mensagemAlerta)
        {
            if (mensagemAlerta.Length > 0)
            {
                png2Nuvem.Visible = true;
                pngNuvem.Visible = true;
                lblNuvem.Text = mensagemAlerta;
                lblNuvem.Visible = true;
                pngNuvem.Refresh();
                png2Nuvem.Refresh();
            }
            else
            {
                lblNuvem.Visible = false;
                png2Nuvem.Visible = false;
                pngNuvem.Visible = false;
            }

        }

        private void btnReiniciarDebug_Click(object sender, EventArgs e)
        {
            if (timerXray.Enabled)
            {
                processarXray(false);
            }
            else
            {
                QuadroDebug("", reiniciarXray);
                reiniciarXray = true;
                processarXray(true);
            }
        }

        private void processarXray(bool iniciarProcesso)
        {
            if (iniciarProcesso)
            {
                btnReiniciarDebug.Text = "Parar o Debug";
                timerXray.Enabled = true;
            }
            else
            {
                btnReiniciarDebug.Text = "Reiniciar Debug";
                timerXray.Enabled = false;
            }

            listBoxProcedures.Visible = false;
            arqAcompanhamentos.Enabled = false;

            panelAcompanhamento.Visible = false;
            listBoxAcompanhamento.Visible = false;

            btnExcluir.Visible = false;
        }

        private void QuadroDebug(string apresentacaoQuadro, bool deletarRegistros)
        {
            if (deletarRegistros)
            {
                if (Rotinas.Mensagem("Reiniciar a lista de dados rastreados?", true))
                {
                    if (DataBaseSQL_Server.SQL_Server_Open(true))
                    {
                        // Tem alguma tabela temporária criada?
                        if (DataTableTmp.Length > 2)
                        {
                            if (DataTableTmp.ToLower().Contains("temp"))
                            {
                                DataBaseSQL_Server.SQL_Server_Comando("DROP TABLE IF EXISTS " + DataTableTmp);
                            }
                        }

                        DataBaseSQL_Server.SQL_Server_Comando("DELETE FROM tbX_ray");

                        DataBaseSQL_Server.SQL_Server_Close();
                    }

                    if (dataGridViewSelect.Visible)
                    {
                        dataGridViewSelect.Visible = false;
                    }
                }
            }

            if (!captouAcompFora)
            {
                quadroAcompanhamento = this.textBoxComunicacao.Text;
                captouAcompFora = true;
                captouAcompanham = false;
                salvaAcompanhamento();
            }

            DataTableTmp = "";

            X_ray.qtdeLinhas = 0;
            this.richTextBoxAcompanhamento.Visible = false;
            this.textBoxComunicacao.Visible = true;
            this.textBoxComunicacao.ReadOnly = true;
            this.dataGridViewSelect.Visible = false;
            this.btnRun.Enabled = false;
            this.textBoxComunicacao.Text = apresentacaoQuadro;
            this.textBoxComunicacao.Refresh();
        }

        private void btnWayPoints_Click(object sender, EventArgs e)
        {
            processarXray(false);
            try
            {
                richTextBoxAcompanhamento.LoadFile("waypoints.rtf");
            }
            catch { }
            richTextBoxAcompanhamento.Visible = true;
            textBoxComunicacao.Visible = false;
            dataGridViewSelect.Visible = false;
            btnRun.Enabled = false;
        }

        private void btnCriarDebug_Click(object sender, EventArgs e)
        {
            processarXray(false);
            try
            {
                richTextBoxAcompanhamento.LoadFile("procedure.rtf");
            }
            catch { }
            richTextBoxAcompanhamento.Visible = true;
            textBoxComunicacao.Visible = false;
            dataGridViewSelect.Visible = false;
            btnRun.Enabled = false;
        }

        private void btnAcompanhamento_Click(object sender, EventArgs e)
        {
            processarXray(false);
            richTextBoxAcompanhamento.Visible = false;
            textBoxComunicacao.Visible = true;
            textBoxComunicacao.ReadOnly = false;
            dataGridViewSelect.Visible = false;
            btnRun.Enabled = true;

            if (!captouAcompanham)
            {
                textBoxComunicacao.Text = quadroAcompanhamento;
                captouAcompFora = false;
                captouAcompanham = true;
            }

            // No "Operador", a Grid fica menor...
            dataGridViewSelect.Top = 80;
            dataGridViewSelect.Height = 235;

            arqAcompanhamentos.Enabled = true;
        }

        private void tbProcurar_TextChanged(object sender, EventArgs e)
        {
            processarXray(false);
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            listBoxProcedures.Visible = false;
            dataGridViewSelect.Visible = false;

            string textoRTF = "";
            string arquivoRTF = "";
            string procedure1 = "";

            processarXray(false);

            if (tbProcurar.Text.Trim().Length > 0)
            {
                string[] campoSelect;

                textBoxComunicacao.Visible = false;
                richTextBoxAcompanhamento.Visible = true;

                // Traz uma lista com as PROCEDURES e os respectivos conteúdos puros (sem formatação RTF), separados por "»"
                proceduresSQL = x_Ray.Xray_Pesquisa(tbProcurar.Text);

                int qtdeArquivos = proceduresSQL.Count;

                if (chBxMesmaLinha.Checked || chBxNumerarLinhas.Checked)
                {
                    if (qtdeArquivos > 0)
                    {
                        string linhaAdd = "";
                        string fimDestaqueLinha = "";
                        int qtdeLista = proceduresSQL.Count;
                        bool linhaContem = false;
                        bool contemPalavra = false;
                        string numLinha = "";
                        int linhaProc = 0;
                        int maxLinhasProc = 0;

                        qtdeArquivos = 0;
                        List<string> novaLista = new List<string>();

                        try
                        {
                            // Cada PROCEDURE...
                            foreach (string lista in proceduresSQL)
                            {
                                linhaProc = 0;

                                if (lista.Contains('»'))
                                {
                                    campoSelect = lista.Split(new char[] { '»' });
                                    textoRTF = campoSelect[1];
                                    linhaAdd = campoSelect[0] + '»';
                                }
                                else
                                {
                                    textoRTF = lista;
                                    linhaAdd = "";
                                }
                                textoRTF = textoRTF.Replace((char)10, (char)94);    // Troca os salto de linha (10) por um "chapelzinho"

                                linhaContem = false;
                                string[] listaProcs = textoRTF.Split(new char[] { '^' });
                                maxLinhasProc = listaProcs.Length.ToString().Length;

                                // Cada linha da PROCEDURE...
                                foreach (string cadaLinha in listaProcs)
                                {
                                    contemPalavra = chBxMesmaLinha.Checked;

                                    // Cada uma das palavras procuradas...
                                    if (contemPalavra)
                                    {
                                        foreach (string par in X_ray.parteProcura)
                                        {
                                            if (par.Length > 0)
                                            {
                                                if (!cadaLinha.Contains(par))
                                                {
                                                    contemPalavra = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    fimDestaqueLinha = "";

                                    if (contemPalavra)
                                    {
                                        linhaAdd = linhaAdd + "ºhighlight5ºfs24ºb ";
                                        fimDestaqueLinha = "ºb0ºfs20ºhighlight0 ";
                                        linhaContem = true;
                                    }

                                    if (chBxNumerarLinhas.Checked)
                                    {
                                        linhaProc++;
                                        numLinha = linhaProc.ToString();
                                        numLinha = "00000000000".Substring(0, maxLinhasProc - numLinha.Length) + numLinha;
                                        linhaAdd = linhaAdd + numLinha + ": ";

                                        if (!chBxMesmaLinha.Checked)
                                        {
                                            linhaContem = true;
                                        }
                                    }

                                    linhaAdd = linhaAdd + cadaLinha + fimDestaqueLinha + " ºpar ";
                                }

                                if (linhaContem)
                                {
                                    novaLista.Add(linhaAdd);
                                    qtdeArquivos++;
                                }
                            }
                        }
                        catch (Exception erro)
                        {

                            Rotinas.Mensagem(erro.Message + " (" + erro.Data.ToString() + ")");
                        }

                        if (qtdeArquivos > 0)
                        {
                            proceduresSQL = novaLista;
                        }
                    }
                }

                if (qtdeArquivos == 0)
                {
                    x_Ray.UltimoComandoSql();
                    proceduresSQL.Add("Nao foi encontrado nenhum registro contendo a(s) palavra(s) informada(s) acima!" +
                                      (char)13 + (char)10 + (char)13 + (char)10 +
                                       x_Ray.UltimoComandoSql());
                    procedure1 = proceduresSQL[0];
                }
                else if (qtdeArquivos > 1)
                {
                    int qtdeLista = proceduresSQL.Count;
                    int arrayList = 0;
                    string[] arquivoSelect = new string[qtdeLista];

                    foreach (string lista in proceduresSQL)
                    {
                        campoSelect = lista.Split(new char[] { '»' });
                        arquivoSelect[arrayList] = campoSelect[0];
                        arrayList++;

                        if (procedure1.Length == 0)
                        {
                            procedure1 = campoSelect[1];
                        }
                    }
                    listBoxProcedures.DataSource = arquivoSelect;
                    listBoxProcedures.Visible = true;
                }
                else
                {
                    procedure1 = proceduresSQL[0];
                }

                AtivaRichTextBox(procedure1);
            }
        }


        private void tbComunicacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!conexaoSQL_Server)
            {
                Processando("Conectando...");
                conexaoSQL_Server = DataBaseSQL_Server.SQL_Server_Open(false);
                Processando();

                if (conexaoSQL_Server)
                {
                    CheckHabilitarBotoes(true);
                    btnCriarDebug.Enabled = true;
                }
            }

            if (conexaoSQL_Server)
            {
                quadroAcompanhamento = this.textBoxComunicacao.Text.Trim();

                arquivoAcompanhamentoDrive("Gravar", false);

                if (quadroAcompanhamento.Length > 3)
                {
                    string resultadoRun = "";
                    if (quadroAcompanhamento.Substring(0, 6).ToUpper() == "SELECT")
                    {
                        resultadoRun = ShowQuery(false);
                    }
                    else
                    {
                        dataGridViewSelect.Visible = false;
                        Processando("Executando...");
                        resultadoRun = Acompanhamento.RunAcompanhamento(quadroAcompanhamento.ToString());
                        Processando();
                        Rotinas.Mensagem(resultadoRun);
                    }
                }
            }
        }

        private void FormDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            escritor = new StreamWriter(arquivoConfig);
            escritor.WriteLine(this.Location.X.ToString());
            escritor.WriteLine(this.Location.Y.ToString());
            escritor.WriteLine();
            escritor.Close();

            if (Program.startProces.Substring(0, 5) == "TESTE" && Program.startProces.Length>5)
            {
                string tipoTeste = Program.startProces.Substring(5, 1);
                if (tipoTeste == " ")
                {
                    tipoTeste = Program.startProces.Substring(6, 1);
                }

                string arquivoTesteExec = Path.Combine(localArquivos, "exec_teste" + tipoTeste + ".ok");

                if (File.Exists(arquivoTesteExec))
                {
                    File.Delete(arquivoTesteExec);
                }
            }
            else
            {
                salvaAcompanhamento();
            }
        }

        private void salvaAcompanhamento()
        {
            if (!captouAcompFora)
            {
                quadroAcompanhamento = this.textBoxComunicacao.Text;
            }

            arquivoAcompanhamentoDrive("Gravar", false);
        }

        private void timerXray_Tick(object sender, EventArgs e)
        {
            if (textBoxComunicacao.Visible)
            {
                logSQL_Server = "";
                bool refreshTextBox = false;

                // Tem alguma tabela para ser apresentada?
                if (X_ray.tabelaTmp.Length > 2)
                {
                    refreshTextBox = true;
                    DataTableTmp = X_ray.tabelaTmp;

                    dataGridViewSelect.Top = 27;
                    dataGridViewSelect.Height = 285;

                    X_ray x_ray = new X_ray();
                    logSQL_Server = x_ray.PovoarDataGridView(DataTableTmp, dataGridViewSelect);

                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    this.dataGridViewSelect.Refresh();
                }

                informacaoXray = x_Ray.Xray_Apontamento(this.textBoxComunicacao.Text, logSQL_Server);

                if (!refreshTextBox)
                {
                    refreshTextBox = (this.textBoxComunicacao.Text != informacaoXray);
                }

                if (!refreshTextBox)
                {
                    btnReiniciarDebug.Image = Debug_SQL.Properties.Resources.download_32;
                }
                else
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    btnReiniciarDebug.Image = Debug_SQL.Properties.Resources.download_ok_32;
                    this.textBoxComunicacao.Text = informacaoXray;
                }
            }
        }

        private void listBoxProcedures_Click(object sender, EventArgs e)
        {
            int arrayList = listBoxProcedures.SelectedIndex;
            string[] campoSelect;
            string[] arquivoSelect = new string[2];

            campoSelect = proceduresSQL[arrayList].Split(new char[] { '»' });

            AtivaRichTextBox(campoSelect[1]);
        }

        private void AtivaRichTextBox(string textoPuro)
        {
            Processando("Carregando...");
            string textoRTF = ConverteRTF.InsereControlesRTF(textoPuro, X_ray.parteProcura, chBxMesmaLinha.Checked);
            string arquivoRTF = ConverteRTF.ApresentaPROCEDURES(textoRTF);
            Processando();

            if (arquivoRTF.Substring(0, 4) == "Erro")
            {
                richTextBoxAcompanhamento.Text = arquivoRTF;
            }
            else
            {
                richTextBoxAcompanhamento.LoadFile(arquivoRTF);
            }
        }

        private void btnAcessoV4_Click(object sender, EventArgs e)
        {
            processarXray(false);

            //var xData = new Date();
            //var xSenha = "@saurus" + (xData.getDate() + xData.getDate() + xData.getHours()).toString() + "2024";

            var xSenha = "@saurus" + (DateTime.Now.Day + DateTime.Now.Day + DateTime.Now.Hour).ToString() + "2024";

            Clipboard.SetText(xSenha);
            if (!conexaoSQL_Server)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void arqAcompanhamentos_Click(object sender, EventArgs e)
        {
            if (this.textBoxComunicacao.Text.Trim().Length > 0)
            {
                quadroAcompanhamento = this.textBoxComunicacao.Text.Trim();
                arquivoAcompanhamentoDrive("Gravar", false);
            }

            PovoarListBoxAcompanhamento();
            this.panelAcompanhamento.Visible = true;
            this.listBoxAcompanhamento.Visible = true;
            this.labelNomeArq.Visible = false;
            this.tbNovoArquivo.Visible = false;
            this.btnNovoArquivo.Visible = false;
            this.listBoxAcompanhamento.Focus();

        }

        private void PovoarListBoxAcompanhamento()
        {
            dataGridViewSelect.Visible = false;

            if (!File.Exists("(Novo).apt"))
            {
                StreamWriter escritor = null;
                escritor = new StreamWriter("(Novo).apt");
                escritor.WriteLine("Novo arquivo de acompanhamento...");
                escritor.Close();
            }

            int arrayArq = 0;
            arqsAcompanhamento = Directory.GetFiles(".", "*.apt");

            foreach (string lista in arqsAcompanhamento)
            {
                arqsAcompanhamento[arrayArq] = arqsAcompanhamento[arrayArq].Replace(".\\", "");
                arqsAcompanhamento[arrayArq] = arqsAcompanhamento[arrayArq].Replace(".apt", "");
                arrayArq++;
            }

            listBoxAcompanhamento.DataSource = arqsAcompanhamento;
        }

        private void listBoxAcompanhamento_Click(object sender, EventArgs e)
        {
            int arrayArq = listBoxAcompanhamento.SelectedIndex;
            string arquivoSelecionado = arqsAcompanhamento[arrayArq];

            if (arquivoSelecionado.Contains("(Novo)"))
            {
                if (textBoxComunicacao.Text.Trim().Length > 10)
                {
                    this.tbNovoArquivo.Text = String.Empty;
                    this.labelNomeArq.Visible = true;
                    this.tbNovoArquivo.Visible = true;
                    this.btnNovoArquivo.Visible = true;
                    this.tbNovoArquivo.Focus();
                }
                this.btnExcluir.Visible = false;
            }
            else
            {
                arquivoAcompanhamento = arquivoSelecionado;
                arquivoAcompanhamentoDrive("Ler", true);

                char ultimoCaracter = char.Parse(quadroAcompanhamento.Substring(quadroAcompanhamento.Length - 1, 1));

                // Tira os saltos de linhas em excesso do final do conteúdo
                while (quadroAcompanhamento.Length > 1 && (byte)ultimoCaracter < 30)
                {
                    quadroAcompanhamento = quadroAcompanhamento.Substring(0, quadroAcompanhamento.Length - 1);
                    ultimoCaracter = char.Parse(quadroAcompanhamento.Substring(quadroAcompanhamento.Length - 1, 1));
                }
                quadroAcompanhamento = quadroAcompanhamento + (char)13 + (char)10;

                this.textBoxComunicacao.Text = quadroAcompanhamento;
                this.textBoxComunicacao.Refresh();

                this.panelAcompanhamento.Visible = false;
                this.listBoxAcompanhamento.Visible = false;
                this.textBoxComunicacao.Focus();

                this.btnAcompanhamento.PerformClick();
                this.btnExcluir.Visible = true;
            }
        }

        public void arquivoAcompanhamentoDrive(string acaoArquivo, bool listBoxAcomp)
        {
            string nomeApt = string.Empty;

            if (acaoArquivo == "Ler")
            {
                StreamReader leitor = null;

                // Nome do arquivo
                if (listBoxAcomp)
                {
                    escritor = new StreamWriter(arquivoAcomp);
                    escritor.WriteLine(arquivoAcompanhamento);
                    escritor.WriteLine();
                    escritor.Close();
                }
                else
                {
                    leitor = new StreamReader(arquivoAcomp);
                    arquivoAcompanhamento = leitor.ReadLine().ToString();
                    leitor.Close();
                }

                // Conteúdo do arquivo
                if (arquivoAcompanhamento.Length > 0)
                {
                    lblArquivoAberto.Text = arquivoAcompanhamento;

                    nomeApt = arquivoAcompanhamento;
                    if (!nomeApt.ToLower().Contains(".apt"))
                    {
                        nomeApt = nomeApt + ".apt";
                    }

                    if (File.Exists(nomeApt))
                    {
                        leitor = new StreamReader(nomeApt);
                        quadroAcompanhamento = leitor.ReadToEnd();
                        leitor.Close();

                        lblArquivoAberto.Visible = true;
                    }
                }
            }
            else
            {
                if (arquivoAcompanhamento.Length == 0)
                {
                    arquivoAcompanhamento = "Anotacoes.apt";
                }

                nomeApt = arquivoAcompanhamento;
                if (!nomeApt.ToLower().Contains(".apt"))
                {
                    nomeApt = nomeApt + ".apt";
                }

                escritor = new StreamWriter(arquivoAcomp);
                escritor.WriteLine(arquivoAcompanhamento);
                escritor.WriteLine();
                escritor.Close();

                escritor = new StreamWriter(nomeApt);
                escritor.WriteLine(quadroAcompanhamento);
                escritor.WriteLine();
                escritor.Close();
            }

        }

        private void btnNovoArquivo_Click(object sender, EventArgs e)
        {
            if (tbNovoArquivo.Text.Trim().Length > 1)
            {
                string nomeApt = tbNovoArquivo.Text.Trim();
                if (!nomeApt.ToLower().Contains(".apt"))
                {
                    nomeApt = nomeApt + ".apt";
                }
                bool gravarNovoArquivo = true;
                if (File.Exists(nomeApt))
                {
                    gravarNovoArquivo = Rotinas.Mensagem("O arquivo " + (char)34 + nomeApt + (char)34 + " já existe! Sobrescrevê-lo?", true);
                }
                if (gravarNovoArquivo)
                {
                    arquivoAcompanhamento = tbNovoArquivo.Text.Trim();
                    quadroAcompanhamento = textBoxComunicacao.Text.Trim();
                    arquivoAcompanhamentoDrive("Gravar", true);
                }
            }

            this.labelNomeArq.Visible = false;
            this.tbNovoArquivo.Visible = false;
            this.btnNovoArquivo.Visible = false;
            this.panelAcompanhamento.Visible = false;
            this.listBoxAcompanhamento.Visible = false;
            this.textBoxComunicacao.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Rotinas.Mensagem("Deseja realmente excluir o arquivo " + (char)34 + arquivoAcompanhamento + (char)34 + "?", true))
            {
                if (arquivoAcompanhamento.Length > 0)
                {
                    string nomeApt = arquivoAcompanhamento;
                    if (!nomeApt.ToLower().Contains(".apt"))
                    {
                        nomeApt = nomeApt + ".apt";
                    }

                    if (File.Exists(nomeApt))
                    {
                        File.Delete(nomeApt);

                        lblArquivoAberto.Visible = false;
                    }
                }

                textBoxComunicacao.Text = String.Empty;
                textBoxComunicacao.Refresh();
                dataGridViewSelect.Visible = false;
                btnExcluir.Visible = false;
            }
        }


        private string ShowQuery(bool ampliarGrid)
        {
            return ShowQuery(ampliarGrid, "", 0, false, "");
        }

        private string ShowQuery(bool ampliarGrid, string filtro, int tipoFiltro, bool naoVisivelZero, string stringConexao)
        {
            string comandoSQL = quadroAcompanhamento.ToString();
            bool alertarErro  = (comandoSQL.TrimStart(' ').ToUpper().Substring(0, 6) == "SELECT");
            int linhasCmd = 0;

            Processando("Executando...");
            X_ray x_ray = new X_ray();
            string resultadoRun = x_ray.PovoarDataGridView(comandoSQL, dataGridViewSelect, filtro, tipoFiltro, stringConexao, naoVisivelZero);
            Processando();
            if (resultadoRun.Length > 0 && alertarErro)  // Deu erro?
            {
                resultadoRun = "Erro: " + resultadoRun;
                dataGridViewSelect.Visible = false;
                Rotinas.Mensagem(resultadoRun);
            }
            else
            {
                linhasCmd = 0;
                for (int posChar = 1; posChar < quadroAcompanhamento.Length; posChar++)
                {
                    char ultimoCaracter = char.Parse(quadroAcompanhamento.Substring(posChar, 1));

                    if ((byte)ultimoCaracter < 30)
                    {
                        linhasCmd++;
                    }
                }

                if (ampliarGrid)
                {
                    if (!naoVisivelZero || dataGridViewSelect.Rows.Count > 0)
                    {
                        TesteLimparTela(true);
                    }
                }
                else 
                { 
                    if (linhasCmd > 1) linhasCmd = (linhasCmd / 2) + 1;
                    if (linhasCmd == 0) linhasCmd = 1;

                    dataGridViewSelect.Top = 27 + ((linhasCmd - 1) * 15);
                    dataGridViewSelect.Height = 285 - ((linhasCmd - 1) * 15);
                }

                // Se tiver só uma linha e poucas colunas, não exibir o DataGridShow e retornar o resultado dessa(s) célula(s)
                if (dataGridViewSelect.Rows.Count == 1)
                {
                    if (dataGridViewSelect.Rows[0].Cells.Count > 0 && dataGridViewSelect.Rows[0].Cells.Count < 6)
                    {
                        resultadoRun = "";
                        for (linhasCmd = 0; linhasCmd < dataGridViewSelect.Rows[0].Cells.Count; linhasCmd++)
                        {
                            if (resultadoRun.Length > 0) resultadoRun += ", ";
                            resultadoRun = resultadoRun + X_ray.colunasDGV[linhasCmd].ToString()+": " + dataGridViewSelect.Rows[0].Cells[linhasCmd].Value.ToString();
                        }
                        resultadoRun = "Resultado = " + resultadoRun;
                        return resultadoRun;
                    }
                }

                if (comandoSQL.ToUpper().IndexOf("COUNT(*)") > 0)
                {
                    resultadoRun = "Resultado = NENHUM";
                    return resultadoRun;
                }

                // Se o DataGridView estiver vazio e o quarto parâmetro for TRUE, então não exibir o DataGridView
                if (!naoVisivelZero || dataGridViewSelect.Rows.Count > 0)
                {
                    dataGridViewSelect.Visible = true;
                    dataGridViewSelect.Refresh();
                }
            }

            return resultadoRun;
        }


        /* ----------------------------------------[ ÁREA DE TESTES DIVERSOS ]---------------------------------------- */

        private void testeEspecial(bool startTeste)
        {
            if (Program.startProces.Length==5)
            {
                Rotinas.Mensagem("O aplicativo para testes não contém o parâmetro adequado - O sexto dígito deve ser um número.");
                this.Close();
            }

            string tipoTeste = Program.startProces.Substring(5, 1);
            if (tipoTeste == " ")
            {
                tipoTeste = Program.startProces.Substring(6, 1);
            }

            string arquivoTesteExec = Path.Combine(localArquivos, "exec_teste"+ tipoTeste+".ok");

            if (File.Exists(arquivoTesteExec))
            {
                if (Rotinas.Mensagem("O aplicativo para testes " + (char)34 + tipoTeste + (char)34 + " já está em funcionamento. Abortar essa tentativa de execução?", true))
                {
                    this.Close();
                }
            }

            escritor = new StreamWriter(arquivoTesteExec);
            escritor.WriteLine("Aplicativo para testes (" + tipoTeste + ")");
            escritor.WriteLine();
            escritor.Close();

            if (startTeste)
            {
                Rotinas.Mensagem("Aplicativo para testes (" + tipoTeste + ")");
            }

            switch (tipoTeste)
            {
                // Gera um arquivo do Sped executando [dbo].[ff_teste]
                case "1":
                    servInf      = "Servidor";
                    senhaDecrip  = "Senha";
                    dominioDebug = "dbsaurus_testessped";

                    // Configuração da conexão com SQL Server utilizada pela última vez
                    if (File.Exists(arquivoBDados))
                    {
                        StreamReader leitor = new StreamReader(arquivoBDados);
                        servInf         = leitor.ReadLine();
                        tbUsuario.Text  = leitor.ReadLine();
                        string senhaInf = leitor.ReadLine();
                        dominioDebug    = leitor.ReadLine();
                        leitor.Close();

                        if (senhaInf.Trim().Length == 0)
                        {
                            senhaDecrip = "201o$@UruS";
                            if (servInf.Contains("prod-"))
                            {
                                senhaDecrip = senhaDecrip + servInf.Substring(5, 6);
                            }
                        }
                        else
                        {
                            senhaDecrip = crypt.Decrypt(senhaInf);
                        }

                        if(!dominioDebug.Contains("dbsaurus_"))
                        {
                            dominioDebug = "dbsaurus_" + dominioDebug;
                        }
                    }

                    if (!TesteSped())
                    {
                        Rotinas.Mensagem("O aplicativo para testes será encerrado!");
                        if (File.Exists(arquivoTesteExec))
                        {
                            File.Delete(arquivoTesteExec);
                        }
                        this.Close();
                    }
                break;

                // "Varre" todo do servidor na busca de registros que atendam os critérios do SELECT especificado
                case "2":
                    parteFinal = "";
                    testeContador = (-1);
                    TesteLimparTela(false);
                break;

                // "Habilita o botão de "Testes" para testar linhas de códigos diversos (uma função, por exemplo)
                case "3":
                    this.tbTeste.Visible = true;
                    TesteLimparTela(false);
                    break;
            }
        }

        private bool TesteSped()
        {
            this.tbTeste.Visible = true;

            DataBaseSQL_Server.timeConnection = 1800;

            string stringConexao = "Data Source = " + servInf.Trim() + "; " +
                                   "Initial Catalog = " + dominioDebug + "; " +
                                   "User ID = admSaurus; " +
                                   "Password = " + senhaDecrip.Trim() + "; " +
                                   "Integrated Security = False; Connect Timeout = 1800;";

            bool testeOk = DataBaseSQL_Server.SQL_Server_Open(stringConexao, false);

            if (testeOk)
            {
                TesteLimparTela(true);

                bool criticaSped = false;

                if (criticaSped)
                {
                    quadroAcompanhamento = "SELECT * FROM ff_retSpedCriticas('2024-03-01 00:00:00', '2024-03-31 23:59:50', 1, 1, NULL)";
                }
                else
                {
                    quadroAcompanhamento = "SELECT * FROM ff_teste(0) ORDER BY nroLinha";

                }
                string filtroTeste = tbTeste.Text.Trim();
                if(filtroTeste.Length > 1)
                {
                    if(filtroTeste.Substring(0, 2)=="--")
                    {
                        filtroTeste = "";
                    }
                }

                string resultadoRun = ShowQuery(true, filtroTeste, 1, false, stringConexao);
                DataBaseSQL_Server.SQL_Server_Close();

                if (filtroTeste.Length == 0 && !criticaSped)
                {
                    try
                    {
                        escritor = new StreamWriter("C:\\Users\\SAURUS\\OneDrive - Saurus Software (ISV)\\Documentos\\EFD_ICMS_IPI.txt");
                        //escritor = new StreamWriter("\\\\valmir\\Users\\Valmir\\Documents\\Sped.txt");
                        foreach (DataGridViewRow linhaGrid in dataGridViewSelect.Rows)
                        {
                            escritor.WriteLine(linhaGrid.Cells[1].Value.ToString());
                        }
                        escritor.Close();

                        //File.Copy("\\\\valmir\\Users\\Valmir\\Documents\\Sped.txt", "C:\\Users\\SAURUS\\OneDrive - Saurus Software (ISV)\\Documentos\\Sped_copia.txt", true);
                    }
                    catch (Exception ex)
                    {
                        Rotinas.Mensagem("Problemas no acesso ao Notebook: "+ex.Message, "Erro");
                    }
                }
            }

            return testeOk;
        }

        private void TesteVarrerBancoDados()
        {
            textBoxComunicacao.Visible = true;
            dataGridViewSelect.Visible = false;

            string arquivoTesteExec = Path.Combine(localArquivos, "exec_teste2.ok");
            string stringConexao = "";

            if (testeContador < 0)
            {
                textBoxComunicacao.Text = "< Servidor >;< Senha >" + (char)13 + (char)10 + (char)13 + (char)10 +
                                          "SELECT < Instrução SELECT >" + (char)13 + (char)10 + (char)13 + (char)10 +
                                          "[ DOMINIO: < Domínio > ]" + (char)13 + (char)10;

                FormSped formSped = new FormSped("Servidor / Instrução SQL", "VARRER");
                formSped.ShowDialog();

                if (FormSped.servidorSQL.ToUpper().Substring(0, 4) == "STOP" || !FormSped.cliqueOk)
                {
                    return;
                }

                dominioSaurus.Clear();

                textBoxComunicacao.Text = "Conectando com " + (char)34 + FormSped.servidorSQL + (char)34 + "..." + (char)13 + (char)10;

                stringConexao = "Data Source = " + FormSped.servidorSQL + "; " +
                                "Initial Catalog = master; " +
                                "User ID = admSaurus; " +
                                "Password = " + FormSped.senhaSQL + "; " +
                                "Integrated Security = False; Connect Timeout = 30;";

                Processando(FormSped.servidorSQL);
                if (DataBaseSQL_Server.SQL_Server_Open(stringConexao, false))
                {
                    DataBaseSQL_Server.SQL_Server_Comando("SELECT name, database_id, create_date " +
                                                          "FROM sys.databases WHERE LEFT(name,9)='dbsaurus_' " +
                                                          "ORDER BY name");
                    while (!DataBaseSQL_Server.SQL_Server_Eof())
                    {
                        stringConexao = DataBaseSQL_Server.SQL_Server_LeCampo("name");
                        dominioSaurus.Add(stringConexao);
                        DataBaseSQL_Server.SQL_Server_Skip();
                    }
                }
                else
                {
                    Processando();
                    if (File.Exists(arquivoTesteExec))
                    {
                        File.Delete(arquivoTesteExec);
                    }
                    this.Close();
                }
                DataBaseSQL_Server.SQL_Server_Close();
                Processando();
            }

            int qtdeBancos       = dominioSaurus.Count;
            string filtroDominio = "";

            // Se tiver algo escrito em "comentários", considerar apenas o texto que antecede o símbolo "/*"
            if (FormSped.comandoSQL.Contains("/*"))
            {
                FormSped.comandoSQL = FormSped.comandoSQL.Substring(0, FormSped.comandoSQL.IndexOf("/*"));
            }

            // Especificar um banco de dados?
            if (FormSped.comandoSQL.Contains("DOMINIO:") && qtdeBancos > 0)
            {
                if (FormSped.comandoSQL.Contains("--DOMINIO:"))
                {
                    FormSped.comandoSQL = FormSped.comandoSQL.Replace("--", "");
                }
                else
                { 
                    int tamanhoTexto = FormSped.comandoSQL.Length;
                    int posLgDominio = FormSped.comandoSQL.IndexOf("DOMINIO:");

                    filtroDominio    = FormSped.comandoSQL.Substring(posLgDominio + 8, (tamanhoTexto - (posLgDominio + 8))).Trim();

                    if (filtroDominio.Substring(0, 1) == ">")
                    {
                        filtroDominio = filtroDominio.Substring(1, filtroDominio.Length - 1);

                        if (!filtroDominio.Contains("dbsaurus_"))
                        {
                            filtroDominio = "dbsaurus_" + filtroDominio;
                        }
                    }
                    else
                    {
                        if (!filtroDominio.Contains("dbsaurus_"))
                        {
                            filtroDominio = "dbsaurus_" + filtroDominio;
                        }

                        dominioSaurus[0] = filtroDominio;
                        filtroDominio = "";
                        qtdeBancos = 1;
                    }
                }
                FormSped.comandoSQL = FormSped.comandoSQL.Substring(0, FormSped.comandoSQL.IndexOf("DOMINIO:")).Trim();
            }

            string parteInicial = "Servidor: " + FormSped.servidorSQL;
            string porcentagem  = " " + (char)13 + (char)10 + (char)13 + (char)10;
            bool temRegistros   = false;
            bool exibeLog       = true;
            bool exibeLinha     = (FormSped.comandoSQL.ToUpper().IndexOf("COUNT(*)") == 0);
            int  evolucaoPerc   = 0;

            do
            {
                rodandoQuerys = true;
                exibeLog      = true;

                testeContador++;
                if(testeContador >= qtdeBancos)
                {
                    porcentagem = " " + (char)13 + (char)10 + (char)13 + (char)10;

                    textBoxComunicacao.Text = parteInicial + porcentagem +
                                              "ACABOU!!!" + (char)13 + (char)10 +
                                              parteFinal + (char)13 + (char)10;
                    break;
                }

                if(((testeContador * 100) / qtdeBancos) > evolucaoPerc)
                {
                    evolucaoPerc = ((testeContador * 100) / qtdeBancos) ;
                    porcentagem  = "  (" + evolucaoPerc.ToString() + "%)" + (char)13 + (char)10 + (char)13 + (char)10;
                }

                if (!File.Exists(arquivoTesteExec))
                {
                    this.Close();
                }

                textBoxComunicacao.Text = parteInicial + porcentagem +
                                            (exibeLinha ? dominioSaurus[testeContador] : "") + (char)13 + (char)10 +
                                            parteFinal + (char)13 + (char)10;
                textBoxComunicacao.Refresh();

                if (filtroDominio.Length > 0)
                {
                    if(dominioSaurus[testeContador] == filtroDominio)
                    {
                        filtroDominio = "";
                    }
                }
                else
                {
                    temRegistros = ExibeTeste(FormSped.comandoSQL);

                    if (FormSped.comandoSQL.TrimStart(' ').ToUpper().Substring(0, 6) != "SELECT")
                    {
                        if  (quadroAcompanhamento.Contains("-1") || quadroAcompanhamento.Contains(": 0"))
                             quadroAcompanhamento = "";
                        else quadroAcompanhamento = " (" + quadroAcompanhamento + ")";

                        parteFinal = dominioSaurus[testeContador] + quadroAcompanhamento + (char)13 + (char)10 + parteFinal;
                        exibeLog   = false;
                    }

                    if(quadroAcompanhamento.Substring(0, 12) == "Resultado = ")
                    {
                        if (!quadroAcompanhamento.Contains("NENHUM"))
                        {
                            parteFinal = dominioSaurus[testeContador] + ": " + quadroAcompanhamento + (char)13 + (char)10 + parteFinal;
                        }
                        if(dataGridViewSelect.Visible)
                        {
                            dataGridViewSelect.Visible = false;
                            textBoxComunicacao.Visible = true;
                            textBoxComunicacao.Refresh();
                        }
                        temRegistros = false;
                        exibeLog     = false;
                    }
                }

                if (exibeLog)
                {
                    parteFinal = dominioSaurus[testeContador] + (char)13 + (char)10 + parteFinal;
                }

            } while (!temRegistros);

            rodandoQuerys = false;

            if (testeContador < qtdeBancos)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Text = "Servidor: " + FormSped.servidorSQL + "   Domínio: " + dominioSaurus[testeContador] + " (" + dataGridViewSelect.Rows.Count.ToString() + ")";
                textBoxComunicacao.Visible = false;
                dataGridViewSelect.Visible = true;
            }
            else
            {
                parteFinal = "";
                testeContador = -1;
            }
        }

        private bool ExibeTeste(string comandoSelect)
        {
            string stringConexao = "Data Source = " + FormSped.servidorSQL + "; " +
                                   "Initial Catalog = " + dominioSaurus[testeContador] + "; " +
                                   "User ID = admSaurus; " +
                                   "Password = " + FormSped.senhaSQL + "; " +
                                   "Integrated Security = False; Connect Timeout = 30;";

            quadroAcompanhamento = comandoSelect;

            string resultadoRun = ShowQuery(true, "", 0, true, stringConexao);

            if (resultadoRun.Length > 7)
            {
                if (FormSped.comandoSQL.TrimStart(' ').ToUpper().Substring(0, 6) == "SELECT")
                {
                    if (resultadoRun.Substring(0, 5) == "Erro:")
                    {
                        testeContador = 999999;
                        return (true);
                    }
                }
                quadroAcompanhamento = resultadoRun;
            }

            return (dataGridViewSelect.Rows.Count > 0);
        }

        private void btnRunTeste_Click(object sender, EventArgs e)
        {
            string tipoTeste = Program.startProces.Substring(5, 1);
            if (tipoTeste == " ")
            {
                tipoTeste = Program.startProces.Substring(6, 1);
            }

            switch (tipoTeste)
            {
                case "1":
                    TesteSped();
                    break;

                case "2":
                    if (rodandoQuerys)
                    {
                        rodandoQuerys = false;
                        testeContador = 999999999;
                    }
                    else
                    {
                        TesteVarrerBancoDados();
                    }
                    break;

                case "3":
                    TestesDiversos();
                    break;
            }
        }

        private void TesteLimparTela(bool incluirDataGridView)
        {
            this.richTextBoxAcompanhamento.Visible = false;
            this.tbProcurar.Visible = false;
            this.btnProcurar.Visible = false;
            this.chBxMesmaLinha.Visible = false;
            this.chBxNumerarLinhas.Visible = false;
            this.btnReiniciarDebug.Visible = false;
            this.btnWaypoins.Visible = false;
            this.btnAcompanhamento.Visible = false;
            this.btnCriarDebug.Visible = false;
            this.btnRun.Visible = false;
            this.btnAcessoV4.Visible = false;
            this.btnCopiaSenha.Visible = false;
            this.logoSaurus.Visible = false;
            this.arqAcompanhamentos.Visible = false;
            this.panelGrid.Top = 0;

            if (incluirDataGridView)
            {
                arqAcompanhamentos.Visible = false;
                dataGridViewSelect.Visible = true;
                dataGridViewSelect.Top = 0;
            }
            else
            {
                textBoxComunicacao.Text = "";
                textBoxComunicacao.Visible = true;
                dataGridViewSelect.Visible = false;
                textBoxComunicacao.Top = 0;
            }

            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }

        private void dataGridViewSelect_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object cellValue = dataGridViewSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (cellValue.ToString().Contains("|"))
                {
                    object cellValue0 = dataGridViewSelect.Rows[e.RowIndex].Cells[0].Value;
                    this.TopMost = false;

                    FormSped formSped = new FormSped(cellValue0.ToString(), cellValue.ToString());
                    formSped.ShowDialog();

                    this.TopMost = true;
                }
            }
        }

        private void FormDebug_Resize(object sender, EventArgs e)
        {
            if (this.panelGrid.Top == 0)
            {
                int larguraForm           = this.Width;   // Padrão: 738
                int alturaForm            = this.Height;  // Padrão: 505

                panelGrid.Height          = (alturaForm - 47);
                tbTeste.Top               = (alturaForm - 101);
                btnRunTeste.Top           = (alturaForm - 105);
                dataGridViewSelect.Height = (alturaForm - 55);
                textBoxComunicacao.Height = (alturaForm - 55);

                panelGrid.Width           = (larguraForm - 30);
                dataGridViewSelect.Width  = (larguraForm - 42);
                textBoxComunicacao.Width  = (larguraForm - 42);

                btnRunTeste.Location      = new Point(larguraForm - 70, btnRunTeste.Location.Y);  // Padrão: X=668 e Y=711

                png2Nuvem.Location        = new System.Drawing.Point((larguraForm - png2Nuvem.Size.Width)  / 2,
                                                                     (alturaForm  - png2Nuvem.Size.Height) / 2);
            }
        }



        // -------------------------------------------[ TESTES DIVERSOS ] ------------------------------------------- //

        private void TestesDiversos()  // Clique no botão "Run Teste"
        {
            string linhaSped = "| E110 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 | 0,00 |";

            textBoxComunicacao.Text = RetCortaPalavra(linhaSped, "|", 6, true);
            textBoxComunicacao.Refresh();
        }

        public DateTime DataDebug()   // Data do dia da compilação, para forçar o teste no dia que precisa fazê-lo
        {
            return new DateTime(2024, 5, 9);
        }

        private void tbTeste_TextChanged(object sender, EventArgs e) // Uma alteração no TextBox "tbTeste" (canto inferior esquerdo)
        {
        }

        private string RetCortaPalavra(string linhaString, string caracterCorte, int incidParaCorte, bool formatarNumero)
        {
            if (!linhaString.Contains(caracterCorte))
            {
                return (formatarNumero ? "0" : "");
            }

            string retornoString = "";
            int posAnalise = 1;
            int incAnalise = 0;

            while (posAnalise < linhaString.Length && incAnalise <= incidParaCorte)
            {
                if (linhaString.Substring(posAnalise - 1, 1) == caracterCorte)
                {
                    incAnalise++;
                }
                if (incAnalise == incidParaCorte)
                {
                    if (linhaString.Substring(posAnalise - 1, 1) != caracterCorte)
                    {
                        retornoString += linhaString.Substring(posAnalise - 1, 1);
                    }
                }
                posAnalise++;
            }

            if (formatarNumero)
            {
                if (retornoString.Length > 0)
                {
                    retornoString = retornoString.Replace(",", ".");
                }
                else retornoString = "0";
            }

            return retornoString;
        }


    }
}
