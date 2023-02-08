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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MUHAMMEDYILMAZ\SQLEXPRESS;Initial Catalog=DbBankamatik;Integrated Security=True");

        private void LnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmKayitOl fr = new FrmKayitOl();
            fr.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLKISILER WHERE HESAPNO=@P1 AND SIFRE=@P2", baglanti);
            cmd.Parameters.AddWithValue("@P1", MskHesapNo.Text);
            cmd.Parameters.AddWithValue("@P2", TxtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                FrmHavale fr = new FrmHavale();
                fr.hesapno = MskHesapNo.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Hesap No veya Şifre Hatalı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
