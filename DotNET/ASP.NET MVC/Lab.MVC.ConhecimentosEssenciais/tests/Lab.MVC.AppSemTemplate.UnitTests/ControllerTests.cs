using Lab.MVC.AppSemTemplate.Controllers;
using Lab.MVC.AppSemTemplate.Data;
using Lab.MVC.AppSemTemplate.Models;
using Lab.MVC.AppSemTemplate.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace Lab.MVC.AppSemTemplate.UnitTests
{
    public class ControllerTests
    {
        /// <summary>
        /// Testando a controller TesteController
        /// action Index
        /// deve ocorrer com sucesso
        /// </summary>
        [Fact]
        public void TesteController_Index_Sucesso()
        {
            // Arrange
            var controller = new TesteController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ProdutosController_Index_Sucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var ctx = new AppDbContext(options);

            ctx.Produto.Add(new Models.Produto() { Id = 1, Name = "Prod 1", Valor = 10m });
            ctx.Produto.Add(new Models.Produto() { Id = 2, Name = "Prod 2", Valor = 10m });
            ctx.Produto.Add(new Models.Produto() { Id = 3, Name = "Prod 3", Valor = 10m });
            ctx.SaveChanges();

            // Identity
            var mockClaims = new Mock<ClaimsIdentity>();
            mockClaims
                .Setup(m => m.Name)
                .Returns("teste@email.com");

            var principal = new ClaimsPrincipal(mockClaims.Object);

            var mockContext = new Mock<HttpContext>();
            mockContext
                .Setup(c => c.User)
                .Returns(principal);

            //IFormFile
            var fileMock = new Mock<IFormFile>();
            var content = "Dados do seu arquivo fake (mock)";
            var fileName = "test.jpg";

            var ms = new MemoryStream();
            var writter = new StreamWriter(ms);
            writter.Write(content);
            writter.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns($"inline; filename={fileName}");

            // Img service
            var imgService = new Mock<IImageUploadService>();
            imgService
                .Setup(i =>
                i.UploadArquivo(fileMock.Object,
                It.IsAny<string>()))
                .ReturnsAsync(Tuple.Create(true, ""));

            var controller = new ProdutosController(ctx, imgService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockContext.Object
                }
            };

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ProdutosController_Create_Sucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var ctx = new AppDbContext(options);

            //IFormFile
            var fileMock = new Mock<IFormFile>();
            var content = "Dados do seu arquivo fake (mock)";
            var fileName = "test.jpg";

            var ms = new MemoryStream();
            var writter = new StreamWriter(ms);
            writter.Write(content);
            writter.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns($"inline; filename={fileName}");
            
            // Img service
            var imgService = new Mock<IImageUploadService>();
            imgService
                .Setup(i =>
                i.UploadArquivo(fileMock.Object,
                It.IsAny<string>()))
                .ReturnsAsync(Tuple.Create(true, ""));

            var controller = new ProdutosController(ctx, imgService.Object);

            Produto produto = new Produto()
            {
                Id = 1,
                Name = "test",
                Valor = 10m,
                Upload = fileMock.Object,
                Image = string.Empty
            };

            // Act
            var result = await controller.Create(produto);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
