using System.Text;
using Apps.AIUtilities.Constants;
using Apps.AIUtilities.Enums;
using Apps.AIUtilities.Models.Request.Prompts;
using Apps.AIUtilities.Models.Response.Prompts;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.AIUtilities.Actions;

[ActionList]
public class PromptActions
{
    private readonly IFileManagementClient _fileManagementClient;

    public PromptActions(IFileManagementClient fileManagementClient)
    {
        _fileManagementClient = fileManagementClient;
    }

    private const string PromptSeparator = ";;";

    [Action("Summary prompt", Description = "Get prompt for summarizing text")]
    public async Task<PromptResponse> Summary([ActionParameter] TextRequest input)
    {
        var promptText = await BuildPromptFromInputs(input.Text, input.TextFile) ??
                         throw new("Both Text and File inputs can't be empty");

        return new(string.Format(Prompts.Summary, promptText));
    }

    [Action("Generate edit prompt", Description = "Get prompt for editing the input text given an instructions")]
    public async Task<PromptResponse> GenerateEdit([ActionParameter] GenerateEditRequest input)
    {
        var promptText = await BuildPromptFromInputs(input.Text, input.TextFile) ??
                         throw new("Both Text and File inputs can't be empty");

        var systemPrompt = Prompts.GenerateEditSystem;
        var userPrompt = string.Format(Prompts.GenerateEditUser, promptText, input.Instructions);

        return new(string.Join(PromptSeparator, systemPrompt, userPrompt));
    }

    [Action("Post-edit MT prompt",
        Description = "Get prompt for reviewing MT translated text and generating a post-edited version")]
    public async Task<PromptResponse> PostEditMt([ActionParameter] PostEditMtRequest input,
        [ActionParameter] GlossaryRequest glossary)
    {
        var systemPrompt = input.AdditionalPrompt is null 
            ? Prompts.PostEditMtSystem 
            : $"{Prompts.PostEditMtSystem} {input.AdditionalPrompt}";

        if (glossary.Glossary != null)
            systemPrompt = $"{systemPrompt} {Prompts.PostEditMtGlossarySystem}";

        var sourceTextPrompt = await BuildPromptFromInputs(input.SourceText, input.SourceTextFile) ??
                               throw new("Both Source text and Source text file inputs can't be empty");

        var targetTextPrompt = await BuildPromptFromInputs(input.TargetText, input.TargetTextFile) ??
                               throw new("Both Target text and Target text file inputs can't be empty");

        var glossaryPrompt = glossary.Glossary != null
            ? await GetGlossaryPromptPart(glossary.Glossary)
            : string.Empty;
        
        var userPrompt = string.Format(Prompts.TranslationReview, sourceTextPrompt, targetTextPrompt, glossaryPrompt);
        return new(string.Join(PromptSeparator, systemPrompt, userPrompt));
    }

    [Action("Find translation issues prompt",
        Description = "Get prompt for reviewing text translation and generating a comment with the issue description")]
    public async Task<PromptResponse> FindTranslationIssues([ActionParameter] TranslationRequest input,
        [ActionParameter] GlossaryRequest glossary)
    {
        var sourceLanguagePart = input.SourceLanguage != null ? $"written in {input.SourceLanguage} " : string.Empty;
        var targetLanguagePart = input.TargetLanguage != null ? $"written in {input.TargetLanguage}" : string.Empty;
        var systemPrompt = string.Format(Prompts.FindTranslationIssuesSystem, sourceLanguagePart, targetLanguagePart);

        if (input.AdditionalPrompt != null)
            systemPrompt = $"{systemPrompt} {input.AdditionalPrompt}";
        
        if (glossary.Glossary != null)
            systemPrompt = $"{systemPrompt} {Prompts.FindTranslationIssuesGlossarySystem}";

        var sourceTextPrompt = await BuildPromptFromInputs(input.SourceText, input.SourceTextFile) ??
                               throw new("Both Source text and Source text file inputs can't be empty");

        var targetTextPrompt = await BuildPromptFromInputs(input.TargetText, input.TargetTextFile) ??
                               throw new("Both Target text and Target text file inputs can't be empty");

        var glossaryPrompt = glossary.Glossary != null
            ? await GetGlossaryPromptPart(glossary.Glossary)
            : string.Empty;

        var userPrompt = string.Format(Prompts.TranslationReview, sourceTextPrompt, targetTextPrompt, glossaryPrompt);
        return new(string.Join(PromptSeparator, systemPrompt, userPrompt));
    }

    [Action("MQM report prompt",
        Description =
            "Get prompt for performing an LQA Analysis of the translation. The result will be in the MQM framework form.")]
    public Task<PromptResponse> MqmReport([ActionParameter] MqmRequest input, 
        [ActionParameter] GlossaryRequest glossary)
        => GetMqmPrompt(input, Prompts.MqmReportSystem, glossary.Glossary);

