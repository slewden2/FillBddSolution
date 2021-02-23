using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class GuidGeneratorTests
  {
    public static IEnumerable<object[]> Data()
    {
      var guid1 = Guid.NewGuid();
      var guid2 = Guid.NewGuid();
      return new List<object[]> 
      { 
        new object[] { guid1, $"'{{{guid1}}}'" },
        new object[] { guid2, $"'{{{guid2}}}'" }
      };
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void GenerateWorks(Guid value, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      var x = random.Setup(x => x.Next()).Returns(value);


      var sut = new GuidGenerator(random.Object);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
