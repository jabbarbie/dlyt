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
            
            return attr;
        }
        public string convertFilename(string str1)
        {
            //return Regex.Replace(str1, "[^a-zA-Z]", "");
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
        public void cekUrl()
        {

        }

        public void cekPlaylist()
        {

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
