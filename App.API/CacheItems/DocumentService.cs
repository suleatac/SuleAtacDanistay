using App.Repository.DocumentItems;
using Microsoft.Extensions.Caching.Distributed;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.API.CacheItems
{
    public class DocumentService(IDistributedCache distributedCache)
    {
        private string GetCacheKey() => CacheKeyConst.CacheKey;
       
        public Task<string?> GetDocumentsFromCache(CancellationToken cancellationToken)
        {

            return distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }
        public async Task CreateCacheAsync(Document document, CancellationToken cancellationToken)
        {
            var key = GetCacheKey();

            var options = new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

            // 1) mevcut listeyi oku
            var cached = await distributedCache.GetStringAsync(key, cancellationToken);

            List<Document> list;
            if (string.IsNullOrWhiteSpace(cached))
            {
                list = new List<Document>();
            }
            else
            {
                list = System.Text.Json.JsonSerializer.Deserialize<List<Document>>(cached)
                       ?? new List<Document>();
            }

            // 2) ekle (istersen duplicate kontrolü yap)
            list.Add(document);

            // 3) geri yaz (ARTIK array yazıyoruz)
            var json = System.Text.Json.JsonSerializer.Serialize(list);
            await distributedCache.SetStringAsync(key, json, options, cancellationToken);
        }
        public Task DeleteDocumentsCacheAsync(CancellationToken cancellationToken)
        {
            return distributedCache.RemoveAsync(GetCacheKey(), token: cancellationToken);
        }
    }
}