    [Action("MQM dimension values prompt",
        Description =
            "Get prompt for performing an LQA Analysis of the translation. The result will be in the MQM framework form, namely the scores (between 1 and 10) of each dimension.")]

    public async Task<PromptResponse> MqmDimensionValues([ActionParameter] MqmRequest input, 
        [ActionParameter] GlossaryRequest glossary)
        => new($"{(await GetMqmPrompt(input, Prompts.MqmDimensionValuesSystem, glossary.Glossary)).Prompt}{PromptSeparator}{FileFormat.Json}");

    [Action("Translate prompt", Description = "Get prompt for localizing the provided text")]
    public async Task<PromptResponse> Translate([ActionParameter] TranslateRequest input, 
        [ActionParameter] GlossaryRequest glossary)
    {
        var textPrompt = await BuildPromptFromInputs(input.Text, input.TextFile) ??
                         throw new("Both Text and Text file inputs can't be empty");

        var glossaryPrompt = glossary.Glossary != null
            ? string.Format(Prompts.TranslateGlossaryPart, await GetGlossaryPromptPart(glossary.Glossary))
            : string.Empty;
        
        return new(string.Format(Prompts.Translate, textPrompt, input.Locale, glossaryPrompt));
    }

    [Action("Get localizable content from image prompt",
        Description = "Get prompt for retrieving localizable content from image")]
    public PromptResponse GetLocalizableContentFromImage()
        => new(Prompts.GetLocalizableContentFromImage);

    private async Task<string?> BuildPromptFromInputs(string? text, FileReference? textFile)
    {
        if (text is null && textFile is null)
            return null;

        var promptTextParts = new List<string>();

        if (text is not null)
            promptTextParts.Add(text);

        if (textFile is not null)
        {
            var fileStream = await _fileManagementClient.DownloadAsync(textFile);
            var fileBytes = await fileStream.GetByteData();

            promptTextParts.Add(Encoding.UTF8.GetString(fileBytes));
        }

        return string.Join(" ", promptTextParts);
    }

    private async Task<PromptResponse> GetMqmPrompt(MqmRequest input, string systemPromptPart, FileReference? glossary)
    {
        var systemPrompt = input.AdditionalPrompt is null
            ? systemPromptPart
            : $"{systemPromptPart} {input.AdditionalPrompt}";

        if (glossary != null)
            systemPrompt = $"{systemPrompt} {Prompts.MqmGlossarySystem}";

        var sourceTextPrompt = await BuildPromptFromInputs(input.SourceText, input.SourceTextFile) ??
                               throw new("Both Source text and Source text file inputs can't be empty");

        var targetTextPrompt = await BuildPromptFromInputs(input.TargetText, input.TargetTextFile) ??
                               throw new("Both Target text and Target text file inputs can't be empty");

        var sourceLanguagePrompt = input.SourceLanguage != null ? $"The {input.SourceLanguage} " : string.Empty;
        var targetLanguagePrompt = input.TargetLanguage != null ? $" into {input.TargetLanguage}" : string.Empty;
        var targetAudiencePrompt = input.TargetAudience != null
            ? $" The target audience is {input.TargetAudience}"
            : string.Empty;
        var glossaryPrompt = glossary != null
            ? await GetGlossaryPromptPart(glossary)
            : string.Empty;

        var userPrompt = string.Format(Prompts.MqmUser, sourceLanguagePrompt, sourceTextPrompt, targetTextPrompt,
            targetLanguagePrompt, targetAudiencePrompt, glossaryPrompt);

        return new(string.Join(PromptSeparator, systemPrompt, userPrompt));
    }
    
    private async Task<string> GetGlossaryPromptPart(FileReference glossary)
    {
        var glossaryStream = await _fileManagementClient.DownloadAsync(glossary);
        var blackbirdGlossary = await glossaryStream.ConvertFromTBX();

        var glossaryPromptPart = new StringBuilder();
        glossaryPromptPart.AppendLine();
        glossaryPromptPart.AppendLine();
        glossaryPromptPart.AppendLine("Glossary entries (each entry includes terms in different language. Each " +
                                      "language may have a few synonymous variations which are separated by |):");

        foreach (var entry in blackbirdGlossary.ConceptEntries)
        {
            glossaryPromptPart.AppendLine();
            glossaryPromptPart.AppendLine("\tEntry:");
                
            foreach (var section in entry.LanguageSections)
            {
                glossaryPromptPart.AppendLine(
                    $"\t\t{section.LanguageCode}: {string.Join('|', section.Terms.Select(term => term.Term))}");
            }
        }

        return glossaryPromptPart.ToString();
    }
}