using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace MESAİ_TAKİP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static OleDbCommand komut;
        string tarih;
        string[] pTarih = new string[4];
        Boolean ozelgun = false;
        
        int kon = 0, bHucre1, bHucre2, dHucre, say = 0;
        private void Form2_Load(object sender, EventArgs e)
        {           
            MesaiAtamaExceliOkuma();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.dt.Clear();
            Form1.frm1DataGridview.Visible = true;
           
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            tarih = e.Start.ToLongDateString();
            pTarih = tarih.Split(' ');
            comboBox1.Items.Clear();
            if (pTarih[3] == "Cuma")
            {
                comboBox1.Items.Add("24:00--08:00");
                label2.Visible = true;
                comboBox1.Visible = true;
            }
            else
            if (pTarih[3] == "Cumartesi" || OzelGün() == true)
            {              
                comboBox1.Items.Add("08:00--16:00");
                comboBox1.Items.Add("16:00--24:00");
                comboBox1.Items.Add("24:00--08:00");
                label2.Visible = true;
                comboBox1.Visible = true;

            }
            else
           if (pTarih[3] == "Pazar")
            {
                comboBox1.Items.Add("08:00--16:00");
                comboBox1.Items.Add("16:00--24:00");
                label2.Visible = true;
                comboBox1.Visible = true;
            }
            else
            {
                comboBox1.Items.Clear();
                MessageBox.Show("Lütfen Mesai İçeren Gün Seçiniz");
            }
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                label3.Visible = true;
                comboBox2.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Mesai Ata")
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                button2.Visible = false;
                button1.Visible = true;
            }
            else
            if (comboBox2.Text == "Mesai Onayla")
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                button1.Visible = false;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Visible = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OzelGün() == true || pTarih[3] == "Cumartesi" || pTarih[3] == "Pazar"||pTarih[3]=="Cuma")
            {
                if (ozelgun == true)
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 7;
                        bHucre1 = 6;
                        bHucre2 = 8;
                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 8;
                        bHucre1 = 7;
                        bHucre2 = 9;
                    }
                    else
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 9;
                        bHucre1 = 8;
                        bHucre2 = -1;
                    }
                }
                else
                if (pTarih[3] == "Cuma")
                {
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 1;
                        bHucre1 = 2;
                        bHucre2 = -1;
                    }
                }
                else
                if (pTarih[3] == "Cumartesi")
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 2;
                        bHucre1 = 1;
                        bHucre2 = 3;

                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 3;
                        bHucre1 = 2;
                        bHucre2 = 4;
                    }
                    else
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 4;
                        bHucre1 = 3;
                        bHucre2 = 5;
                    }
                }
                else
                if (pTarih[3] == "Pazar")
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 5;
                        bHucre1 = 4;
                        bHucre2 = 6;
                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 6;
                        bHucre1 = 5;
                        bHucre2 = 7;
                    }

                }
                try
                {
                   // MesaiAtamaExceliOkuma();        
                    if (dataGridView1[bHucre1, Form1.tıkSatir].Value.ToString() != "10")
                    {
                        if (bHucre2 == -1)
                        {
                            if (dataGridView1[dHucre, Form1.tıkSatir].Value.ToString() != "10")
                            {
                                dataGridView1[dHucre,Form1.tıkSatir].Value = 10;
                                Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Style.BackColor = Color.Red;
                                MesaiAtamaExceliAta(); 
                               
                            }
                            else
                            {
                                MessageBox.Show("Mesai Atamak İstediğiniz" + " " + dataGridView1[0, Form1.tıkSatir].Value.ToString() + " " + "İsimli Kişide Bu Mesai Mevcuttur. ");
                            }
                        }
                        else
                        if (dataGridView1[bHucre2, Form1.tıkSatir].Value.ToString() != "10")
                        {
                            if (dataGridView1[dHucre, Form1.tıkSatir].Value.ToString() != "10")
                            {
                                dataGridView1[dHucre,Form1.tıkSatir].Value = 10;
                                Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Style.BackColor = Color.Red;
                                MesaiAtamaExceliAta(); 
                            
                            }
                            else
                            {
                                MessageBox.Show("Mesai Atamak İstediğiniz" + " " + dataGridView1[0, Form1.tıkSatir].Value.ToString() + " " + "İsimli Kişide Bu Mesai Mevcuttur. ");
                            }
                        }
                        else
                        {
                            MessageBox.Show("" + dataGridView1[0, Form1.tıkSatir].Value.ToString() + " " + "Adlı Kişiye Art Arda İki Mesaiyiye Atayamazsınız.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("" + dataGridView1[0, Form1.tıkSatir].Value.ToString() + " " + "Adlı Kişiye Art Arda İki Mesaiyiye Atayamazsınız.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Mesai Atama aşamasında Oluşan Hata" + " " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Lütfen Mesai İçeren Bir Tarih Seçiniz");
            }
        }

        public void MesaiAtamaExceliOkuma()//Mesai Atama Exceli Okuma
        {
            try
            {
                Form1.baglanti.Open();      
                Form1.dt = new DataTable();               
                Form1.sql = "SELECT * FROM [MesaiAta]";
                OleDbDataAdapter dataAdaptor = new OleDbDataAdapter(Form1.sql, Form1.baglanti);                 
                dataAdaptor.Fill(Form1.dt);
                dataGridView1.DataSource = Form1.dt.DefaultView;
                Form1.baglanti.Close();

            }
            catch (Exception ex)
            {
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Atama Veritabanını Okumada Oluşan Hata" + " " + ex.Message);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OzelGün() == true || pTarih[3] == "Cumartesi" || pTarih[3] == "Pazar"|| pTarih[3] == "Cuma")
            {             
                if (ozelgun == true)
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 7;
                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 8;
                    }
                    else
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 9;
                    }
                }
                else
                 if (pTarih[3] == "Cuma")
                {
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 1;                      
                    }
                }
                else
                if (pTarih[3] == "Cumartesi")
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 2;
                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 3;
                    }
                    else
                    if (comboBox1.Text == "24:00--08:00")
                    {
                        dHucre = 4;
                    }
                }
                else
                if (pTarih[3] == "Pazar")
                {
                    if (comboBox1.Text == "08:00--16:00")
                    {
                        dHucre = 5;
                    }
                    else
                    if (comboBox1.Text == "16:00--24:00")
                    {
                        dHucre = 6;
                    }

                }
                try
                {
                    //MesaiAtamaExceliOkuma();
                    if (dataGridView1[dHucre, Form1.tıkSatir].Value.ToString().Equals("10"))///dataGridView1.Rows[Form1.tıkSatir].Cells[sHucre].Value.ToString()=="10"
                    {
                        if (radioButton1.Checked == true)
                        {
                            if (Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Value.ToString() == "")
                            {
                                say = 0;
                            }
                            else
                            {
                                say = Convert.ToInt32(Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Value.ToString());
                            }
                            say += 1;
                            Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Value = say;
                            MesaiOnaylamaSonucuMesaiSayExcelAta();
                        }
                        dataGridView1[dHucre, Form1.tıkSatir].Value = 0;
                        Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Style.BackColor = Color.White;
                        MesaiOnaylamaSonucuMesaiAtaExcelAta();
                    }
                    else
                    {
                        MessageBox.Show("Onaylama İşlemi yapmak İstediğiniz Kişide Mesai Mevcut Değildir");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Onaylama İşleminde Gerçekleşen Hata"+" "+ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show("Lütfen Mesai İçeren Bir Tarih Seçiniz");
            }
        }
        
        void MesaiAtamaExceliAta()//Mesai Atama İşlemi
        {
            try
            {
                Form1.baglanti.Open();
                Form2.komut = new OleDbCommand();
                komut.Connection = Form1.baglanti;
                Form1.sql = "Update  [MesaiAta] set [" + dataGridView1.Columns[dHucre].Name.ToString() + "]= '" + dataGridView1[dHucre, Form1.tıkSatir].Value.ToString() + "'   WHERE [BİLGİ İŞLEM PERSONEL LİSTESİ]='" + dataGridView1[0, Form1.tıkSatir].Value.ToString() + "'";
                komut.CommandText = Form1.sql;
                komut.ExecuteNonQuery();
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Atama İşleminiz Başarı ile Gerçekleştirildi.");
                this.Close();
            }
            catch (Exception hat1)
            {
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Atama Veritabanına Mesai Atamada Oluşan Hata" + " " + hat1.Message);

            }
        }
        
        void MesaiOnaylamaSonucuMesaiSayExcelAta()//Mesai onay sonucunu mesaisay exceline yazma işlemi
        {
            try
            {
                Form1.baglanti.Open();
                komut = new OleDbCommand();
                komut.Connection = Form1.baglanti;
                Form1.sql = "Update  [MesaiSay] set [" + Form1.frm1DataGridview.Columns[dHucre].Name.ToString() + "]= '" + Form1.frm1DataGridview[dHucre, Form1.tıkSatir].Value.ToString() + "'   WHERE [BİLGİ İŞLEM PERSONEL LİSTESİ]='" + Form1.frm1DataGridview[0, Form1.tıkSatir].Value.ToString() + "'";
                komut.CommandText = Form1.sql;
                komut.ExecuteNonQuery();
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Onaylama İşleminiz Başarı ile Gerçekleştirildi.");

            }
            catch (Exception hat1)
            {
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Onaylama İşlemini Mesai Say Veritabanına  Yazmada Oluşan Hata" + " " + hat1.Message);

            }
        }
        void MesaiOnaylamaSonucuMesaiAtaExcelAta()//Mesai onay sonucunu MesaiAta exceline yazma işlemi
        {
            try
            {
                Form1.baglanti.Open();
                Form2.komut = new OleDbCommand();
                komut.Connection = Form1.baglanti;
                Form1.sql = "Update  [MesaiAta] set [" + dataGridView1.Columns[dHucre].Name.ToString() + "]= " + dataGridView1[dHucre, Form1.tıkSatir].Value.ToString() + "   WHERE [BİLGİ İŞLEM PERSONEL LİSTESİ]='" + dataGridView1[0, Form1.tıkSatir].Value.ToString() + "'";
                komut.CommandText = Form1.sql;
                komut.ExecuteNonQuery();
                Form1.baglanti.Close();
                this.Close();
            }
            catch (Exception hat1)
            {
                Form1.baglanti.Close();
                MessageBox.Show("Mesai Onaylama İşlemini Mesai Ata Veritabanına Yazmada Oluşan Hata" + " " + hat1.Message);

            }
        }
        bool OzelGün()// Resmi ve Dini Tatil Kontrolü
        {
            if (monthCalendar1.BoldedDates.Contains(monthCalendar1.SelectionStart))
            {
                ozelgun = true;
            }
            else
            {
                ozelgun = false;
            }
            return ozelgun;
        }
    }
}
