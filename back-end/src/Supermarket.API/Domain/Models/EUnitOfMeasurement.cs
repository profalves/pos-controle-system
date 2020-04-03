using System.ComponentModel;

namespace Supermarket.API.Domain.Models
{
  public enum EUnitOfMeasurement : byte
  {
    [Description("un")]
    Unity = 1,

    [Description("mg")]
    Milligram = 2,

    [Description("gr")]
    Gram = 3,

    [Description("kg")]
    Kilogram = 4,

    [Description("lt")]
    Liter = 5
  }
}