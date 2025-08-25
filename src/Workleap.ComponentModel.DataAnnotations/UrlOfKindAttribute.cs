using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class UrlOfKindAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The {{0}} field must be a well-formatted {0} URL";
    internal const string RelativeKind = "relative";
    internal const string AbsoluteKind = "absolute";
    private const string RelativeOrAbsoluteKind = RelativeKind + " or " + AbsoluteKind;

    public UrlOfKindAttribute(UriKind kind)
        : base(() => ErrorMessageFormatAccessor(kind))
    {
        this.Kind = kind;
    }

    public UriKind Kind { get; }

    private static string ErrorMessageFormatAccessor(UriKind kind) => kind switch
    {
        UriKind.Absolute => string.Format(CultureInfo.InvariantCulture, ErrorMessageFormat, AbsoluteKind),
        UriKind.Relative => string.Format(CultureInfo.InvariantCulture, ErrorMessageFormat, RelativeKind),
        UriKind.RelativeOrAbsolute => string.Format(CultureInfo.InvariantCulture, ErrorMessageFormat, RelativeOrAbsoluteKind),
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
    };

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        Uri valueAsUri => Uri.TryCreate(valueAsUri.OriginalString, this.Kind, out _),
        string valueAsString => Uri.TryCreate(valueAsString, this.Kind, out _),
        _ => false,
    };
}