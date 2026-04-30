namespace Docx2PDF_WinForms
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void listArquivos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SelecionarPasta(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta com arquivos .docx"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pastaSelecionada = dialog.SelectedPath;
                lblPasta.Text = pastaSelecionada;
                listArquivos.Items.Clear();

                var arquivos = Directory.GetFiles(pastaSelecionada, "*.docx");
                foreach (var a in arquivos)
                    listArquivos.Items.Add(Path.GetFileName(a));

                btnConverter.Enabled = arquivos.Length > 0;
                lblStatus.Text = $"{arquivos.Length} arquivo(s) encontrado(s)";
            }
        }
    }
}
