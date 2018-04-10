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
        IApiService _thermostatService;

        public Function()
        {
            _thermostatService = new ThermostatApiService();
        }

        /// <summary>
        /// The entrypoint for our Alexa Skill.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            LambdaLogger.Log("Skill Request Received");

            if (input.Request.GetType() == typeof(LaunchRequest))
            {
                return CreateAudioResponse("<speak>Hi there Devcon. This is the thermostat skill</speak>");

            }
            else if (input.Request.GetType() == typeof(IntentRequest))
            {
                var intent = input.Request as IntentRequest;
                switch (intent.Intent.Name)
                {
                    case "GetTemperature":
                        var currentTemperature = await _thermostatService.GetTemperatureAsync(input.Session.User.AccessToken);

                        return CreateAudioResponse($"<speak> The temperature is <say-as interpret-as='number'>{currentTemperature.Temperature}</say-as> degrees </speak>");
                    case "SetTemperature":
                        var requestedTemperature = intent.Intent.Slots["Temperature"].Value;

                        await _thermostatService.SetTemperatureAsync(input.Session.User.AccessToken, Double.Parse(requestedTemperature));

                        return CreateAudioResponse($"<speak>Ok, I've set the thermostat to <say-as interpret-as='number'>{requestedTemperature}</say-as> degrees</speak>");

                    default:
                        return DefaultResponse();
                }
            }
            else
            {
                return DefaultResponse();
            }
        }


        private SkillResponse CreateAudioResponse(string message)
        {
            var apiVersion = "1.0";

            return new SkillResponse()
            {
                Version = apiVersion,
                Response = new ResponseBody()
                {
                    ShouldEndSession = true,
                    OutputSpeech = new SsmlOutputSpeech()
                    {
                        Ssml = message
                    }
                }
            };
        }

        private SkillResponse DefaultResponse()
        {
            return CreateAudioResponse("<speak>There was a problem with the thermostat</speak>");
        }
    }
}
