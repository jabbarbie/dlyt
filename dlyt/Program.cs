using System;
using System.Threading.Tasks;
using System.IO;

using YoutubeDownload;

/*
 TODO: 
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
            Console.ResetColor();
            Program main = new Program();
            
            // Cek Keyboard 
            var clipboard = new HandleKeyboard.Link();
            string paste = await clipboard.Paste();
            if (!clipboard.isLink(paste)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Link Salah");
                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(0);
            }
            var getInfo = clipboard.getInfo(paste);         
            if (getInfo.hostname != "www.youtube.com"){ return; }

            // get info video - youtube
            Info info = new Info(paste);
            attributeVideo youtubeInfo = await info.getInfo();

            // Tampilkan Info Video
            main.showInfo(youtubeInfo);
            await info.getTanyaSize();

            // var TanyaSize = await info.getTanyaSize();
            //return;
            // await info.downloadBaru(TanyaSize, youtubeInfo);

        }

        public void garis(string kata)
        {
            for (Int16 i = 0; i < (kata.Length + 14); i++) {
                
                if (i >= 53) break;
                Console.Write("-");
            };
            Console.WriteLine();
        }
        public void showInfo(attributeVideo video)
        {
            
            garis(video.Title);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"| Judul   : {video.Title} |");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"| Chanel  : {video.Chanel} \n");
            Console.ResetColor();

            Console.WriteLine($"| Durasi  : {video.Duration} Menit");
            Console.WriteLine($"| Like    : {video.Like} - Dislike : {video.Dislike}");
            Console.WriteLine($"| View    : {video.View}");
            Console.WriteLine($"| Upload  : {video.DateUpload}");
            garis(video.Title);


        }
    }
}
