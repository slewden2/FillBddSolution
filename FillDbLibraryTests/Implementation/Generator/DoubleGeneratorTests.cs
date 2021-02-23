using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class DoubleGeneratorTests
  {
    [Theory]
    [InlineData(0D, "0")]
    [InlineData(1.1, "1.1")]
    [InlineData(-5.893, "-5.893")]
    public void GenerateWorks(double value, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      var x = random.Setup(x => x.Next(double.MinValue, double.MaxValue)).Returns(value);
      

      var sut = new DoubleGenerator(random.Object);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
