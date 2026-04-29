namespace Docx2PDF;

using System.Diagnostics;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FormPrincipal());
    }
}

public class FormPrincipal : Form
{
    private Button btnSelecionarPasta;
    private Button btnConverter;
    private Label lblPasta;
    private ListBox listArquivos;
    private ProgressBar progressBar;
    private Label lblStatus;
    private string pastaSelecionada = "";

    public FormPrincipal()
    {
        Text = "Conversor DOCX → PDF";
        Width = 600;
        Height = 480;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        lblPasta = new Label
        {
            Text = "Nenhuma pasta selecionada",
            Left = 20,
            Top = 20,
            Width = 450,
            AutoSize = false
        };

        btnSelecionarPasta = new Button
        {
            Text = "Selecionar Pasta",
            Left = 20,
            Top = 50,
            Width = 150,
            Height = 35
        };
        btnSelecionarPasta.Click += SelecionarPasta!;

        listArquivos = new ListBox
        {
            Left = 20,
            Top = 100,
            Width = 540,
            Height = 230
        };

        progressBar = new ProgressBar
        {
            Left = 20,
            Top = 345,
            Width = 540,
            Height = 25
        };

        lblStatus = new Label
        {
            Text = "",
            Left = 20,
            Top = 375,
            Width = 540,
            AutoSize = false
        };

        btnConverter = new Button
        {
            Text = "Converter para PDF",
            Left = 20,
            Top = 400,
            Width = 175,
            Height = 35,
            Enabled = false
        };
        btnConverter.Click += Converter!;

        Controls.AddRange(
            lblPasta, btnSelecionarPasta,
            listArquivos, progressBar,
            lblStatus, btnConverter
        );
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