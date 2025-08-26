using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ContainsNonEmptyGuidAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The field {0} must be an collection that contains at least one non-empty GUID";

    public ContainsNonEmptyGuidAttribute() : base(ErrorMessageFormat)
    {
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        IEnumerable<Guid> enumerable => enumerable.Any(this.IsValidGuid),
        IEnumerable<Guid?> enumerable => enumerable.Any(x => x != null && this.IsValidGuid(x.Value)),
        IEnumerable<string> enumerable => enumerable.Any(this.IsValidGuid),
        _ => false,
    };

    private bool IsValidGuid(string valueAsString)
    {
        return Guid.TryParse(valueAsString, out var guid) && this.IsValidGuid(guid);
    }

    private bool IsValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}