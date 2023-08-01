using Newtonsoft.Json;

namespace CashFlow.Application.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj, Formatting format = Formatting.None)
            => JsonConvert.SerializeObject(obj, format);
    }
}
