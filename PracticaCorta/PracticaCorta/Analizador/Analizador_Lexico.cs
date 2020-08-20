using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace PracticaCorta.Analizador
{
    class Analizador_Lexico
    {
        private String palabra;
        //Buscaremos de primero separar la cadena de texto en numerosas palabras
        public Analizador_Lexico(String palabra)
        {
            this.palabra = palabra;
        }

        public String[] AnalizarPalabra()
        {
            String[] palabras = new String[0];
            String palabraAnalizada = "";
            //Agregamos la variable booleana para que solo permita el paso de un espacio en el texto, si hay mas de un espacio
            //lo omitira y seguira hasta avanzar al siguiente espacio, hallar un nuevo caracter y volver a ser analizado
            Boolean analizado = false;
            for(int indexPalabra = 0; indexPalabra < palabra.Length; indexPalabra++)
            {
                if (palabra[indexPalabra] != ' ')
                {
                    palabraAnalizada += palabra[indexPalabra];
                    analizado = true;
                }
                if(palabra[indexPalabra] == ' ' && analizado == true || indexPalabra == (palabra.Length-1))
                {
                    palabras = this.AddPalabra(palabras, palabraAnalizada);
                    palabraAnalizada = "";
                    analizado = false;
                }
            }

            return palabras;
        } 

        
        private String[] AddPalabra(String[] palabras, String palabra)
        {

            try
            {
                String[] palabrasAuxiliares = new String[palabras.Length + 1];

                for (int indexPalabras = 0; indexPalabras < palabras.Length; indexPalabras++)
                {
                    palabrasAuxiliares[indexPalabras] = palabras[indexPalabras];
                }
                palabrasAuxiliares[palabrasAuxiliares.Length - 1] = palabra;

                return palabrasAuxiliares;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show("ERROR LÉXICO: NO SE HA PODIDO ANALIZAR LA PALABRA O NÚMERO \n" + 
                    "ERROR: " + error.Message);
                return palabras;
            }
            
        }
    }
}
