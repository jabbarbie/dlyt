using System;
using System.Threading.Tasks;
using System.IO;

using YoutubeDownload;
using Keyboardx;

/*
 TODO 

 Fungsi untuk sinkronkan semua file yg di E:/Youtube
    Hapus semua spesial karakter

 */
namespace dlyt
{
    class Program
    {
        public string link { get; set; }
        
        static async Task Main(string[] args)
        {
            //Console.Clear();
            Program main = new Program();
            cekDuplikasi cek = new cekDuplikasi();
            Tanya tanya = new Tanya();

            string currentDir = Environment.CurrentDirectory;

            cek.validasiLink(tanya.PasteX()); // cek apakalah link valid atau tidak

            main.link = tanya.PasteX();


            var info = new Info(main.link);
            var getInfo = await info.getInfo();

            string namaFile = $"{info.convertFilename(getInfo.Title)}.mp4";
            getInfo.namaFile = namaFile;

            //Console.WriteLine($"{namaFile}");

            //Console.WriteLine("sate ",cek.fileSudahAda(namaFile).keterangan.ToString());

            Console.WriteLine(namaFile.ToString());

            var x = cek.fileSudahAda(getInfo.namaFile);
            Console.WriteLine(x.status.ToString(), x.keterangan);
            if (x.status)
            {
                Console.WriteLine($"File Sudah ada {cek.fileSudahAda(namaFile).keterangan }");
                return;
            }



            //if (!cek.fileSudahAda(namaFile))
            //{
            //    Console.WriteLine("File Sudah ada ",namaFile);
            //}

            main.showInfo(getInfo);
            // Console.Title = getInfo.Title;
            //Console.WriteLine(Path.Combine("E:/", info.convertFilename(getInfo.Title)));

            //await info.downloadVideo(getInfo);


            Console.ReadKey();
            return;
        }
        public void garis(string kata)
        {
            for (Int16 i = 0; i < (kata.Length); i++) {
                Console.Write("=");
            };
            Console.WriteLine();
        }
        public void showInfo(attributeVideo video)
        {
            
            garis(video.Title);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Judul   : {video.Title}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" Chanel  : {video.Chanel} \n");
            Console.ResetColor();

            Console.WriteLine($" Durasi  : {video.Duration} Menit");
            Console.WriteLine($" Like    : {video.Like} - Dislike : {video.Dislike}");
            Console.WriteLine($" View    : {video.View}");
            Console.WriteLine($" Upload  : {video.DateUpload}");
            garis(video.Title);


        }
    }
}
