using Lab.MVC.AppSemTemplate.Controllers;
using Lab.MVC.AppSemTemplate.Data;
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

            var controller = new ProdutosController(ctx)
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
    }
}
