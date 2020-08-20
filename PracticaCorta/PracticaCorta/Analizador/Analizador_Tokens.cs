using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaCorta.Analizador
{
    class Analizador_Tokens
    {
        private String[] palabras = new String[0];
        private String[] lexemas = new String[0];
        private String[] tokens = new String[0];
        public Analizador_Tokens(String palabra)
        {
            Analizador_Lexico analizadorLexico = new Analizador_Lexico(palabra);
            palabras = analizadorLexico.AnalizarPalabra();
        }

        public String[] GetTokens()
        {
            
            try
            {
                Tokens analizador = new Tokens();
                char letraAuxiliar = ' ';
                String token = "";
                String tokenAnalizado = "";
                String atributo = "";
                String atributoAnalizado = "";
                String palabraAnalizada = "";
                //El string tokenAnalizado nos ayudara a saber a que tipo de token nos dirigimos y esperamos
                int valorEnteros = 0;
                Boolean indicadorMoneda = false;
                for(int indexPalabras =0; indexPalabras < palabras.Length; indexPalabras++)
                {
                    letraAuxiliar = ' ';
                    tokenAnalizado = "";
                    token = "";
                    valorEnteros = 0;
                    atributo = "";
                    atributoAnalizado = "";
                    palabraAnalizada = palabras[indexPalabras];
                    for (int indexPalabra = 0; indexPalabra < palabraAnalizada.Length; indexPalabra++)
                    {
                        
                        //Obtenemos de primero el string y luego el caracter
                        letraAuxiliar = palabras[indexPalabras][indexPalabra];
                        atributo += Char.ToString(letraAuxiliar);
                        switch (tokenAnalizado)
                        {
                            case "":
                                token = analizador.checkNull(letraAuxiliar);
                                if(indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    tokens = this.addToken(tokens, token);
                                    lexemas = this.addToken(lexemas, atributo);
                                }
                                break;

                            case "ENTERO":
                                token = analizador.checkNumber(letraAuxiliar);

                                if (token.Equals("") || indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    if(indexPalabra != (palabraAnalizada.Length - 1) || token.Equals(""))
                                    {
                                        indexPalabra--;
                                        atributo = atributoAnalizado;
                                    }
                                        

                                    tokens = this.addToken(tokens, tokenAnalizado);
                                    lexemas = this.addToken(lexemas, atributo);
                                    atributo = "";
                                    
                                    
                                }

                                break;

                            case "DECIMAL":
                                token = analizador.checkDecimal(letraAuxiliar);

                               

                                //Marcado que no es decimal
                                if (token.Equals("") || indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    //Si nos retorna aqui hay dos opciones con token, o tiene valor "" o es cero 
                                    //Si es cero lo seguiremos considerando un decimal
                                    //Si no es cero y los valoresEnteros no es cero se considerara que es un decimal y avanzara al siguiente token
                                    //Si no es cero y los valoresEnteros es cero se considerara un entero y avanzara al siguiente token
                                    if (letraAuxiliar == '0')
                                    {
                                        token = "DECIMAL";
                                    }
                                    else if (letraAuxiliar != '0' && valorEnteros != 0)
                                    {
                                        token = "DECIMAL";
                                        tokens = this.addToken(tokens, token);
                                        if(indexPalabra != (palabraAnalizada.Length - 1))
                                        {
                                            indexPalabra--;
                                            atributo = atributoAnalizado;
                                        }
                                            

                                        valorEnteros = 0;
                                        token = "";
                                        lexemas = this.addToken(lexemas, atributo);
                                        atributo = "";
                                    }
                                    else if (letraAuxiliar != '0' && valorEnteros == 0)
                                    {
                                        token = "ENTERO";
                                        tokens = this.addToken(tokens, token);

                                        if (indexPalabra != (palabraAnalizada.Length - 1))
                                        {
                                            indexPalabra--;
                                            atributo = atributoAnalizado;
                                        }
                                            

                                        valorEnteros = 0;
                                        token = "";
                                        lexemas = this.addToken(lexemas, atributo);
                                        atributo = "";
                                    }
                                }
                                else
                                    valorEnteros++;
                                //Esta sentencia permite reconocer el cero cuando ya se halla terminado de leer la expresion
                                if (letraAuxiliar == '0' && indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    //Depende de cuantos valores halla recorrido permitira saber si se queda como decimal o ENTERO
                                    if (valorEnteros == 0)
                                        token = "ENTERO";
                                    else
                                        token = "DECIMAL";

                                    tokens = this.addToken(tokens, token);
                                    valorEnteros = 0;
                                    token = "";
                                    lexemas = this.addToken(lexemas, atributo);
                                    atributo = "";
                                }
                                break;

                            case "PALABRA":
                                token = analizador.checkPalabra(letraAuxiliar);

                                if (token.Equals("") || indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    if (indexPalabra != (palabraAnalizada.Length - 1) || token.Equals(""))
                                    {
                                        indexPalabra--;
                                        atributo = atributoAnalizado;
                                    }
                                        
                                    tokens = this.addToken(tokens, tokenAnalizado);
                                    lexemas = this.addToken(lexemas, atributo);
                                    atributo = "";
                                }

                                break;

                            case "MONEDA":
                                token = analizador.checkMoneda(letraAuxiliar);

                                //Esta condicion nos permite saber que si toca una palabra que coloque los atributos anteriores, 
                                //y abra un nuevo token para esta palabra
                                if (token.Equals("PALABRA"))
                                {
                                    indexPalabra--;
                                    tokens = this.addToken(tokens, tokenAnalizado);
                                    lexemas = this.addToken(lexemas, atributoAnalizado);
                                    token = "";
                                    atributo = "";
                                }
                                //Comprobaremos si es decimal, estas condiciones nos permiten saber si se mantiene como un entero o decimal
                                if (token.Equals("DECIMAL") )
                                {
                                    if (indicadorMoneda == false)
                                    {
                                        indicadorMoneda = true;
                                        token = "MONEDA";
                                    }
                                    else
                                    {
                                        indicadorMoneda = false;
                                        indexPalabra--;
                                        tokens = this.addToken(tokens, tokenAnalizado);
                                        lexemas = this.addToken(lexemas, atributoAnalizado);
                                        token = "";
                                        atributo = "";
                                    }
                                }else if(indexPalabra == (palabraAnalizada.Length - 1))
                                {
                                    indicadorMoneda = false;
                                    tokens = this.addToken(tokens, tokenAnalizado);
                                    lexemas = this.addToken(lexemas, atributo);
                                    token = "";
                                    atributo = "";
                                }
                                break;

                            
                        }
                        tokenAnalizado = token;
                        atributoAnalizado = atributo;
                    }
                }


                return tokens;
            }
            catch(Exception error)
            {
                System.Windows.Forms.MessageBox.Show("ERROR LÉXICO: NO SE HA PODIDO ANALIZAR LA PALABRA O NÚMERO PARA TRANSFORMARLA EN TOKEN \n" +
                   "ERROR: " + error.Message);
                return new String[0];
            }
            
        }

        public String[] GetLexemas()
        {
            return lexemas;
        }
        private String[] addToken(String[] token, String newToken)
        {
            try
            {
                String[] tokenAuxiliar = new string[token.Length + 1];
                for (int indexToken = 0; indexToken < token.Length; indexToken++)
                {
                    tokenAuxiliar[indexToken] = token[indexToken];
                }
                tokenAuxiliar[tokenAuxiliar.Length - 1] = newToken;

                return tokenAuxiliar;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ERROR LÉXICO: NO SE HA PODIDO INTRODUCIR EL TOKEN \n" +
                    "ERROR: " + ex.Message);
                return token;
            }
        }

       
    }

    
}
