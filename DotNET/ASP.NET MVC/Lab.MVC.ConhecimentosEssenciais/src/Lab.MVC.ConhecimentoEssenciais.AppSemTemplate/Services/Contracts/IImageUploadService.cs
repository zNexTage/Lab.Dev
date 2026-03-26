namespace Lab.MVC.AppSemTemplate.Services.Contracts
{
    public interface IImageUploadService
    {
        public Task<Tuple<bool, string>> UploadArquivo(IFormFile arquivo, string prefixo);
    }
}
