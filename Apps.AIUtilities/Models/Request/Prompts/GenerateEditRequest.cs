using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.BlackbirdPrompts.Models.Request.Prompts;

public class GenerateEditRequest
{
    public string? Text { get; set; }
    
    [Display("Text file")]
    public File? TextFile { get; set; }
    
    public string Instructions { get; set; }
}