using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RockPaperScissorNetworkLibrary
{
    public static class JsonFactory
    {
        public static string Serialize(object obj) => JsonConvert.SerializeObject(obj);
        public static T Deserialize<T>(string objJson) => JsonConvert.DeserializeObject<T>(objJson);

        public static object DeserializeAuto(string objJson)
        {
            JObject obj = JObject.Parse(objJson);
            string Type = (string)obj["type"];
            if (string.IsNullOrEmpty(Type)) throw new Exception("null Or empty Type from deserialized object");

            switch (Type)
            {
                case nameof(GameCommand): return Deserialize<GameCommand>(objJson);
                case nameof(GameRequestData): return Deserialize<GameRequestData>(objJson);
                case nameof(GameSendData): return Deserialize<GameSendData>(objJson);
                case nameof(ServerResponse): return Deserialize<ServerResponse>(objJson);
            }
            return null;
        }

    }
}
