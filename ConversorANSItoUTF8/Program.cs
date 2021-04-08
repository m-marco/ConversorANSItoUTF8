﻿using System;
using System.IO;
using System.Text;

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
                //string antigoNome = arquivos[i].Name;
                //string novoNome = antigoNome.ToUpper().Replace(".TXT", "") + "-UTF-8.txt";
                string conteudo = File.ReadAllText(arquivos[i].FullName, Encoding.Latin1);

                StreamWriter swFromFileTrueUTF8Buffer = new StreamWriter(arquivos[i].FullName, false, new UTF8Encoding(false), 512);

                swFromFileTrueUTF8Buffer.Write(conteudo);
                swFromFileTrueUTF8Buffer.Flush();
                swFromFileTrueUTF8Buffer.Close();
            }
        }
    }
}
