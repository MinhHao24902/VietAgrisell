using System.Text;

namespace VietAgrisell.Helpers
{
    public class MyUtil
    {
        public static string UploadPicture(IFormFile Pic, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pic", folder, Pic.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Pic.CopyTo(myfile);
                }
                return Pic.FileName;
            } catch(Exception ex)
            {
                return string.Empty;
            }
        }
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRDVTGBYHNUJMIKLOP!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
