using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using System;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class BaseGeneratorTests
  {
    [Fact]
    public void ConstructorWithNullArgumentFail()
    {
      // Arrange & Act & Assert
      Assert.Throws<ArgumentNullException>(() => new DummyGenerator(null));
    }

    [Fact]
    public void ConstructorWithIRandomNumberWorks()
    {
      // Arrange & Act & Assert
      _ = new DummyGenerator(Mock.Of<IRandomNumber>());
    }

    private class DummyGenerator : BaseGenerator
    {
      public DummyGenerator(IRandomNumber rnd)
        :base(rnd)
      {
      }
      public override string Generate() => "dummy";
    }
  }
}
