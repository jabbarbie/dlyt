using System;
using TextCopy;

namespace Keyboardx
{
    public class Tanya
    {
        public string PasteX()
        {
            try
            {
                return ClipboardService.GetText();
            }
            catch (Exception e)
            {
                return "kosong";
            }
        }
        public bool isURL(string link)
        {
            try
            {
                if (new Uri(link).IsAbsoluteUri)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
    }
}
