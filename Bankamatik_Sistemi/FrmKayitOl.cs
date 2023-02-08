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

namespace Bankamatik_Sistemi
{
    public partial class FrmKayitOl : Form
    {
        public FrmKayitOl()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MUHAMMEDYILMAZ\SQLEXPRESS;Initial Catalog=DbBankamatik;Integrated Security=True");

        private void BtnKayitOl_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            cmd.Parameters.AddWithValue("@P1",TxtAd.Text);
            cmd.Parameters.AddWithValue("@P2",TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@P3",MskTCKimlik.Text);
            cmd.Parameters.AddWithValue("@P4",MskTelefon.Text);
            cmd.Parameters.AddWithValue("@P5",MskHesapNo.Text);
            cmd.Parameters.AddWithValue("@P6",TxtSifre.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt İşleminiz Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("insert into TBLHESAP (HESAPNO) VALUES (@P1)", baglanti);
            cmd2.Parameters.AddWithValue("@P1", MskHesapNo.Text);
            cmd2.ExecuteNonQuery();
            baglanti.Close();

            TxtAd.Text = "";
            TxtSifre.Text = "";
            TxtSoyad.Text = "";
            MskHesapNo.Text = "";
            MskTCKimlik.Text = "";
            MskTelefon.Text = "";
        }

        private void BtnHesapNo_Click(object sender, EventArgs e)
        {
            
            Random rastgele = new Random();
            int sayi = rastgele.Next(100000,1000000);
            MskHesapNo.Text = sayi.ToString();

            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM TBLKISILER", baglanti);
            SqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                if (MskHesapNo.Text == dr[5].ToString())
                {
                    MessageBox.Show("Lütfen Başka Hesap No İsteyiniz(Tuşa Tekrar Tıkayınız.)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            baglanti.Close();
        }
    }
}
