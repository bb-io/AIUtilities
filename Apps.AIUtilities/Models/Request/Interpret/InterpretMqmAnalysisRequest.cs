using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts.Models.Request.Interpret;

public class InterpretMqmAnalysisRequest
{
    [Display("AI MQM analysis response")]
    public string ResponseContent { get; set; }
}