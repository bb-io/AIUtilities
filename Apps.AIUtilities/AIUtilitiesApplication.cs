using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.AIUtilities;

public class AiUtilitiesApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.ArtificialIntelligence, ApplicationCategory.Utilities];
        set { }
    }
    
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