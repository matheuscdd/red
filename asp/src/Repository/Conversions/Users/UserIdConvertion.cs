using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repository.Conversions.Users;

public class UpperCaseStringConverter : ValueConverter<string, string>
{
    public UpperCaseStringConverter()
        : base(v => v.ToUpper(), v => v) { }
}
