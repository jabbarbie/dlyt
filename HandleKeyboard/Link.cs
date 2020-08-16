using System;
using System.Threading.Tasks;
using TextCopy;

namespace HandleKeyboard
{
    public class attributeLink{
        public string link;
        public string hostname;
        public bool isYoutube;
        public bool isPlaylist;

    }
    public class Link
    {
        public async Task<string> Paste()
        {
            // return ClipboardService.GetText()? ClipboardService.GetText() : '';
            return await ClipboardService.GetTextAsync();
        }

        public attributeLink getInfo(string link)
        {
            attributeLink attr = new attributeLink();
            Uri alamat = new Uri(link);
            string[] alamatsegment = alamat.Segments;

            attr.hostname = alamat.Host;
            // cek hostname
            switch (alamat.Host)
            {
                case "www.youtube.com":
                    attr.isYoutube = true; 
                break;
                default:
                    attr.isYoutube = false;
                    break;
            }

            return attr;
        } 

        public bool isLink(string link)
        {
            Uri buatlink = null;
            Uri.TryCreate(link, UriKind.Absolute, out buatlink);

            if (buatlink == null)                
                return false;                
            
            return true;
        }
    }
}
