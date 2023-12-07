using Blackbird.Applications.Sdk.Common;

namespace Apps.AIUtilities.Models.Request.Prompts;

public class TranslationRequest : PostEditMtRequest
{
    [Display("Source language")] public string? SourceLanguage { get; set; }

    [Display("Target language")] public string? TargetLanguage { get; set; }
}