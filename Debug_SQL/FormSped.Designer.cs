namespace Debug_SQL
{
    partial class FormSped
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxRegistro = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btInterromper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRegistro
            // 
            this.textBoxRegistro.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRegistro.Location = new System.Drawing.Point(4, 5);
            this.textBoxRegistro.Multiline = true;
            this.textBoxRegistro.Name = "textBoxRegistro";
            this.textBoxRegistro.Size = new System.Drawing.Size(790, 503);
            this.textBoxRegistro.TabIndex = 0;
            this.textBoxRegistro.TextChanged += new System.EventHandler(this.textBoxRegistro_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Executar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btInterromper
            // 
            this.btInterromper.Location = new System.Drawing.Point(408, 523);
            this.btInterromper.Name = "btInterromper";
            this.btInterromper.Size = new System.Drawing.Size(170, 38);
            this.btInterromper.TabIndex = 2;
            this.btInterromper.Text = "Interromper";
            this.btInterromper.UseVisualStyleBackColor = true;
            this.btInterromper.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormSped
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 576);
            this.Controls.Add(this.btInterromper);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxRegistro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSped";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSped_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRegistro;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btInterromper;
    }
}