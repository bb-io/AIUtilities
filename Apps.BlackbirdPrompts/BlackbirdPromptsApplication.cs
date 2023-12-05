using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts;

public class BlackbirdPromptsApplication : IApplication
{
    public string Name
    {
        get => "Blackbird Prompts";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}