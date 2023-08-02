﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Vural\SQLEXPRESS;Initial Catalog=FilmArsivim;Integrated Security=True");

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
    }
}
