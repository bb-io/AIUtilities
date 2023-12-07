using Blackbird.Applications.Sdk.Common;

namespace Apps.AIUtilities.Models.Request.Prompts;

public class MqmRequest : TranslationRequest
{
    [Display("Target audience")] public string? TargetAudience { get; set; }
}