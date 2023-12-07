using Blackbird.Applications.Sdk.Common;

namespace Apps.AIUtilities;

public class AiUtilitiesApplication : IApplication
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