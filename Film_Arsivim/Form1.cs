using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CefSharp.WinForms;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Vural\SQLEXPRESS;Initial Catalog=FilmArsivim;Integrated Security=True");
        private ChromiumWebBrowser tarayici; 
        void Filmler()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLFILMLER",baglanti); // Veritabanından verileri çekme
            DataTable dt = new DataTable(); // Verileri tabloda gösterme
            da.Fill(dt); // Verileri tabloya doldurma
            dataGridView1.DataSource = dt; // Verileri datagridview'e aktarma    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Filmler();
            
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLFILMLER (AD,KATEGORI,LINK) VALUES (@P1,@P2,@P3)",baglanti);
            komut.Parameters.AddWithValue("@P1",TxtFilmAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtKategori.Text);
            komut.Parameters.AddWithValue("@P3", TxtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Filmler();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seçilen satırın verilerini textboxlara aktarma
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string link = dataGridView1.Rows[secilen].Cells[3].Value.ToString(); // Seçilen filmin linkini almak için

            TxtFilmAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtKategori.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtLink.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            if (tarayici != null) // Webbrowser'ın boş olup olmadığını kontrol etme
            {
                tarayici.Load(link); 
            }
            else
            {
                tarayici = new ChromiumWebBrowser(link); // Seçilen filmin linkini webbrowser'da açma
                this.webBrowser1.Controls.Add(tarayici); // Webbrowser'ı forma ekleme
                tarayici.Dock = DockStyle.Fill;   // Webbrowser'ı forma tam ekran yapma
            }

            //webBrowser1.Navigate(link); // Seçilen filmin linkini webbrowser'da açma - desteklenmiyor -
        }

        private void OpenLink()
        {
            string link = "https://www.linkedin.com/in/vural-sayan-79326a171/"; 

            System.Diagnostics.Process.Start(link); // Linki varsayılan tarayıcıda açma
        }

        private void BtnHakkimizda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu program Vural Sayan tarafından 2.06.2023 tarihinde  yapılmıştır.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            OpenLink();
        }
    }
}
