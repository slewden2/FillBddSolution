using System;

namespace FillDbLibrary
{
  public interface IRandomNumber
  {
    double Next(double min, double max);

    DateTime Next(DateTime min, DateTime max);

    long Next(long min, long max);

    Guid Next();
  }
}
