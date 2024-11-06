namespace TPR.MVC.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string?> UploadImage(IFormFile file, string uploadDirectory)
        {
            if (file == null || file.Length == 0) return null;

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
        public static void DeleteImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
