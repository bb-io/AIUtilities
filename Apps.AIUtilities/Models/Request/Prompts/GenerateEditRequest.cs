using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.AIUtilities.Models.Request.Prompts;

public class GenerateEditRequest
{
    public string? Text { get; set; }
    
    [Display("Text file")]
    public FileReference? TextFile { get; set; }
    
    public string Instructions { get; set; }
}