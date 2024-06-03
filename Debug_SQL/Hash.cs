using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Debug_SQL
{
    /// Enumerator com os tipos de classes para criação de Hash.
    /// </summary>

    public enum HashProvider
    {
        /// Computa um hash SHA1 hash para os dados.
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        MD5
    }

    /// Classe auxiliar com métodos para crição de hash dos dados inseridos.
    public class Hash
    {
        #region Private members

        private HashAlgorithm _algorithm = null;

        #endregion
        #region Constructors

        /// Contrutor padrão da classe, é setado um tipo de hash padrão.
        public Hash()
        {
            _algorithm = new SHA1Managed();
        }

        /// Construtor com o tipo de hash a ser usado.
        /// <param name="hashProvider">Tipo de Hash.</param>
        public Hash(HashProvider hashProvider)
        {
            switch (hashProvider)
            {
                case HashProvider.MD5:
                    _algorithm = new MD5CryptoServiceProvider();
                    break;

                case HashProvider.SHA1:
                    _algorithm = new SHA1Managed();
                    break;

                case HashProvider.SHA256:
                    _algorithm = new SHA256Managed();
                    break;

                case HashProvider.SHA384:
                    _algorithm = new SHA384Managed();
                    break;

                case HashProvider.SHA512:
                    _algorithm = new SHA512Managed();
                    break;
            }
        }

        #endregion
        #region Public methods

        /// Monta hash para algum dado texto.
        /// <param name="plainText">Texto a ser criado o hash.</param>
        /// <returns>Hash do texto inserido.</returns>
        public string GetHash(string plainText)
        {
            byte[] cryptoByte = _algorithm.ComputeHash(ASCIIEncoding.UTF8.GetBytes(plainText));
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
        }

        /// Cria hash para algum tipo de stream.
        /// <param name="fileStream">Stream a ser criado o hash.</param>
        /// <returns>Hash do stream inserido.</returns>
        public string GetHash(FileStream fileStream)
        {
            byte[] cryptoByte;
            cryptoByte = _algorithm.ComputeHash(fileStream);
            fileStream.Close();

            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
        }

        #endregion
    }
}
