using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Criptografia
{
    public class DecriptEncript
    {
        public int Numero_Casas { get; set; }
        public string Token { get; set; }
        public string Cifrado { get; set; }
        public string Decifrado { get; set; }
        public string Resumo_Criptografico { get; set; }

        private const int letraInicial = 97;
        private const int letraFinal = 122;

        public void DecriptCriptografia()
        {
            string decifrado = "";
            for (int i = 0; i < Cifrado.Length; i++)
            {
                char[] letra = Cifrado.Substring(i, 1).ToCharArray();
                int letraMinima = letraInicial + Numero_Casas;
                int valorLetra = letra[0];
                if (valorLetra >= letraInicial && valorLetra <= letraFinal)
                {
                    if (valorLetra < letraMinima)
                    {
                        valorLetra = letraFinal + 1 - (letraMinima - valorLetra);
                    }
                    else
                    {
                        valorLetra -= Numero_Casas;
                    }
                }
                decifrado += Char.ConvertFromUtf32(valorLetra);
            }
            Decifrado = decifrado;
        }

        public void EncriptCriptogragia()
        {
            string cifrado = "";
            for (int i = 0; i < Decifrado.Length; i++)
            {
                char[] letra = Decifrado.Substring(i, 1).ToCharArray();
                int letraMaxima = letraFinal - Numero_Casas;
                int valorLetra = letra[0];
                if (valorLetra >= letraInicial && valorLetra <= letraFinal)
                {
                    if (valorLetra > letraMaxima)
                    {
                        valorLetra = letraInicial - 1 + (valorLetra - letraMaxima);
                    }
                    else
                    {
                        valorLetra += Numero_Casas;
                    }
                }
                cifrado += Char.ConvertFromUtf32(valorLetra);
            }
            Cifrado = cifrado;
        }

        public void CriptografiaSHA1()
        {
            byte[] buffer = Encoding.Default.GetBytes(Decifrado);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            Resumo_Criptografico = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
}
