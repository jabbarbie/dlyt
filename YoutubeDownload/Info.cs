using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

using System.Text.RegularExpressions;

namespace YoutubeDownload
{
    // dapatkan link
    // cek jenis url valid atau tidak
    // cek fdirectory exist atau tidak
    // cek jenis video playlist atau single link
    // cek apakah video sudah pernah didownload atau belum / exist or not
    // proses download

    public class attributeVideo
    {
        public string Title { get; set; }
        public string Chanel { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string Dislike { get; set; }
        public string Like { get; set; }
        public string View { get; set; }
        public string Thumbnail { get; set; }
        public string DateUpload { get; set; }
        public string namaFile { get; set; }
    }
    public class Info 
    {

        public bool isPlaylist { get; set; }
        public string rootDirectory = Path.Combine("E:","Youtube");
        public string link { get; set; }
        public YoutubeClient yt;
        public Info(string links)
        {
            link = links;
        }

        public async Task<attributeVideo> getInfo ()
        {
            yt = new YoutubeClient();
            var video = await yt.Videos.GetAsync(link);

            attributeVideo attr = new attributeVideo();

            attr.Title = video.Title;
            attr.Chanel = video.Author;
            attr.Duration = Math.Round((video.Duration.TotalMinutes), 1).ToString();
            attr.Rating = video.Engagement.AverageRating.ToString();
            attr.Dislike = video.Engagement.DislikeCount.ToString();
            attr.Like = video.Engagement.LikeCount.ToString();
            attr.View = video.Engagement.ViewCount.ToString();
            attr.Thumbnail = video.Thumbnails.HighResUrl.ToString();
            attr.DateUpload = video.UploadDate.DateTime.ToString("dddd, dd MMMM yyyy");
//            attr.namaFile = convertFilename(video.Title);
//            attr.namaFile = "sate.mp4";
            return attr;
        }
        public async Task getTanyaSize()
//        public async Task getTanyaSize()
        {
            /* StreamManifest.cs
            YoutubeExplode.Videos.Streams:
                StreamManifest:
                    Streams 
                    GetAudio
                    GetVideo
                    GetMuxed
                    GetAudioOnly
                    GetVideoOnly

            */
            yt = new YoutubeClient();

            var test = await yt.Videos.GetAsync(link);
            var dataStream = await yt.Videos.Streams.GetManifestAsync(link);

            // var video = streamManifest.GetMuxed().WithHighestVideoQuality();

            //var video = dataStream.GetVideoOnly().WithHighestVideoQuality();



            // var video = streamManifest.GetVideoOnly().WithHighestVideoQuality();
            // var audio = streamManifest.GetAudioOnly().WithHighestBitrate();

            double[] daftar = new double[2];
            daftar[0] = dataStream.GetMuxed().WithHighestVideoQuality().Size.TotalMegaBytes;
            daftar[1] = dataStream.GetVideo().WithHighestVideoQuality().Size.TotalMegaBytes;
            //daftar[2] = dataStream.GetVideoOnly().WithHighestBitrate().Size.TotalMegaBytes;

            // Console.WriteLine("==================");     
            // Console.WriteLine("Kualitas Video :");       

            for(int i = 1; i <= daftar.Length; i++){
                Console.WriteLine($"{i} - {Math.Round(daftar[i-1])}MB");
            }
            Console.WriteLine("-------------");
            // foreach (double i in daftar)
            // {
            //     Console.WriteLine($"- {Math.Round(i)}MB");
            // }
            // Console.WriteLine("==================");            
            Console.Write("Pilih kualitas video di atas (1 atau 2) :");

            int pilih = int.Parse(Console.ReadLine()); 

            //if (pilih == 1) return dataStream.GetMuxed().WithHighestVideoQuality();
            //else if (pilih == 2) return dataStream.GetVideo().WithHighestVideoQuality();
            //else return null;

            // Console.WriteLine("----------------------------------");
            //Console.WriteLine($"+ 1. {video.Resolution} -  {video.Size}");
            //Console.WriteLine();
            //var x = dataStream.GetVideo().
            //var daftar = dataStream.GetVideoOnly().GetAllVideoQualityLabels();

            //Console.WriteLine($"+ 2. {streamManifest.GetVideoOnly().WithHighestBitrate()} -  {streamManifest.GetVideoOnly().WithHighestBitrate().Size}");
            // Console.WriteLine("----------------------------------");

            // Console.Write("# Pilih Size (1 atau 2)? ");
            // Int16 pilih = Int16.Parse( Console.ReadLine() );
            // Console.WriteLine($"Audio {audio.Size} - {audio.Bitrate} ");


            // Console.WriteLine("pilihan " + pilih.ToString());
            // if (pilih == 1) 
            //     return x;
            // else 
            //     return audio;


        }
        public string convertFilename(string str1)
        {
            return Regex.Replace(str1, "[^a-zA-Z]", "");
            //return str1.Trim(new Char[] { ' ', '*', '.', '-', '#', '\', ',' , '^' ,' }) ;
            Regex reg = new Regex("[*'\",_)(&#^@]");
            //Regex reg = new Regex("[a - zA - Z0 - 9]");


            str1 = reg.Replace(str1, string.Empty);
            //return str1;
//            Regex reg1 = new Regex("[ ]");
            
            return str1.Trim(new Char[] { '-','?' });
        }
        public void cekDirectory(string dirx)
        {
            if (!Directory.Exists(dirx))
            {
                Directory.CreateDirectory(dirx);
            }
        }
        public async Task downloadBaru(IStreamInfo streamInfo, attributeVideo attr)
        {
            if (streamInfo == null)
            {
                Console.Error.WriteLine("This videos has no streams");
            }

            // Compose file name, based on metadata
            string fileName = attr.namaFile;
            string folder = Path.Combine(rootDirectory, attr.Chanel);
            Console.WriteLine("nama filenya " + folder + " dan " + fileName );
            string lokasi = Path.Combine(folder, fileName);

            cekDirectory(folder);

            Console.WriteLine(lokasi);

            Console.Write("sedang mengunduh - ");

            using (var progress = new ProgressBar())
                await yt.Videos.Streams.DownloadAsync(streamInfo, lokasi, progress);


            Console.WriteLine($"video disimipan di '{lokasi}'");
        }
        public async Task downloadVideo(attributeVideo attr)
        {
            var streams = await yt.Videos.Streams.GetManifestAsync(link);

            //           var streams = await yt.Videos.Streams.GetManifestAsync(videoId);

            var streamInfo = streams.GetMuxed().WithHighestVideoQuality();

            //var streamInfo = video.GetMuxed().WithHighestVideoQuality();
            if (streamInfo == null)
            {
                Console.Error.WriteLine("This videos has no streams");
            }

            // Compose file name, based on metadata
            string fileName = attr.namaFile;
            string folder = Path.Combine(rootDirectory, attr.Chanel);
            string lokasi = Path.Combine(folder, fileName);
            
            cekDirectory(folder);

            Console.WriteLine(lokasi);

            Console.Write("sedang mengunduh - ");
            
            using (var progress = new ProgressBar())
                await yt.Videos.Streams.DownloadAsync(streamInfo, lokasi, progress);


            Console.WriteLine($"video disimipan di '{lokasi}'");

        }



    }
}
