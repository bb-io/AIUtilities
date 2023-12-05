using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts.Models.Request;

public class TranslationRequest : PostEditMtRequest
{
    [Display("Source language")] public string? SourceLanguage { get; set; }

    [Display("Target language")] public string? TargetLanguage { get; set; }
}