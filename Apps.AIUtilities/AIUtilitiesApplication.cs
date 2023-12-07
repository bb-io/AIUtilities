using Blackbird.Applications.Sdk.Common;

namespace Apps.BlackbirdPrompts;

public class AIUtilitiesApplication : IApplication
{
    public string Name
    {
        get => "AI Utilities";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}