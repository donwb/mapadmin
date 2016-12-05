using System;
using Nancy;
using Newtonsoft.Json.Linq;

namespace NancyApplication
{

    public class ApiModule : NancyModule
    {
        public ApiModule()
        {
            Get("/api/ping", args => "Pong");
            Get("/api/getActions", args => GetActions(args));
            Get("/api/userExists/{username}", args => UserExists(args));
            Get("/api/getActivity/{username}", args => GetActivity(args));
            
        }

        public object GetActions(dynamic o)
        {
            DataAccess da = new DataAccess();
            var actions = da.GetActions();

            JArray jsonArray = JArray.Parse(actions);
            var res = PrepResponse("ok", jsonArray);
            var wireResponse = Response.AsText(res, "application/json");
            
            return wireResponse;
        }

        public object UserExists(dynamic o )
        {
            var username = (string)o.username;

            DataAccess da = new DataAccess();
            var exists = da.UserExists(username);

            JObject obj = JObject.FromObject(new
            {
                userExists = exists
            });
            var jsonResult = PrepResponse("ok", obj);

            var response = Response.AsText(jsonResult.ToString(), "application/json");
            return response;
        }

        public object GetActivity(dynamic o)
        {
            var username = (string)o.username;
            DataAccess da = new DataAccess();
            var activity = da.GetActivity(username);

            Console.WriteLine(activity);

            JArray obj = JArray.Parse(activity);
            var res = PrepResponse("ok", obj);

            var response = Response.AsText(res, "application/json");
            return response;
            
        }

        private string PrepResponse(string status, JContainer message)
        {
            JObject json = JObject.FromObject(new
            {
                status = status,
                res = message
            });

            var resp = json.ToString(Newtonsoft.Json.Formatting.None);
            return resp;
        }
    } 
}