using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoInc.Core.Web
{
    public static class ReactiveClient
    {
        public static async void GetJson(string url, IDictionary<string, string> parameters = null, IProgress<Tuple<long, long>> progress = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            bool acceptFormEncoding = true;
            bool requireAuth = true;
            string bearerAccessToken = "abc";

            Uri uri = GetUri(url, parameters);
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            if (requireAuth && !string.IsNullOrWhiteSpace(bearerAccessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", bearerAccessToken);
            }

            if (acceptFormEncoding)
            {
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                request.Content = new FormUrlEncodedContent(parameters);
            }

            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
            {
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();
            }
        }

        public static IObservable<T> Get<T>(string url, IDictionary<string, string> parameters, IProgress<Tuple<long, long>> progress, bool observeOnDispatcher = true)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            Uri uri = GetUri(url, parameters);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpClient client = new HttpClient(new HttpClientHandler()) { Timeout = TimeSpan.FromSeconds(10) };

            var get = Observable.FromAsync(() => client.SendAsync(request, new CancellationToken(false)))
                .SelectMany(async response =>
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Error request.");
                    }

                    return await response.Content.ReadAsStringAsync();
                })
                .Select(json => JsonConvert.DeserializeObject<T>(json));

            return observeOnDispatcher ? get.ObserveOnDispatcher() : get;
        }

        public static IObservable<MemoryStream> GetImage(string imageUrl,
            IDictionary<string, string> parameters = null,
            IProgress<Tuple<long, long>> progress = null,
            bool observeOnDispatcher = true)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            Uri uri = GetUri(imageUrl, parameters);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpClient client = new HttpClient(new HttpClientHandler()) { Timeout = TimeSpan.FromSeconds(10) };

            var image = from response in Observable.FromAsync(() => client.SendAsync(request, new CancellationToken(false)))
                        from stream in Observable.FromAsync(() => ReadResponseStreamToMemoryAsync(response, progress))
                        select stream;

            return observeOnDispatcher ? image.ObserveOnDispatcher() : image;
        }

        private static async Task<MemoryStream> ReadResponseStreamToMemoryAsync(HttpResponseMessage response, IProgress<Tuple<long, long>> progress = null)
        {
            var contentLength = response.Content.Headers.ContentLength;
            var buffer = new byte[4096]; // 4KBs
            var read = 0;

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (MemoryStream memoryStream = new MemoryStream())
            {
                do
                {
                    read = await responseStream.ReadAsync(buffer, 0, buffer.Length);
                    await memoryStream.WriteAsync(buffer, 0, read);

                    //progress.Report(100 * (double)memoryStream.Length / contentLength);

                    // For display progress on UI
                    await Task.Delay(100);
                }
                while (read != 0);

                return memoryStream;
            }
        }

        private static Uri GetUri(string url, IDictionary<string, string> parameters = null)
        {
            Uri uri = new Uri(url);

            if (null != parameters && 0 < parameters.Count)
            {
                StringBuilder sb = new StringBuilder();
                int count = parameters.Count;
                foreach (var pair in parameters)
                {
                    string format = --count == 0 ? "{0}={1}" : "{0}={1}&";
                    sb.AppendFormat(format, pair.Key, Uri.EscapeDataString(pair.Value));
                }

                UriBuilder uriBuilder = new UriBuilder(uri)
                {
                    Query = sb.ToString()
                };

                return uriBuilder.Uri;
            }

            return uri;
        }
    }
}
