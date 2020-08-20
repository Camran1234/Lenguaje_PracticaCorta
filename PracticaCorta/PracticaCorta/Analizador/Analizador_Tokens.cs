using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaCorta.Analizador
{
    class Analizador_Sintactico
    {
        String[] palabras = new String[0];

        public Analizador_Sintactico(String palabra)
        {
            Analizador_Lexico analizadorLexico = new Analizador_Lexico(palabra);
            palabras = analizadorLexico.AnalizarPalabra();
        }

        public String[] GetTokens()
        {
            String[] tokens = new String[0];
            try
            {
                char palabraAuxiliar = ' ';
                String token = "";
                //El string direccion nos ayudara a saber a que tipo de token nos dirigimos y esperamos
                String direccion = " ";
                for(int indexPalabras =0; indexPalabras < palabras.Length; indexPalabras++)
                {
                    palabraAuxiliar = ' ';
                    for (int indexPalabra = 0; indexPalabra < palabras[indexPalabras].Length; indexPalabra++)
                    {
                        //Obtenemos de primero el string y luego el caracter
                        palabraAuxiliar += palabras[indexPalabras][indexPalabra];

                        if(this.comprobarNumero(palabraAuxiliar))
                        {

                        }

                    }
                }


                return tokens;
            }
            catch(Exception error)
            {
                System.Windows.Forms.MessageBox.Show("ERROR SINTÁCTICO: NO SE HA PODIDO ANALIZAR LA PALABRA O NÚMERO PARA TRANSFORMARLA EN TOKEN \n" +
                   "ERROR: " + error.Message);
                return new String[0];
            }
            
        }

        private Boolean comprobarNumero(char letra)
        {
            if(letra == '1' || letra == '2' || letra == '3' || letra == '4' || letra == '5' || letra == '6' || letra == '7'
                || letra == '8' || letra == '9' || letra == '0')
            {
                return true;
            }
            return false;
        }

        private Boolean comprobarDecimal(char letra)
        {
            if (letra == '.')
            {
                return true;
            }
            return false;
        }
    }

    
}
