using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.AIUtilities.Models.Request.Prompts;

public class TextRequest
{
    public string? Text { get; set; }
    
    [Display("Text file")]
    public File? TextFile { get; set; }
}