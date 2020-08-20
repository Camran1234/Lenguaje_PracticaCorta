using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaCorta.Analizador
{
    class Tokens
    {
        public string checkNull(char letra)
        {
            if (comprobarNumero(letra))
                return "NUMERO";
            else if (comprobarDecimal(letra))
                return "DECIMAL";
            else if (comprobarPalabra(letra))
            {
                if(letra == 'Q')
                {
                    return "MONEDA";
                }else
                    return "PALABRA";
            }
                

            return "";

        }

        public string checkNumber(char letra)
        {
            //Al haber analizado que es numero solo hay dos opciones, sigue siendo un entero o puede ser un decimal
            if (comprobarNumero(letra))
                return "NUMERO";
            else if (comprobarDecimal(letra))
                return "DECIMAL";

            return "";
        }

        internal string checkDecimal(char letraAuxiliar)
        {
            //Al ya haber analizado el punto solo puede encontrar numero, si encuentra cualquier otro valor consideramos que ya no es decimal
            if (comprobarNumeroParaDecimal(letraAuxiliar))
                return "DECIMAL";


            return "";
        }

        internal string checkPalabra(char letraAuxiliar)
        {
            if (comprobarPalabra(letraAuxiliar))
            {
                if (letraAuxiliar == 'Q')
                {
                    return "MONEDA";
                }
                else
                    return "PALABRA";
            }

            return "";
        }

        internal string checkMoneda(char letraAuxiliar)
        {
            if (comprobarPalabra(letraAuxiliar))
                return "PALABRA";
            else if (comprobarNumero(letraAuxiliar))
                return "MONEDA";
            else if (comprobarDecimal(letraAuxiliar))
                return "DECIMAL";
            return "PALABRA";
        }

        private Boolean comprobarNumero(char letra)
        {
            if (letra == '1' || letra == '2' || letra == '3' || letra == '4' || letra == '5' || letra == '6' || letra == '7'
                || letra == '8' || letra == '9' || letra == '0')
            {
                return true;
            }
            return false;
        }

        private Boolean comprobarNumeroParaDecimal(char letra)
        {
            if (letra == '1' || letra == '2' || letra == '3' || letra == '4' || letra == '5' || letra == '6' || letra == '7'
                || letra == '8' || letra == '9')
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

        private Boolean comprobarPalabra(char letra)
        {
            if ((letra >= 'A' && letra <= 'Z') || (letra >= 'a' && letra <= 'z'))
                    return true;
            return false;
        }

        
    }
}
