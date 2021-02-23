using FillDbLibrary;
using FillDbLibrary.Implementation.Generator;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FillDbLibraryTests.Implementation.Generator
{
  public class DateTimeGeneratorTests
  {

    public static IEnumerable<object[]> ValidDates =>
      new List<object[]>
      {
        new object[]{10, 0, new DateTime(1987, 5, 7), "'1987-05-07'"},
        new object[]{16, 4, new DateTime(1901, 10, 1, 13, 46, 00), "'1901-10-01T13:46:00'"},
        new object[]{16, 7, new DateTime(1999, 10, 1, 17, 6, 10, 40), "'17:06:10.0400000'"},
        new object[]{23, 0, new DateTime(2021, 2, 22, 0, 59, 17, 1), "'2021-02-22T00:59:17.0010000'"},
        new object[]{27, 0, new DateTime(2000, 2, 22, 0, 59, 17, 0), "'2000-02-22T00:59:17.0000000'"},
      };
    public static IEnumerable<object[]> ValidDatesWithOffset =>
      new List<object[]>
      {
        new object[]{new DateTime(1901, 10, 1, 13, 46, 00), new TimeSpan(14, 0, 0), false,"'1901-10-01 13:46:00.0000000 -14:00'"},
        new object[]{new DateTime(1999, 10, 1, 17, 6, 10, 40), new TimeSpan(3, 17, 0), false, "'1999-10-01 17:06:10.0400000 -03:17'"},
        new object[]{new DateTime(2021, 2, 22, 0, 59, 17, 1), new TimeSpan(7, 59, 0), true, "'2021-02-22 00:59:17.0010000 +07:59'"},
        new object[]{new DateTime(2000, 2, 22, 0, 59, 17, 0), new TimeSpan(13, 17, 0), true , "'2000-02-22 00:59:17.0000000 +13:17'"},
      };


    [Theory]
    [MemberData(nameof(ValidDates))]
    public void GenerateWorks(int precision, int maxLength, DateTime value, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      random.Setup(x => x.Next(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(value);

      var sut = new DateTimeGenerator(random.Object, precision, maxLength);

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }


    [Theory]
    [MemberData(nameof(ValidDatesWithOffset))]
    public void GenerateWorksWithOffset(DateTime value, TimeSpan offset, bool offsetPositive, string expected)
    {
      // Arrange
      var random = new Mock<IRandomNumber>();
      random.Setup(x => x.Next(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(value);
      random.Setup(x => x.Next(new DateTime(1, 1, 1), new DateTime(1, 1, 1, 14, 0, 0))).Returns(new DateTime(1, 1, 1) + offset);
      random.Setup(x => x.Next(0, 1)).Returns(offsetPositive ? 1 : 0);

      var sut = new DateTimeGenerator(random.Object, 34, 10); // DateTimeOffset

      // Act
      var result = sut.Generate();

      // assert
      Assert.Equal(expected, result);
    }
  }
}
