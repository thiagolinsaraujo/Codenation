using System;

namespace Criptografia
{
    class Program
    {
        public static void Main(string[] args)
        {
            CriptografiaService criptografia = new CriptografiaService();
            DecriptEncript decriptEncript = new DecriptEncript();

            decriptEncript = criptografia.GetJson();
            decriptEncript.DecriptCriptografia();
            decriptEncript.EncriptCriptogragia();
            decriptEncript.CriptografiaSHA1();

            criptografia.CreateFile(decriptEncript);
            criptografia.SendFile();
        }
    }
}
