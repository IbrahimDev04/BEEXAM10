namespace BEExam10.Extensions
{
    public static class ManageFile
    {
        public static bool IsValidType(this IFormFile formFile, string type)
            => formFile.ContentType.Contains(type);

        public static bool IsValidSize(this IFormFile formFile, int KByte)
            => formFile.Length <= 1024*KByte;

        public async static Task<string> ManageFileSave(this IFormFile formFile, string path)
        {
            var fileName = formFile.FileName;

            fileName = Guid.NewGuid().ToString() + fileName;

            FileStream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            return fileName;
        }

        public async static Task Delete(this string formFile, string path)
        {
            File.Delete(Path.Combine(path , formFile));
        }
    }
}
