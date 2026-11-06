using System;
using System.Security.Cryptography;

namespace HumanResourcesManagement
{
    public static class SeguridadContrasenas
    {
        private const int Iteraciones = 120000;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        public static string CrearHash(string contrasena)
        {
            byte[] salt = new byte[TamanoSalt];

            using (var generador = RandomNumberGenerator.Create())
            {
                generador.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                contrasena, salt, Iteraciones))
            {
                byte[] hash = pbkdf2.GetBytes(TamanoHash);

                return "PBKDF2$" + Iteraciones + "$" +
                    Convert.ToBase64String(salt) + "$" +
                    Convert.ToBase64String(hash);
            }
        }

        public static bool Verificar(string contrasena, string hashGuardado)
        {
            try
            {
                string[] partes = hashGuardado.Split('$');

                if (partes.Length != 4 || partes[0] != "PBKDF2")
                    return false;

                int iteraciones = int.Parse(partes[1]);
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashOriginal = Convert.FromBase64String(partes[3]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    contrasena, salt, iteraciones))
                {
                    byte[] hashCalculado = pbkdf2.GetBytes(hashOriginal.Length);

                    int diferencia = 0;

                    for (int i = 0; i < hashOriginal.Length; i++)
                        diferencia |= hashOriginal[i] ^ hashCalculado[i];

                    return diferencia == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool EsHash(string valor)
        {
            return !string.IsNullOrEmpty(valor) &&
                   valor.StartsWith("PBKDF2$",
                       StringComparison.Ordinal);
        }
    }
}