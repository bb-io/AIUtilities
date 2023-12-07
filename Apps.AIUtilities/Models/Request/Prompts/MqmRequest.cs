using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts.Models.Request.Prompts;

public class MqmRequest : TranslationRequest
{
    [Display("Target audience")] public string? TargetAudience { get; set; }
}