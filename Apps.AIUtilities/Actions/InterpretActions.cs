using Apps.BlackbirdPrompts.Models.Request.Interpret;
using Apps.BlackbirdPrompts.Models.Response.Interpret;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Newtonsoft.Json;

namespace Apps.BlackbirdPrompts.Actions;

[ActionList]
public class InterpretActions
{
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