using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace kart2
{
    public partial class Form1 : Form
    {
        List<Button> kartlar;
        Button birinciSecim = null;
        Button ikinciSecim = null;
        int xal = 0;
        int tapilanCutler = 0;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kartlar = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8 };
            OyunuBaslat();
        }

        private void OyunuBaslat()
        {
            xal = 0;
            tapilanCutler = 0;
            birinciSecim = null;
            ikinciSecim = null;
            label1.Text = "Xal: 0";
            label2.Text = "";

            List<Color> rengler = new List<Color>
            {
                Color.Red, Color.Red,
                Color.Blue, Color.Blue,
                Color.Yellow, Color.Yellow,
                Color.Green, Color.Green
            };

            rengler = rengler.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < kartlar.Count; i++)
            {
                kartlar[i].BackColor = Color.LightGray;
                kartlar[i].Tag = rengler[i];
                kartlar[i].Enabled = true;
            }
        }

        private void Kart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) return;

            Button kliklenenKart = sender as Button;

            if (kliklenenKart == null || kliklenenKart == birinciSecim) return;

            kliklenenKart.BackColor = (Color)kliklenenKart.Tag;

            if (birinciSecim == null)
            {
                birinciSecim = kliklenenKart;
                return;
            }

            ikinciSecim = kliklenenKart;

            if ((Color)birinciSecim.Tag == (Color)ikinciSecim.Tag)
            {
                xal += 10;
                label1.Text = "Xal: " + xal;
                label2.ForeColor = Color.Green;
                label2.Text = "✅";

                birinciSecim.Enabled = false;
                ikinciSecim.Enabled = false;

                birinciSecim = null;
                ikinciSecim = null;
                tapilanCutler++;

                if (tapilanCutler == 4)
                {
                    MessageBox.Show($"Təbriklər! Bütün kartları tapdınız.\nToplam Xal: {xal}", "Oyun Bitdi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "❌";
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (birinciSecim != null) birinciSecim.BackColor = Color.LightGray;
            if (ikinciSecim != null) ikinciSecim.BackColor = Color.LightGray;

            birinciSecim = null;
            ikinciSecim = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            OyunuBaslat();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}