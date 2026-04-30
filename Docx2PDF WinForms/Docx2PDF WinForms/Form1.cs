using System.Diagnostics;

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
        private async void Converter(object sender, EventArgs e)
        {
            string libreOffice = @"C:\Program Files\LibreOffice\program\soffice.exe";

            if (!File.Exists(libreOffice))
            {
                MessageBox.Show(
                    "LibreOffice não encontrado!\nBaixe em: https://www.libreoffice.org",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string pastaSaida = Path.Combine(pastaSelecionada, "pdfs");
            Directory.CreateDirectory(pastaSaida);

            var arquivos = Directory.GetFiles(pastaSelecionada, "*.docx");
            progressBar.Maximum = arquivos.Length;
            progressBar.Value = 0;
            btnConverter.Enabled = false;
            btnSelecionarPasta.Enabled = false;

            int sucesso = 0, falha = 0;

            await Task.Run(() =>
            {
                foreach (var arquivo in arquivos)
                {
                    Invoke(() => lblStatus.Text = $"Convertendo: {Path.GetFileName(arquivo)}...");

                    var processo = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = libreOffice,
                            Arguments = $"--headless --convert-to pdf --outdir \"{pastaSaida}\" \"{arquivo}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    processo.Start();
                    processo.WaitForExit();

                    string pdf = Path.Combine(pastaSaida,
                        Path.GetFileNameWithoutExtension(arquivo) + ".pdf");

                    if (File.Exists(pdf)) sucesso++;
                    else falha++;

                    Invoke(() => progressBar.Value++);
                }
            });

            btnConverter.Enabled = true;
            btnSelecionarPasta.Enabled = true;
            lblStatus.Text = $"✅ {sucesso} convertido(s)  ❌ {falha} falha(s)";

            MessageBox.Show(
                $"Concluído!\n\n{sucesso} PDF(s) salvos em:\n{pastaSaida}",
                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
