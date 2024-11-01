using System.Text.Json.Serialization;

namespace Ozon.API.Performance
{
    public class Credentials()
    {
        public string client_id {get; set;} = "";
        public string client_secret {get; set;} = "";
        public string grant_type{get; set;} = "client_credentials";
    }


    public class Token()
    {
        [JsonIgnore]
        public Credentials? credentials{get; set;}

        public string access_token {get; set;} = "";
        public double expires_in {get; set;} = 0;
        public string token_type {get; set;} = "";
        
        public DateTime creationTime = DateTime.Now;

        public DateTime getExpirationTime()
        {
            return creationTime.AddSeconds(expires_in);
        }

        public void update(Token token)
        {
            this.access_token = token.access_token;
            this.expires_in = token.expires_in;
            this.token_type = token.token_type;

            this.creationTime = token.creationTime;
        }
    }
}