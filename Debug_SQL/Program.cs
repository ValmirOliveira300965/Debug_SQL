using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Windows.Forms;

/*
   Para chamar um determinado formulário, é necessário especificar a camada (namespace) do formulário a ser chamado.
   Exemplo:
     using CamadaApresentacao;

   Depois, alterar a linha para a chamada do formulário.
   Exemplo:
      Application.Run(new formCategoria());
*/

namespace Debug_SQL
{
    internal static class Program
    {
        static public string startProces = "NORMAL";

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if(args.Length > 0)  // O executável tem parâmetro(s)?
            {
                // Zerar, Reiniciar ou Iniciar
                if (args[0].Substring(0, 1).ToUpper().Trim() == "Z" ||
                    args[0].Substring(0, 1).ToUpper().Trim() == "R" ||
                    args[0].Substring(0, 1).ToUpper().Trim() == "I")
                {
                    string configScreen = Path.Combine(FormDebug.localArquivos, FormDebug.configScreen);

                    if (File.Exists(configScreen))
                    {
                        File.Delete(configScreen);
                    }
                }


                if (args[0].Substring(0, 1).ToUpper().Trim() == "T")
                {
                    startProces = args[0].ToUpper().Trim();
                }
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormDebug());
            } catch (Exception ex) { }
        }
    }
}
