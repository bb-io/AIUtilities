using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.AIUtilities.Models.Request.Prompts;

public class PostEditMtRequest
{
    [Display("Source text")]
    public string? SourceText { get; set; }
    
    [Display("Source text file")]
    public FileReference? SourceTextFile { get; set; }
    
    [Display("Target text")]
    public string? TargetText { get; set; }
    
    [Display("Target text file")]
    public FileReference? TargetTextFile { get; set; }
    
    [Display("Additional prompt")]
    public string? AdditionalPrompt { get; set; }
}