using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class VariableLengthStringGeneratorTests
  {
    [Theory]
    [InlineData("Bac", true, "N'Bac'")]
    [InlineData("z", false, "'z'")]
    [InlineData("Theorie du cahos", false, "'Theorie du cahos'")]
    public void GenerateWorks(string value, bool unicode, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      random.Setup(x => x.Next(0, value.Length)).Returns(value.Length);
      var x = random.SetupSequence(x => x.Next(0, CharMap.StandardChars.Length));
      for (int i = 0; i < value.Length; i++)
      {
        x = x.Returns(CharMap.StandardChars.IndexOf(value[i]));
      }

      var sut = new VariableLengthStringGenerator(random.Object, value.Length, unicode);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
