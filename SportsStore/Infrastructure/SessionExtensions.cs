using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            // Configure serialization options to handle object references
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            session.SetString(key, JsonSerializer.Serialize(value, options));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);

            if (string.IsNullOrEmpty(sessionData))
                return default;

            // Configure deserialization options to handle object references
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            return JsonSerializer.Deserialize<T>(sessionData, options);
        }
    }
}