using Lab.MVC.AppSemTemplate.Services.Contracts;

namespace Lab.MVC.AppSemTemplate.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public async Task<Tuple<bool, string>> UploadArquivo(IFormFile arquivo, string prefixo)
        {
            if (arquivo == null || arquivo.Length <= 0) return Tuple.Create(false, string.Empty);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{prefixo}{arquivo.FileName}");

            using var stream = new FileStream(path, FileMode.Create);
            await arquivo.CopyToAsync(stream);

            return Tuple.Create(true, path);
        }
    }
}
