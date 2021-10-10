using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace ElasticsearchDockerSample
{
    internal class Storage
    {
        private const string IndexName = "docs";

        private readonly IElasticClient client;

        public Storage(string connectionString)
        {
            var settings = new ConnectionSettings(new Uri(connectionString));
            client = new ElasticClient(settings);
        }

        internal async Task SaveAsync(string text)
        {
            var document = new Document { Text = text };
            var response = await client.IndexAsync(document, i => i.Index(IndexName));
            if (!response.IsValid)
            {
                Console.WriteLine($"Unable to save a document: {response.OriginalException.Message}");
            }
        }

        internal async Task<IEnumerable<object>> SearchAsync(string term)
        {
            var response = await client.SearchAsync<Document>(r => r.Index(IndexName).Query(q => q.Match(m => m.Field(f => f.Text).Query(term))));

            if (!response.IsValid)
            {
                Console.WriteLine($"Unable search documents: {response.OriginalException.Message}");
            }

            return response.Hits.Select(h => h.Source);
        }
    }
}