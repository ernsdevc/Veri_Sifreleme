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

namespace Veri_Sifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MACHINEX;Initial Catalog=DBProjeVeriSifreleme;Integrated Security=True");

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLVeriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            string veri = "";
            string sifreliVeri = "";
            string sifreliAd = "";
            string sifreliSoyad = "";
            string sifreliMail = "";
            string sifreliSifre = "";
            string sifreliHesapNo = "";

            for (int i = 0; i <= sayac; i++)
            {
                if (i == 0)
                {
                    veri = txtAd.Text;
                }
                else if (i == 1)
                {
                    veri = txtSoyad.Text;
                    sifreliAd = sifreliVeri;
                }
                else if (i == 2)
                {
                    veri = txtMail.Text;
                    sifreliSoyad = sifreliVeri;
                }
                else if (i == 3)
                {
                    veri = txtSifre.Text;
                    sifreliMail = sifreliVeri;
                }
                else if (i == 4)
                {
                    veri = txtHesapNo.Text;
                    sifreliSifre = sifreliVeri;
                }
                else
                {
                    sifreliHesapNo = sifreliVeri;
                    break;
                }

                byte[] veriDizi = ASCIIEncoding.ASCII.GetBytes(veri);
                sifreliVeri = Convert.ToBase64String(veriDizi);
                sayac++;
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLVeriler(Ad,Soyad,Mail,Sifre,HesapNo) VALUES(@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", sifreliAd);
            komut.Parameters.AddWithValue("@p2", sifreliSoyad);
            komut.Parameters.AddWithValue("@p3", sifreliMail);
            komut.Parameters.AddWithValue("@p4", sifreliSifre);
            komut.Parameters.AddWithValue("@p5", sifreliHesapNo);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veriler Eklendi.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtHesapNo.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            string veri = "";
            string sifreliVeri = "";
            string ad = "";
            string soyad = "";
            string mail = "";
            string sifre = "";
            string hesapNo = "";

            for (int i = 0; i <= sayac; i++)
            {
                if (i == 0)
                {
                    sifreliVeri = txtAd.Text;
                }
                else if (i == 1)
                {
                    sifreliVeri = txtSoyad.Text;
                    ad = veri;
                }
                else if (i == 2)
                {
                    sifreliVeri = txtMail.Text;
                    soyad = veri;
                }
                else if (i == 3)
                {
                    sifreliVeri = txtSifre.Text;
                    mail = veri;
                }
                else if (i == 4)
                {
                    sifreliVeri = txtHesapNo.Text;
                    sifre = veri;
                }
                else
                {
                    hesapNo = veri;
                    txtAd.Text = ad;
                    txtSoyad.Text = soyad;
                    txtMail.Text = mail;
                    txtSifre.Text = sifre;
                    txtHesapNo.Text = hesapNo;
                    break;
                }

                byte[] veriDizi = Convert.FromBase64String(sifreliVeri);
                veri = ASCIIEncoding.ASCII.GetString(veriDizi);
                sayac++;
            }
        }
    }
}
