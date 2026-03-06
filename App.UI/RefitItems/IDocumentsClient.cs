using Refit;

namespace App.UI.RefitItems
{
    public interface IDocumentsClient
    {
        [Get("/api/Documents")]
        public Task<Object> getAllDocuments();
    }
}
