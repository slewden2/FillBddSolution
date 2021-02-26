using FillDbLibrary;
using FillDbLibrary.Implementation;
using Moq;
using System;
using Xunit;

namespace FillDbLibraryTests.Implementation
{
  public class TypeGeneratorFactoryTests
  {
    [Fact]
    public void ConstructorWithNullParamFail()
    {
      // Arrange & Act & Assert
      Assert.Throws<ArgumentNullException>(() => new TypeGeneratorFactory(null));
    }

    [Fact]
    public void ConstructorWithIRandomNumberWorks()
    {
      // Arrange & Act & Assert
      _ = new TypeGeneratorFactory(Mock.Of<IRandomNumber>());
    }

    [Theory]
    [InlineData("bit", 0, 0)]
    [InlineData("char", 2, 20)]
    public void GetGeneratorWorks(string typeName, int precision, int maxLength)
    {
      // Arrange 
      var sut = new TypeGeneratorFactory(Mock.Of<IRandomNumber>());

      // Act 
      var result = sut.GetGenerator(typeName, precision, maxLength);

      // Assert
      Assert.NotNull(result);
    }
  }
}
