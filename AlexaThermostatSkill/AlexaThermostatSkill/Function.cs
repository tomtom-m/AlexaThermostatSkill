using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AlexaThermostatSkill
{
    public class Function
    {
        /// <summary>
        /// The entrypoint for our Alexa Skill.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            LambdaLogger.Log("Skill Request Received");

            if (input.Request.GetType() == typeof(LaunchRequest))
            {
                return CreateAudioResponse(new SsmlOutputSpeech()
                {
                    Ssml = "<speak><prosody volume='x-loud'>Hello Devcon</prosody></speak>"
                });
            }
            else if (input.Request.GetType() == typeof(IntentRequest))
            {
                var request = input.Request as IntentRequest;

                switch (request.Intent.Name)
                {
                    case "GetTemperature":
                        return CreateAudioResponse(new SsmlOutputSpeech()
                        {
                            Ssml = "<speak>The current temperature is <say-as interpret-as='Number'>22</say-as> degrees</speak>"
                        });
                    default:
                        return DefaultResponse();
                }
            }
            else
            {
                return DefaultResponse();
            }
        }


        private SkillResponse CreateAudioResponse(SsmlOutputSpeech message)
        {
            var apiVersion = "1.0";

            return new SkillResponse()
            {
                Version = apiVersion,
                Response = new ResponseBody()
                {
                    ShouldEndSession = true,
                    OutputSpeech = message
                }
            };
        }

        private SkillResponse DefaultResponse()
        {
            return CreateAudioResponse(new SsmlOutputSpeech()
            {
                Ssml = "<speak>I don't know what you meant</speak>"
            });
        }
    }
}
