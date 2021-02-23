using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class BitGeneratorTests
  {
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    public void GenerateWorks(int value, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      random.Setup(x => x.Next(0, 1)).Returns(value);

      var sut = new BitGenerator(random.Object);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
