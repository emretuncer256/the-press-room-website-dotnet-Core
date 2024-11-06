namespace TPR.MVC.Enums
{
    public static class UploadPaths
    {
        public static readonly string Base = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");

        public static readonly string Authors = Path.Combine(Base, "Authors");
        public static readonly string AuthorThumbnails = Path.Combine(Authors, "Thumbnails");
    }
}
