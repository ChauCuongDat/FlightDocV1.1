using Microsoft.AspNetCore.Mvc;

namespace FlightDocV1._1.FileHandler
{
    public class FileHandler
    {
        public static async Task<String> SaveFile (IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileHandler", "File", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
