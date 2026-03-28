namespace Lab.Dev.TestDemoApp.UnitTests
{
    public class CalculadoraTestes
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            var calc = new Calculadora();

            //Act
            var resultado = calc.Somar(10, 10);

            //Assert
            Assert.Equal(20, resultado);
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(4, 4, 8)]
        [InlineData(8, 9, 17)]
        [InlineData(7, 3, 10)]
        [InlineData(9, 9, 18)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double x, double y, double resultadoEsperado)
        {
            //Arrange
            var calc = new Calculadora();

            //Act
            var resultado = calc.Somar(x, y);

            //Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

    }
}
