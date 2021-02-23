using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class LongGeneratorTests
  {
    public static IEnumerable<object[]> ValidLong =>
      new List<object[]>
      {
        new object[]{3, 1, "1"},
        new object[]{5,-1, "-1"},
        new object[]{10, 100, "100"},
        new object[]{19, 200, "200"},
      };
    

    [Theory]
    [MemberData(nameof(ValidLong))]
    public void GenerateWorks(int precision, long value, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      random.Setup(x => x.Next(It.IsAny<long>(), It.IsAny<long>())).Returns(value);

      var sut = new LongGenerator(random.Object, precision);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
