using Blackbird.Applications.Sdk.Common;

namespace Apps.AIUtilities.Models.Request.Interpret;

public class InterpretMqmAnalysisRequest
{
    [Display("AI MQM analysis response")]
    public string ResponseContent { get; set; }
}