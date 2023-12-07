using Apps.AIUtilities.Models.Request.Interpret;
using Apps.AIUtilities.Models.Response.Interpret;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Newtonsoft.Json;

namespace Apps.AIUtilities.Actions;

[ActionList]
public class InterpretActions
{
    [Action("Interpret MQM Analysis", Description = "Interpret AI response of MQM analysis")]
    public MqmAnalysis InterpretMqmAnalysis([ActionParameter] InterpretMqmAnalysisRequest input)
    {
        try
        {
            return JsonConvert.DeserializeObject<MqmAnalysis>(input.ResponseContent)!;
        }
        catch
        {
            throw new Exception("Something went wrong parsing the output from OpenAI, most likely due to a hallucination!");
        }
    }
}