namespace App.API.CacheItems
{
    public class RedisConnectionTostringOptions
    {
        public const string Key = "ConnectionStrings";
        public string Redis { get; set; } = default!;
    }
}
