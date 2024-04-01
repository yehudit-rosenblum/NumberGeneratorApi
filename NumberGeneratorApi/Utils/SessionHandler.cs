namespace NumberGeneratorApi.Utils
{
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;

    public class SessionHandler
    {
        private readonly ISession _session;

        public SessionHandler(ISession session)
        {
            _session = session;
        }

        //מקבל או מפתח של הערך או מפתח של הקורנט או מפתח של 
        public void Set<T>(string key, T value)
        {
            _session.SetString(key, JsonSerializer.Serialize(value));
        }

        public T Get<T>(string key)
        {
            var value = _session.GetString(key);

            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }

}
