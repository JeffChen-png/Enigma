using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Энигма_2._0
{
    public partial class Form1 : Form
    {
        string alphab = "ABCDEFGHIJKZMNOPQRSTUVWXYZ";
        string rotor1 = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        string rotor2 = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        string rotor3 = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        string reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        int n;
        Label marked;
        Label[] maliy;

        private void pluse()
        {
            if (vScrollBar3.Value < 25)
                vScrollBar3.Value++;
            else
            {
                vScrollBar3.Value = 0;

                if (vScrollBar2.Value < 25)
                    vScrollBar2.Value++;
                else
                {
                    vScrollBar2.Value = 0;
                    if (vScrollBar1.Value < 25)
                        vScrollBar1.Value++;
                    else
                        vScrollBar1.Value = 0;
                }
            }

        }
        private void vScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(alphab[vScrollBar3.Value]);
        }
        private void vScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(alphab[vScrollBar2.Value]);
        }
        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(alphab[vScrollBar1.Value]);
        }
        private char enigma(int k1, int k2, int k3)
        {
            pluse();

            n = input[input.Length - 1] - 'A';
            int Irot1 = (n + k3) % 26;

            /*----------------------======== 1 РОТОР ========----------------------*/
            n = rotor1[Irot1] - 'A';
            int Irot2 = (n + (k2 + 26 - k3)) % 26;

            /*----------------------======== 2 РОТОР ========----------------------*/
            n = rotor2[Irot2] - 'A';
            int Irot3 = (n + (k1 + 26 - k2)) % 26;

            /*----------------------======== 3 РОТОР ========----------------------*/
            n = rotor3[Irot3] - 'A';
            int Iref = (n + 26 - k1) % 26;

            /*----------------------========  РЕФЛЕКТОР ========----------------------*/
            n = reflector[Iref] - 'A';
            int Irot31 = (n + k1) % 26;

            /*----------------------======== 3 РОТОР ========----------------------*/
            n = rotor3.IndexOf((char)('A' + Irot31));
            int Irot21 = (n + 26 - (k1 + 26 - k2 % 26) % 26) % 26;

            /*----------------------======== 2 РОТОР ========----------------------*/
            n = rotor2.IndexOf((char)('A' + Irot21));
            int Irot11 = (n + 26 - (k2 + 26 - k3) % 26) % 26;

            /*----------------------======== 1 РОТОР ========----------------------*/
            n = rotor1.IndexOf((char)('A' + Irot11));
            int output = (n + 26 - k3) % 26;

            /*----------------------======== ВЫВОД ========----------------------*/
            this.OUTPUTTEXT.Text += alphab[output];

            return alphab[output];

        }
        private void light(char OUTPUTTEXT)
        {
            int index = OUTPUTTEXT - 'A';
            maliy[index].BackColor = Color.LightGoldenrodYellow;
            if (marked != null)
                marked.BackColor = Color.Transparent;
            marked = maliy[index];
        }
        private void comutator()
        {
            bool check1 = true;
            bool check2 = true;
            bool check3 = true;

            if (textBox4.Text != textBox5.Text)
            {
                if (input[input.Length - 1] == textBox4.Text[0])
                {
                    input = textBox5.Text;
                    check1 = false;
                }

                if (input.EndsWith(textBox5.Text) && check1)
                {
                    input = textBox4.Text;
                    check1 = false;
                }
            }

            if (textBox6.Text != textBox7.Text)
            {
                if (input.EndsWith(textBox6.Text))
                {
                    input = textBox7.Text;
                    check2 = false;
                }

                if (input.EndsWith(textBox7.Text) && check2)
                {
                    input = textBox6.Text;
                    check2 = false;
                }
            }

            if (textBox8.Text != textBox9.Text)
            {
                if (input.EndsWith(textBox8.Text))
                {
                    input = textBox9.Text;
                    check3 = false;
                }

                if (input.EndsWith(textBox9.Text) && check3)
                {
                    input = textBox8.Text;
                    check3 = false;
                }
            }
        }
        private void InitializeArray()
        {
            maliy = new Label[] { A, B, C, D, E, F, G, H, I, J, K, Z, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };
        }
        private void Print(object sender, EventArgs e)
        {

            input = input + (sender as Label).Tag;
            input.ToUpper();
            INPUTTEXT.Text = input;
            comutator();
            light(enigma(vScrollBar1.Value, vScrollBar2.Value, vScrollBar3.Value));

        }

        public Form1()
        {
            InitializeComponent();
            InitializeArray();
        }
  
        string input = "";

        private void Help_Click(object sender, EventArgs e)
        {
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox12.Visible = true;
            textBox13.Visible = true;
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            input = "";
            OUTPUTTEXT.Text = input;
            INPUTTEXT.Text = input;
            foreach (Label marked in maliy)
                marked.BackColor = Color.Transparent;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
        }
    }
}
