using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace MESAİ_TAKİP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\MesaiTakip.accdb; ");//Extended Properties='Excel 12.0 xml; IMEX=1;HDR=YES;     
        public static DataGridView frm1DataGridview = new DataGridView();
        public static OleDbDataAdapter dataAdaptor;
        public static DataTable dt;
        public static string sql;
        Form2 frm2;
        public static int tıkSatir;
        int i, j;
        private void Form1_Load(object sender, EventArgs e)
        {
            MesaiTakipExceliOkuma();
            MesaiAtamaExceliKontrol();          
        }
        private void KapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox1.Visible = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            
        }
        private void KisiEkletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                bool ayniKisi = false;
                for (int i = 0; i <dataGridView1.RowCount; i++)
                {
                    if (dataGridView1[0,i].Value.ToString()==textBox1.Text)
                    {
                        ayniKisi = true;                    
                        break;
                    }
                }
                if (ayniKisi==false)
                {           
                    MesaiKisiEkle();
                    MesaiTakipExceliOkuma();
                    MesaiAtamaExceliKontrol();
                    textBox1.Text = "";

                }
                else
                {
                    MessageBox.Show("Eklemek İstediğiniz Kişi Kayıtlarda Mevcuttur");
                }
            }
            else
            {
                MessageBox.Show("İşlem Yapılacak Ad Soyad Girin");
            }

        }
        private void KisiSiltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count == 1)
            {
                MesaiKisiSil();
                MesaiTakipExceliOkuma();
                MesaiAtamaExceliKontrol();
            }
            else
            {
                MessageBox.Show("Lütfen Silinecek Kişiyi Seçin");
            }
        }
        private void MesaiKisiEkle()
        {
            try
            {
                baglanti.Open();
                Form2.komut = new OleDbCommand();
                Form2.komut.Connection = baglanti;
                sql = "Insert into [MesaiSay] ([BİLGİ İŞLEM PERSONEL LİSTESİ]) values('" + textBox1.Text + "')";
                string sql2 = "Insert into [MesaiAta] ([BİLGİ İŞLEM PERSONEL LİSTESİ]) values('" + textBox1.Text + "')";
                Form2.komut.CommandText = Form1.sql;
                Form2.komut.ExecuteNonQuery();
                Form2.komut.CommandText = sql2;
                Form2.komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Ekleme Başarı ile Gerçekleştirildi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kişi Ekleme Durumunu Veritabanına Kaydetme Aşamasında Oluşan Hata" + " " + ex.Message);
            }

        }
        private void MesaiKisiSil()
       {
           try
           {
               baglanti.Open();
                Form2.komut = new OleDbCommand();
                Form2.komut.Connection = baglanti;
               sql = "DELETE FROM [MesaiSay] WHERE [BİLGİ İŞLEM PERSONEL LİSTESİ] ='" + dataGridView1.SelectedCells[0].Value.ToString()+ "'";
               string sql2 = "DELETE FROM [MesaiAta] WHERE [BİLGİ İŞLEM PERSONEL LİSTESİ] ='" + dataGridView1.SelectedCells[0].Value.ToString() + "'";
               Form2.komut.CommandText = sql;
               Form2.komut.ExecuteNonQuery();
               Form2.komut.CommandText = sql2;
               Form2.komut.ExecuteNonQuery();
               baglanti.Close();
               MessageBox.Show("Kişi Silme Başarı ile Gerçekleştirildi");

           }
           catch (Exception ex)
           {

               MessageBox.Show("Kişi Silme Durumunu Veritabanına Kaydetme Aşamasında Oluşan Hata" + " " + ex.Message);
           }

       }
        private void MesaiTakipExceliOkuma()
        {
            try
            {
                baglanti.Open();
                dt = new DataTable();
                dt.Clear();
                sql = "SELECT * FROM [MesaiSay]";
                dataAdaptor = new OleDbDataAdapter(sql, baglanti);
                dataAdaptor.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesai Takip Veritabanını Okuma da Oluşan Hata" + " " + ex.Message);

            }
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0&&dataGridView1[0,e.RowIndex].Value.ToString()!="")
            {
                tıkSatir = e.RowIndex;
                frm2 = new Form2();
                frm2.textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                frm1DataGridview = dataGridView1;
                frm1DataGridview.Visible = false;
                frm2.MdiParent = this;
                frm2.Show();
            }
        }
        
        private void MesaiAtamaExceliKontrol()
        {
            frm2 = new Form2();
            frm2.MesaiAtamaExceliOkuma();
            for (i = 0; i < frm2.dataGridView1.RowCount; i++)
            {
                for (j = 1; j < frm2.dataGridView1.ColumnCount; j++)
                {
                    if (frm2.dataGridView1[j, i].Value.ToString().Equals("10"))
                    {
                        dataGridView1[j, i].Style.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
