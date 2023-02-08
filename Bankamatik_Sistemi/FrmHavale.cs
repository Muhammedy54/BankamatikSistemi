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
    public partial class FrmHavale : Form
    {
        public FrmHavale()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MUHAMMEDYILMAZ\SQLEXPRESS;Initial Catalog=DbBankamatik;Integrated Security=True");

        public string hesapno;

        private void FrmHavale_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesapno;

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLKISILER WHERE HESAPNO=@P1", baglanti);
            cmd.Parameters.AddWithValue("@P1", LblHesapNo.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LblTCKimlik.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
                LblAdSoyad.Text = dr[1] + " " + dr[2];
            }
            baglanti.Close();
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            //para artışı
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update TBLHESAP SET BAKIYE=BAKIYE+@P1 WHERE HESAPNO=@P2",baglanti);
            cmd.Parameters.AddWithValue("@P1",decimal.Parse(TxtTutar.Text));
            cmd.Parameters.AddWithValue("@P2", MskHesapNo.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //para azalışı
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("update TBLHESAP SET BAKIYE=BAKIYE-@K1 WHERE HESAPNO=@K2",baglanti);
            cmd2.Parameters.AddWithValue("@K1", decimal.Parse(TxtTutar.Text));
            cmd2.Parameters.AddWithValue("@K2", hesapno);
            cmd2.ExecuteNonQuery();
            baglanti.Close();

            //hareket tablosu
            baglanti.Open();
            SqlCommand cmd3 = new SqlCommand("insert into TBLHAREKET (GONDEREN,ALICI,TUTAR) VALUES (@P1,@P2,@P3)",baglanti);
            cmd3.Parameters.AddWithValue("@P1", hesapno);
            cmd3.Parameters.AddWithValue("@P2", MskHesapNo.Text);
            cmd3.Parameters.AddWithValue("@P3", TxtTutar.Text);
            cmd3.ExecuteNonQuery();
            baglanti.Close();
        }

        private void BtnHareketler_Click(object sender, EventArgs e)
        {
            FrmHareketler fr = new FrmHareketler();
            fr.Show();
        }
    }
}
