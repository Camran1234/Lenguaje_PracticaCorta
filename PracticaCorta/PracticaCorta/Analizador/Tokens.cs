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
            if (comprobarDecimal(letra))
                return "DECIMAL";
            if (comprobarPalabra(letra))
                return "PALABRA";

            return "";

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
