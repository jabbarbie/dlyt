using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Keyboardx;

namespace dlyt
{
    public class statusReturn
    {
        public bool status { get; }
        public string keterangan { get; }

        public statusReturn(bool s, string k) {
            status = s;
            keterangan = k;
        }
    }

    class cekDuplikasi
    {
        public short  scanDirectory(string targetDirectory, string namaFile, bool status = false)
        {
            // scan direktori
            
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            string fakeDir = Path.Combine(targetDirectory, namaFile);

            Console.Write(fakeDir);
            if (File.Exists(fakeDir))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ditemukan");
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine($"+ {fakeDir} ");
                return 1;
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    scanDirectory(subdirectory, namaFile);

                return 0;
            }
            //foreach (string fileName in fileEntries)
            //{
            //    if (File.Exists(fileName))
            //    {
            //        Console.WriteLine($"+ {fileName} - True");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"- {fileName} - False");

            //    };
            //}
                //return false;

            //Console.WriteLine(Path.GetFileName(fileName));

            // Recurse into subdirectories of this directory.
            

            //return 0;
        }



        public void validasiLink(string link)
        {
            Tanya tanya = new Tanya();

            if (!tanya.isURL(link))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Link salah");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();
                Environment.Exit(0);
            };
        }
        public statusReturn fileSudahAda(string namaFile)
        {
            //statusReturn response = new statusReturn(false, "");
            string path = Path.Combine("E:", "Youtube");
            string pathFileName = Path.Combine(path, namaFile);

            if (File.Exists(pathFileName))
            {
                Console.WriteLine("File sudah ada 1");
                return new statusReturn(true, "aaaa");
            }
            else if (Directory.Exists(path))
            {
                Console.WriteLine($"Scan ---- {namaFile}");
                Int16 hasil = scanDirectory(path, namaFile);
                Console.WriteLine(hasil);
                // This path is a directory
                if (hasil > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("File ada dan ditemukan ");
                    return new statusReturn(true, "bbbbbb");

                }//return true;
            }

            return new statusReturn(false, "Not Found");
        }
    }
}
