namespace Project_PL_.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1 - need location folder path
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2 - file name must be Unique

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3 - get file path

            string filepath = Path.Combine(folderpath, fileName);

            // 4 - save file as stream

            using var filestram = new FileStream(filepath, FileMode.Create);

            file.CopyTo(filestram);

            return fileName;
        }

        // 2 - Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            string pathName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/file", folderName, fileName);

            if (File.Exists(pathName))
                File.Delete(pathName);
        }
    }
}
