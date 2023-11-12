namespace FlightDocV1._1.SignatureHandler
{
    public class SignatureHandler
    {
        public static async Task<String> SaveFile(IFormFile signature)
        {
            if (signature == null || signature.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "SignatureHandler", "Signature", signature.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await signature.CopyToAsync(stream);
            }
            return path;
        }
    }
}
