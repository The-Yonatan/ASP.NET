using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models;

public class ReleaseYearValidation :ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var releaseYear = Convert.ToDateTime(value);
        return releaseYear <= DateTime.Now && releaseYear >= DateTime.Now.AddDays(-1095);
    }
}



