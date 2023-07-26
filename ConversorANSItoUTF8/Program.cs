using System;
using System.IO;
using System.Text;
using Ude;

namespace ConversorANSItoUTF8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PATH:");
            string path = Console.ReadLine();
            ConverterArquivosParaUTF8(path);
        }

        private static void ConverterArquivosParaUTF8(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@path);
            FileInfo[] arquivos = dirInfo.GetFiles();

            DirectoryInfo[] diretorios = dirInfo.GetDirectories();

            foreach (var diretorio in diretorios)
            {
                ConverterArquivosParaUTF8(diretorio.FullName);
            }

            for (int i = 0; i < arquivos.Length; i++)
            {
                var encoding = GetFileEncoding(arquivos[i].FullName);
                if (encoding == Encoding.ASCII)
                {
                    string conteudo = File.ReadAllText(arquivos[i].FullName, Encoding.Latin1);
                    StreamWriter swFromFileTrueUTF8Buffer = new StreamWriter(arquivos[i].FullName, false, new UTF8Encoding(false), 512);

                    swFromFileTrueUTF8Buffer.Write(conteudo);
                    swFromFileTrueUTF8Buffer.Flush();
                    swFromFileTrueUTF8Buffer.Close();

                    Console.WriteLine("The encoding used was {0}.", encoding);
                }
            }
        }

        public static Encoding GetFileEncoding(string srcFile)
        {
            byte[] buffer = File.ReadAllBytes(srcFile);
            CharsetDetector detector = new CharsetDetector();
            detector.Feed(buffer, 0, buffer.Length);
            detector.DataEnd();
            string encodingName = detector.Charset;
            if (encodingName == "windows-1252")
            {
                return Encoding.ASCII;
            }
            try
            {
            return Encoding.GetEncoding(encodingName);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
