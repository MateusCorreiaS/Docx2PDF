namespace Docx2PDF_WinForms
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelecionarPasta = new Button();
            lblPasta = new Label();
            listArquivos = new ListBox();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            btnConverter = new Button();
            SuspendLayout();
            // 
            // btnSelecionarPasta
            // 
            btnSelecionarPasta.Location = new Point(20, 50);
            btnSelecionarPasta.Name = "btnSelecionarPasta";
            btnSelecionarPasta.Size = new Size(150, 35);
            btnSelecionarPasta.TabIndex = 0;
            btnSelecionarPasta.Text = "Selecionar Pasta";
            btnSelecionarPasta.UseVisualStyleBackColor = true;
            btnSelecionarPasta.Click += SelecionarPasta;
            // 
            // lblPasta
            // 
            lblPasta.Location = new Point(20, 20);
            lblPasta.Name = "lblPasta";
            lblPasta.Size = new Size(450, 15);
            lblPasta.TabIndex = 1;
            lblPasta.Text = "Nenhuma pasta selecionada";
            // 
            // listArquivos
            // 
            listArquivos.FormattingEnabled = true;
            listArquivos.Location = new Point(20, 100);
            listArquivos.Name = "listArquivos";
            listArquivos.Size = new Size(540, 229);
            listArquivos.TabIndex = 2;
            listArquivos.SelectedIndexChanged += listArquivos_SelectedIndexChanged;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 345);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(540, 25);
            progressBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(20, 375);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(540, 15);
            lblStatus.TabIndex = 4;
            // 
            // btnConverter
            // 
            btnConverter.Location = new Point(20, 400);
            btnConverter.Name = "btnConverter";
            btnConverter.Size = new Size(150, 35);
            btnConverter.TabIndex = 5;
            btnConverter.Text = "Converter para PDF";
            btnConverter.UseVisualStyleBackColor = true;
            btnConverter.Click += Converter;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 441);
            Controls.Add(btnConverter);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(listArquivos);
            Controls.Add(lblPasta);
            Controls.Add(btnSelecionarPasta);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Conversor DOCX → PDF";
            Load += FormPrincipal_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelecionarPasta;
        private Label lblPasta;
        private ListBox listArquivos;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Button btnConverter;
        private string pastaSelecionada = "";
    }
}
