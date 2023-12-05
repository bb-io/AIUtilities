using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts.Models.Response;

public record SystemAndUserPromptResponse(
    [property: Display("System prompt")] string SystemPrompt,
    [property: Display("User prompt")] string UserPrompt);