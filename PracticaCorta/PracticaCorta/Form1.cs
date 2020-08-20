using PracticaCorta.Analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaCorta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") == false)
            {
                Analizador_Tokens analizador_Tokens = new Analizador_Tokens(textBox1.Text);
                String[] tokens = analizador_Tokens.GetTokens();
                String[] atributos = analizador_Tokens.GetLexemas();

                if(tokens != null && atributos != null)
                {
                    textBox2.Clear();
                    for (int index = 0; index < tokens.Length; index++)
                    {
                        textBox2.AppendText("TOKENS: "+tokens[index]+ " -----> LEXEMA: "+atributos[index]+"\r\n");
                    }
                }
                

            }
            else
            {
                MessageBox.Show("PORFAVOR INTRODUZCA UNA ORACION");
            }
        }
    }
}
