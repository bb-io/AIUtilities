using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.BlackbirdPrompts.Models.Request;

public class PostEditMtRequest
{
    [Display("Source text")]
    public string? SourceText { get; set; }
    
    [Display("Source text file")]
    public File? SourceTextFile { get; set; }
    
    [Display("Target text")]
    public string? TargetText { get; set; }
    
    [Display("Target text file")]
    public File? TargetTextFile { get; set; }
    
    [Display("Additional prompt")]
    public string? AdditionalPrompt { get; set; }
}