namespace Debug_SQL
{
    partial class FormDebug
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDebug));
            this.errorIcone = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTipMsg = new System.Windows.Forms.ToolTip(this.components);
            this.tbNovoArquivo = new System.Windows.Forms.TextBox();
            this.tbProcurar = new System.Windows.Forms.TextBox();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.tbTeste = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbServidor = new System.Windows.Forms.TextBox();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSenha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDominio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.btnRunTeste = new System.Windows.Forms.Button();
            this.lblArquivoAberto = new System.Windows.Forms.Label();
            this.png2Nuvem = new System.Windows.Forms.Panel();
            this.lblNuvem = new System.Windows.Forms.Label();
            this.pngNuvem = new System.Windows.Forms.PictureBox();
            this.listBoxProcedures = new System.Windows.Forms.ListBox();
            this.dataGridViewSelect = new System.Windows.Forms.DataGridView();
            this.textBoxComunicacao = new System.Windows.Forms.TextBox();
            this.richTextBoxAcompanhamento = new System.Windows.Forms.RichTextBox();
            this.timerXray = new System.Windows.Forms.Timer(this.components);
            this.btnRun = new System.Windows.Forms.Button();
            this.btnAcompanhamento = new System.Windows.Forms.Button();
            this.btnReiniciarDebug = new System.Windows.Forms.Button();
            this.btnWaypoins = new System.Windows.Forms.Button();
            this.btnCriarDebug = new System.Windows.Forms.Button();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.logoSaurus = new System.Windows.Forms.PictureBox();
            this.btnAcessoV4 = new System.Windows.Forms.Button();
            this.arqAcompanhamentos = new System.Windows.Forms.Button();
            this.panelAcompanhamento = new System.Windows.Forms.Panel();
            this.btnNovoArquivo = new System.Windows.Forms.Button();
            this.labelNomeArq = new System.Windows.Forms.Label();
            this.listBoxAcompanhamento = new System.Windows.Forms.ListBox();
            this.chBxMesmaLinha = new System.Windows.Forms.CheckBox();
            this.chBxNumerarLinhas = new System.Windows.Forms.CheckBox();
            this.btnCopiaSenha = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorIcone)).BeginInit();
            this.panelGrid.SuspendLayout();
            this.png2Nuvem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pngNuvem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoSaurus)).BeginInit();
            this.panelAcompanhamento.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorIcone
            // 
            this.errorIcone.ContainerControl = this;
            // 
            // toolTipMsg
            // 
            this.toolTipMsg.IsBalloon = true;
            // 
            // tbNovoArquivo
            // 
            this.tbNovoArquivo.Location = new System.Drawing.Point(6, 125);
            this.tbNovoArquivo.Name = "tbNovoArquivo";
            this.tbNovoArquivo.Size = new System.Drawing.Size(105, 22);
            this.tbNovoArquivo.TabIndex = 1;
            this.toolTipMsg.SetToolTip(this.tbNovoArquivo, "Digite o nome do arquivo a ser gravado");
            // 
            // tbProcurar
            // 
            this.tbProcurar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProcurar.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProcurar.Location = new System.Drawing.Point(10, 76);
            this.tbProcurar.Name = "tbProcurar";
            this.tbProcurar.Size = new System.Drawing.Size(485, 34);
            this.tbProcurar.TabIndex = 10;
            this.toolTipMsg.SetToolTip(this.tbProcurar, resources.GetString("tbProcurar.ToolTip"));
            this.tbProcurar.TextChanged += new System.EventHandler(this.tbProcurar_TextChanged);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcluir.BackgroundImage")));
            this.btnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExcluir.Location = new System.Drawing.Point(876, 10);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(26, 32);
            this.btnExcluir.TabIndex = 4;
            this.toolTipMsg.SetToolTip(this.btnExcluir, "Exclusão do arquivo de acompanhamento");
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // tbTeste
            // 
            this.tbTeste.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTeste.Location = new System.Drawing.Point(10, 362);
            this.tbTeste.Name = "tbTeste";
            this.tbTeste.Size = new System.Drawing.Size(878, 24);
            this.tbTeste.TabIndex = 9;
            this.toolTipMsg.SetToolTip(this.tbTeste, "Para filtrar a incidência de mais de uma expressão, separe-as com um \"~\" (til)\r\n\r" +
        "\nExemplo:\r\n\r\n|C190~E110\r\n\r\nOBS: Dois tracinhos no início, faz o aplicativo ignor" +
        "ar o filtro\r\n");
            this.tbTeste.TextChanged += new System.EventHandler(this.tbTeste_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(290, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor:";
            // 
            // tbServidor
            // 
            this.tbServidor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbServidor.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServidor.Location = new System.Drawing.Point(378, 9);
            this.tbServidor.Name = "tbServidor";
            this.tbServidor.Size = new System.Drawing.Size(518, 30);
            this.tbServidor.TabIndex = 2;
            this.tbServidor.TextChanged += new System.EventHandler(this.tbServidor_TextChanged);
            // 
            // tbUsuario
            // 
            this.tbUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsuario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsuario.Location = new System.Drawing.Point(378, 42);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(152, 30);
            this.tbUsuario.TabIndex = 4;
            this.tbUsuario.TextChanged += new System.EventHandler(this.tbUsuario_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.CausesValidation = false;
            this.label2.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(290, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuário:";
            // 
            // tbSenha
            // 
            this.tbSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSenha.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSenha.Location = new System.Drawing.Point(617, 42);
            this.tbSenha.Name = "tbSenha";
            this.tbSenha.Size = new System.Drawing.Size(279, 30);
            this.tbSenha.TabIndex = 6;
            this.tbSenha.UseSystemPasswordChar = true;
            this.tbSenha.TextChanged += new System.EventHandler(this.tbSenha_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(548, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Senha:";
            // 
            // tbDominio
            // 
            this.tbDominio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDominio.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDominio.Location = new System.Drawing.Point(617, 77);
            this.tbDominio.Name = "tbDominio";
            this.tbDominio.Size = new System.Drawing.Size(279, 30);
            this.tbDominio.TabIndex = 8;
            this.tbDominio.TextChanged += new System.EventHandler(this.tbDominio_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.CausesValidation = false;
            this.label4.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(534, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Domínio:";
            // 
            // panelGrid
            // 
            this.panelGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGrid.Controls.Add(this.tbTeste);
            this.panelGrid.Controls.Add(this.btnRunTeste);
            this.panelGrid.Controls.Add(this.lblArquivoAberto);
            this.panelGrid.Controls.Add(this.png2Nuvem);
            this.panelGrid.Controls.Add(this.btnExcluir);
            this.panelGrid.Controls.Add(this.listBoxProcedures);
            this.panelGrid.Controls.Add(this.dataGridViewSelect);
            this.panelGrid.Controls.Add(this.textBoxComunicacao);
            this.panelGrid.Controls.Add(this.richTextBoxAcompanhamento);
            this.panelGrid.Location = new System.Drawing.Point(8, 165);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(942, 396);
            this.panelGrid.TabIndex = 12;
            // 
            // btnRunTeste
            // 
            this.btnRunTeste.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRunTeste.BackgroundImage")));
            this.btnRunTeste.Location = new System.Drawing.Point(891, 349);
            this.btnRunTeste.Name = "btnRunTeste";
            this.btnRunTeste.Size = new System.Drawing.Size(42, 40);
            this.btnRunTeste.TabIndex = 8;
            this.btnRunTeste.UseVisualStyleBackColor = true;
            this.btnRunTeste.Click += new System.EventHandler(this.btnRunTeste_Click);
            // 
            // lblArquivoAberto
            // 
            this.lblArquivoAberto.AutoSize = true;
            this.lblArquivoAberto.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lblArquivoAberto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblArquivoAberto.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblArquivoAberto.Location = new System.Drawing.Point(412, -4);
            this.lblArquivoAberto.Name = "lblArquivoAberto";
            this.lblArquivoAberto.Size = new System.Drawing.Size(113, 18);
            this.lblArquivoAberto.TabIndex = 7;
            this.lblArquivoAberto.Text = "Nome do arquivo";
            this.lblArquivoAberto.Visible = false;
            // 
            // png2Nuvem
            // 
            this.png2Nuvem.BackColor = System.Drawing.Color.Gainsboro;
            this.png2Nuvem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.png2Nuvem.Controls.Add(this.lblNuvem);
            this.png2Nuvem.Controls.Add(this.pngNuvem);
            this.png2Nuvem.Location = new System.Drawing.Point(399, 100);
            this.png2Nuvem.Name = "png2Nuvem";
            this.png2Nuvem.Size = new System.Drawing.Size(135, 132);
            this.png2Nuvem.TabIndex = 6;
            this.png2Nuvem.Visible = false;
            // 
            // lblNuvem
            // 
            this.lblNuvem.AutoSize = true;
            this.lblNuvem.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNuvem.Location = new System.Drawing.Point(12, 102);
            this.lblNuvem.Name = "lblNuvem";
            this.lblNuvem.Size = new System.Drawing.Size(115, 16);
            this.lblNuvem.TabIndex = 6;
            this.lblNuvem.Text = "lblNuvem.................";
            this.lblNuvem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNuvem.UseWaitCursor = true;
            this.lblNuvem.Visible = false;
            // 
            // pngNuvem
            // 
            this.pngNuvem.BackColor = System.Drawing.Color.Gainsboro;
            this.pngNuvem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pngNuvem.Image = ((System.Drawing.Image)(resources.GetObject("pngNuvem.Image")));
            this.pngNuvem.Location = new System.Drawing.Point(1, 2);
            this.pngNuvem.Name = "pngNuvem";
            this.pngNuvem.Size = new System.Drawing.Size(131, 124);
            this.pngNuvem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pngNuvem.TabIndex = 5;
            this.pngNuvem.TabStop = false;
            this.pngNuvem.UseWaitCursor = true;
            this.pngNuvem.Visible = false;
            // 
            // listBoxProcedures
            // 
            this.listBoxProcedures.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.listBoxProcedures.FormattingEnabled = true;
            this.listBoxProcedures.ItemHeight = 16;
            this.listBoxProcedures.Location = new System.Drawing.Point(664, 7);
            this.listBoxProcedures.Name = "listBoxProcedures";
            this.listBoxProcedures.Size = new System.Drawing.Size(239, 132);
            this.listBoxProcedures.TabIndex = 3;
            this.listBoxProcedures.Click += new System.EventHandler(this.listBoxProcedures_Click);
            // 
            // dataGridViewSelect
            // 
            this.dataGridViewSelect.AllowUserToAddRows = false;
            this.dataGridViewSelect.AllowUserToDeleteRows = false;
            this.dataGridViewSelect.AllowUserToOrderColumns = true;
            this.dataGridViewSelect.BackgroundColor = System.Drawing.Color.Beige;
            this.dataGridViewSelect.CausesValidation = false;
            this.dataGridViewSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSelect.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSelect.GridColor = System.Drawing.Color.Black;
            this.dataGridViewSelect.Location = new System.Drawing.Point(7, 48);
            this.dataGridViewSelect.Name = "dataGridViewSelect";
            this.dataGridViewSelect.ReadOnly = true;
            this.dataGridViewSelect.RowHeadersWidth = 51;
            this.dataGridViewSelect.RowTemplate.Height = 24;
            this.dataGridViewSelect.Size = new System.Drawing.Size(928, 339);
            this.dataGridViewSelect.TabIndex = 1;
            this.dataGridViewSelect.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelect_CellContentDoubleClick);
            // 
            // textBoxComunicacao
            // 
            this.textBoxComunicacao.AcceptsReturn = true;
            this.textBoxComunicacao.AcceptsTab = true;
            this.textBoxComunicacao.AllowDrop = true;
            this.textBoxComunicacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxComunicacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxComunicacao.BackColor = System.Drawing.Color.White;
            this.textBoxComunicacao.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxComunicacao.ForeColor = System.Drawing.Color.Black;
            this.textBoxComunicacao.Location = new System.Drawing.Point(5, 7);
            this.textBoxComunicacao.Multiline = true;
            this.textBoxComunicacao.Name = "textBoxComunicacao";
            this.textBoxComunicacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComunicacao.Size = new System.Drawing.Size(927, 379);
            this.textBoxComunicacao.TabIndex = 0;
            this.textBoxComunicacao.TextChanged += new System.EventHandler(this.tbComunicacao_TextChanged);
            // 
            // richTextBoxAcompanhamento
            // 
            this.richTextBoxAcompanhamento.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAcompanhamento.Location = new System.Drawing.Point(5, 6);
            this.richTextBoxAcompanhamento.Name = "richTextBoxAcompanhamento";
            this.richTextBoxAcompanhamento.ReadOnly = true;
            this.richTextBoxAcompanhamento.Size = new System.Drawing.Size(927, 379);
            this.richTextBoxAcompanhamento.TabIndex = 2;
            this.richTextBoxAcompanhamento.Text = "";
            // 
            // timerXray
            // 
            this.timerXray.Interval = 1000;
            this.timerXray.Tick += new System.EventHandler(this.timerXray_Tick);
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.Location = new System.Drawing.Point(902, 111);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(49, 49);
            this.btnRun.TabIndex = 17;
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnAcompanhamento
            // 
            this.btnAcompanhamento.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcompanhamento.Image = ((System.Drawing.Image)(resources.GetObject("btnAcompanhamento.Image")));
            this.btnAcompanhamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcompanhamento.Location = new System.Drawing.Point(636, 113);
            this.btnAcompanhamento.Name = "btnAcompanhamento";
            this.btnAcompanhamento.Size = new System.Drawing.Size(213, 49);
            this.btnAcompanhamento.TabIndex = 16;
            this.btnAcompanhamento.Text = "Acompanhamento";
            this.btnAcompanhamento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcompanhamento.UseVisualStyleBackColor = true;
            this.btnAcompanhamento.Click += new System.EventHandler(this.btnAcompanhamento_Click);
            // 
            // btnReiniciarDebug
            // 
            this.btnReiniciarDebug.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReiniciarDebug.Image = global::Debug_SQL.Properties.Resources.download_32;
            this.btnReiniciarDebug.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReiniciarDebug.Location = new System.Drawing.Point(10, 113);
            this.btnReiniciarDebug.Name = "btnReiniciarDebug";
            this.btnReiniciarDebug.Size = new System.Drawing.Size(211, 49);
            this.btnReiniciarDebug.TabIndex = 15;
            this.btnReiniciarDebug.Text = "Reiniciar Debug";
            this.btnReiniciarDebug.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReiniciarDebug.UseVisualStyleBackColor = true;
            this.btnReiniciarDebug.Click += new System.EventHandler(this.btnReiniciarDebug_Click);
            // 
            // btnWaypoins
            // 
            this.btnWaypoins.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaypoins.Image = ((System.Drawing.Image)(resources.GetObject("btnWaypoins.Image")));
            this.btnWaypoins.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWaypoins.Location = new System.Drawing.Point(226, 113);
            this.btnWaypoins.Name = "btnWaypoins";
            this.btnWaypoins.Size = new System.Drawing.Size(186, 49);
            this.btnWaypoins.TabIndex = 14;
            this.btnWaypoins.Text = "Waypoints";
            this.btnWaypoins.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWaypoins.UseVisualStyleBackColor = true;
            this.btnWaypoins.Click += new System.EventHandler(this.btnWayPoints_Click);
            // 
            // btnCriarDebug
            // 
            this.btnCriarDebug.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriarDebug.Image = ((System.Drawing.Image)(resources.GetObject("btnCriarDebug.Image")));
            this.btnCriarDebug.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCriarDebug.Location = new System.Drawing.Point(417, 113);
            this.btnCriarDebug.Name = "btnCriarDebug";
            this.btnCriarDebug.Size = new System.Drawing.Size(214, 49);
            this.btnCriarDebug.TabIndex = 13;
            this.btnCriarDebug.Text = "Criar debugador";
            this.btnCriarDebug.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCriarDebug.UseVisualStyleBackColor = true;
            this.btnCriarDebug.Click += new System.EventHandler(this.btnCriarDebug_Click);
            // 
            // btnProcurar
            // 
            this.btnProcurar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnProcurar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcurar.BackgroundImage")));
            this.btnProcurar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProcurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcurar.Location = new System.Drawing.Point(495, 76);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(36, 36);
            this.btnProcurar.TabIndex = 11;
            this.btnProcurar.UseVisualStyleBackColor = false;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConectar.Image = ((System.Drawing.Image)(resources.GetObject("btnConectar.Image")));
            this.btnConectar.Location = new System.Drawing.Point(902, 58);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(50, 48);
            this.btnConectar.TabIndex = 9;
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // logoSaurus
            // 
            this.logoSaurus.Image = ((System.Drawing.Image)(resources.GetObject("logoSaurus.Image")));
            this.logoSaurus.Location = new System.Drawing.Point(13, 2);
            this.logoSaurus.Name = "logoSaurus";
            this.logoSaurus.Size = new System.Drawing.Size(158, 51);
            this.logoSaurus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoSaurus.TabIndex = 18;
            this.logoSaurus.TabStop = false;
            // 
            // btnAcessoV4
            // 
            this.btnAcessoV4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcessoV4.Image = ((System.Drawing.Image)(resources.GetObject("btnAcessoV4.Image")));
            this.btnAcessoV4.Location = new System.Drawing.Point(853, 111);
            this.btnAcessoV4.Name = "btnAcessoV4";
            this.btnAcessoV4.Size = new System.Drawing.Size(46, 49);
            this.btnAcessoV4.TabIndex = 19;
            this.btnAcessoV4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcessoV4.UseVisualStyleBackColor = true;
            this.btnAcessoV4.Click += new System.EventHandler(this.btnAcessoV4_Click);
            // 
            // arqAcompanhamentos
            // 
            this.arqAcompanhamentos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.arqAcompanhamentos.Image = ((System.Drawing.Image)(resources.GetObject("arqAcompanhamentos.Image")));
            this.arqAcompanhamentos.Location = new System.Drawing.Point(902, 8);
            this.arqAcompanhamentos.Name = "arqAcompanhamentos";
            this.arqAcompanhamentos.Size = new System.Drawing.Size(50, 48);
            this.arqAcompanhamentos.TabIndex = 20;
            this.arqAcompanhamentos.UseVisualStyleBackColor = false;
            this.arqAcompanhamentos.Click += new System.EventHandler(this.arqAcompanhamentos_Click);
            // 
            // panelAcompanhamento
            // 
            this.panelAcompanhamento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcompanhamento.Controls.Add(this.btnNovoArquivo);
            this.panelAcompanhamento.Controls.Add(this.labelNomeArq);
            this.panelAcompanhamento.Controls.Add(this.tbNovoArquivo);
            this.panelAcompanhamento.Controls.Add(this.listBoxAcompanhamento);
            this.panelAcompanhamento.Location = new System.Drawing.Point(794, 10);
            this.panelAcompanhamento.Name = "panelAcompanhamento";
            this.panelAcompanhamento.Size = new System.Drawing.Size(156, 155);
            this.panelAcompanhamento.TabIndex = 4;
            this.panelAcompanhamento.Visible = false;
            // 
            // btnNovoArquivo
            // 
            this.btnNovoArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoArquivo.Location = new System.Drawing.Point(112, 124);
            this.btnNovoArquivo.Name = "btnNovoArquivo";
            this.btnNovoArquivo.Size = new System.Drawing.Size(34, 24);
            this.btnNovoArquivo.TabIndex = 3;
            this.btnNovoArquivo.Text = "Ok";
            this.btnNovoArquivo.UseVisualStyleBackColor = true;
            this.btnNovoArquivo.Click += new System.EventHandler(this.btnNovoArquivo_Click);
            // 
            // labelNomeArq
            // 
            this.labelNomeArq.AutoSize = true;
            this.labelNomeArq.Location = new System.Drawing.Point(6, 107);
            this.labelNomeArq.Name = "labelNomeArq";
            this.labelNomeArq.Size = new System.Drawing.Size(111, 16);
            this.labelNomeArq.TabIndex = 2;
            this.labelNomeArq.Text = "Nome do arquivo";
            // 
            // listBoxAcompanhamento
            // 
            this.listBoxAcompanhamento.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listBoxAcompanhamento.FormattingEnabled = true;
            this.listBoxAcompanhamento.ItemHeight = 16;
            this.listBoxAcompanhamento.Location = new System.Drawing.Point(3, 3);
            this.listBoxAcompanhamento.Name = "listBoxAcompanhamento";
            this.listBoxAcompanhamento.Size = new System.Drawing.Size(147, 148);
            this.listBoxAcompanhamento.TabIndex = 0;
            this.listBoxAcompanhamento.Visible = false;
            this.listBoxAcompanhamento.Click += new System.EventHandler(this.listBoxAcompanhamento_Click);
            // 
            // chBxMesmaLinha
            // 
            this.chBxMesmaLinha.AutoSize = true;
            this.chBxMesmaLinha.ForeColor = System.Drawing.Color.Black;
            this.chBxMesmaLinha.Location = new System.Drawing.Point(12, 55);
            this.chBxMesmaLinha.Name = "chBxMesmaLinha";
            this.chBxMesmaLinha.Size = new System.Drawing.Size(135, 20);
            this.chBxMesmaLinha.TabIndex = 21;
            this.chBxMesmaLinha.Text = "Na mesma linha...";
            this.chBxMesmaLinha.UseVisualStyleBackColor = true;
            // 
            // chBxNumerarLinhas
            // 
            this.chBxNumerarLinhas.AutoSize = true;
            this.chBxNumerarLinhas.ForeColor = System.Drawing.Color.Black;
            this.chBxNumerarLinhas.Location = new System.Drawing.Point(159, 55);
            this.chBxNumerarLinhas.Name = "chBxNumerarLinhas";
            this.chBxNumerarLinhas.Size = new System.Drawing.Size(130, 20);
            this.chBxNumerarLinhas.TabIndex = 22;
            this.chBxNumerarLinhas.Text = "Mostrar as linhas";
            this.chBxNumerarLinhas.UseVisualStyleBackColor = true;
            // 
            // btnCopiaSenha
            // 
            this.btnCopiaSenha.Font = new System.Drawing.Font("Arial", 10.8F);
            this.btnCopiaSenha.Location = new System.Drawing.Point(536, 41);
            this.btnCopiaSenha.Name = "btnCopiaSenha";
            this.btnCopiaSenha.Size = new System.Drawing.Size(81, 32);
            this.btnCopiaSenha.TabIndex = 23;
            this.btnCopiaSenha.Text = "Senha";
            this.btnCopiaSenha.UseVisualStyleBackColor = true;
            this.btnCopiaSenha.Click += new System.EventHandler(this.btnCopiaSenha_Click);
            // 
            // FormDebug
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(962, 573);
            this.Controls.Add(this.btnCopiaSenha);
            this.Controls.Add(this.panelAcompanhamento);
            this.Controls.Add(this.arqAcompanhamentos);
            this.Controls.Add(this.btnAcessoV4);
            this.Controls.Add(this.logoSaurus);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnAcompanhamento);
            this.Controls.Add(this.btnReiniciarDebug);
            this.Controls.Add(this.btnWaypoins);
            this.Controls.Add(this.btnCriarDebug);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.btnProcurar);
            this.Controls.Add(this.tbProcurar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.tbDominio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSenha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbServidor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chBxMesmaLinha);
            this.Controls.Add(this.chBxNumerarLinhas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Saurus | Debug SQL Server";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDebug_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FormDebug_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.errorIcone)).EndInit();
            this.panelGrid.ResumeLayout(false);
            this.panelGrid.PerformLayout();
            this.png2Nuvem.ResumeLayout(false);
            this.png2Nuvem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pngNuvem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoSaurus)).EndInit();
            this.panelAcompanhamento.ResumeLayout(false);
            this.panelAcompanhamento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorIcone;
        private System.Windows.Forms.ToolTip toolTipMsg;
        private System.Windows.Forms.TextBox tbSenha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServidor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDominio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox tbProcurar;
        private System.Windows.Forms.Button btnProcurar;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.TextBox textBoxComunicacao;
        private System.Windows.Forms.Button btnWaypoins;
        private System.Windows.Forms.Button btnCriarDebug;
        private System.Windows.Forms.Button btnReiniciarDebug;
        private System.Windows.Forms.Button btnAcompanhamento;
        private System.Windows.Forms.DataGridView dataGridViewSelect;
        private System.Windows.Forms.RichTextBox richTextBoxAcompanhamento;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Timer timerXray;
        private System.Windows.Forms.PictureBox logoSaurus;
        private System.Windows.Forms.ListBox listBoxProcedures;
        private System.Windows.Forms.Button btnAcessoV4;
        private System.Windows.Forms.Button arqAcompanhamentos;
        private System.Windows.Forms.Panel panelAcompanhamento;
        private System.Windows.Forms.ListBox listBoxAcompanhamento;
        private System.Windows.Forms.TextBox tbNovoArquivo;
        private System.Windows.Forms.Label labelNomeArq;
        private System.Windows.Forms.Button btnNovoArquivo;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Panel png2Nuvem;
        private System.Windows.Forms.PictureBox pngNuvem;
        private System.Windows.Forms.Label lblNuvem;
        private System.Windows.Forms.Label lblArquivoAberto;
        private System.Windows.Forms.CheckBox chBxMesmaLinha;
        private System.Windows.Forms.CheckBox chBxNumerarLinhas;
        private System.Windows.Forms.Button btnCopiaSenha;
        private System.Windows.Forms.Button btnRunTeste;
        private System.Windows.Forms.TextBox tbTeste;
    }
}

