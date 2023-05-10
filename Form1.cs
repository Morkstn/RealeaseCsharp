using Newtonsoft.Json;
using RealeaseC_.ConsultarCep;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace RealeaseC_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // Verifica se o conteúdo do TextBox é um número inteiro válido
            if (int.TryParse(textBox.Text, out int numero))
            {
                // Adiciona o número à ListBox mantendo a ordem da lista
                int index = 0;
                while (index < lstNumber.Items.Count && (int)lstNumber.Items[index] < numero)
                {
                    index++;
                }
                lstNumber.Items.Insert(index, numero);
            }

            // Limpa o TextBox
            textBox.Clear();
        }


        private void btnGravar_Click(object sender, EventArgs e)
        {

            using (var sw = new StreamWriter("numeros_ordenar.txt"))
            {
                foreach (var item in lstNumber.Items)
                {
                    sw.WriteLine(item);
                }
            }
            MessageBox.Show("Números gravados com sucesso!");
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Process.Start("numeros_ordenar.txt");
        }
        // Q3 Q4 e Q5


            private void btnCriar_Click_1(object sender, EventArgs e)
        
            {// Cria a lista de objetos do tipo clsTeste
            List<clsTeste> listaTeste = new List<clsTeste>();
            for (int i = 1; i <= 100; i++)
            {
                listaTeste.Add(new clsTeste() { codigo = i, descricao = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") });
            }

            string json = JsonConvert.SerializeObject(listaTeste);
            File.WriteAllText("data.json", json);
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                List<clsTeste> lista = JsonConvert.DeserializeObject<List<clsTeste>>(json);
                dataGridView1.DataSource = lista;
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        //Q6
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria um objeto do serviço dos Correios
                var correios = new AtendeClienteClient();

                // Consulta o CEP informado no TextBox
                var endereco = correios.consultaCEP(txtCep.Text);

                // Adiciona o endereço na ListBox 
                lstEndereco.Items.Clear();
                lstEndereco.Items.Add(endereco.bairro);
                lstEndereco.Items.Add(endereco.cep);
                lstEndereco.Items.Add($"{endereco.cidade} - {endereco.uf}");
                lstEndereco.Items.Add(endereco.complemento2);
                lstEndereco.Items.Add(endereco.end);

                MessageBox.Show("Consulta realizada com sucesso!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao consultar o CEP: {ex.Message}");
            }
        }


        //Q7
        private async void btnAddBanks_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://brasilapi.com.br/api/banks/v1");
            var content = await response.Content.ReadAsStringAsync();
            var banks = JsonConvert.DeserializeObject<List<clsBancos>>(content);

            dataGridView2.DataSource = banks;
        }
        //Q8
        private void btnDonwload_Click(object sender, EventArgs e)
        {

            // Define a URL da imagem a ser baixada
            string imageUrl = "https://redeservice.com.br/wp-content/uploads/2020/07/redeservice-logo.png";

            // Define o nome do arquivo e o diretório de destino
            string fileName = "redeservice-logo.png";
            string saveDirectory = Application.StartupPath; // pasta onde a aplicação está sendo executada

            // Concatena o nome do arquivo e o diretório de destino em um caminho completo
            string savePath = Path.Combine(saveDirectory, fileName);

            // Cria uma instância do WebClient para baixar a imagem
            WebClient client = new WebClient();

            // Baixa a imagem e salva no disco
            client.DownloadFile(imageUrl, savePath);

            // Carrega a imagem a partir do disco
            System.Drawing.Image image = System.Drawing.Image.FromFile(savePath);

            // Define a imagem para o PictureBox
            pictureBox.Image = image;

            // Converte a imagem em um formato base64 e exibe na TextBox
            textBoxImage.Text = ImageToBase64(image);

        }

        private string ImageToBase64(System.Drawing.Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Converte a imagem em um array de bytes usando o formato PNG
                image.Save(ms, ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Converte o array de bytes em uma string base64
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }


        private void btnExibir_Click(object sender, EventArgs e)
        {
            
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
