using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoInc.Core.Web;
using ContosoInc.Modules.GoComics.Models.Contracts;
using System.Reactive.Linq;
using System.Diagnostics;
using System.IO;

namespace ContosoInc.Modules.GoComics.Services
{
    public interface IGoComicsService
    {
        void SignIn(string username, string password, IObserver<SignInContract> signInObserver, IProgress<Tuple<long, long>> progress = null);

        void GetAllFeatures(IObserver<FeatureContract> observer,
            IProgress<Tuple<long, long>> progress = null);

        void GetPopularFeatures(IObserver<PopularFeatureArrayContract> observer,
            IProgress<Tuple<long, long>> progress = null);

        void GetNewestFeatures(IObserver<NewestFeaturesContract> observer,
            IProgress<Tuple<long, long>> progress = null);

        void GetRecentFeature(int featureId,
            IObserver<FeatureItemContract> observer,
            IProgress<Tuple<long, long>> progress = null);

        void DownloadComicPageImage(string imageUrl,
            IObserver<MemoryStream> observer,
            IProgress<Tuple<long, long>> progress = null);
    }

    public class GoComicsService : IGoComicsService
    {
        private const int TimeOut = 10; // TODO: should be read from configuration file.
        private const string ApiKey = "KAJS6R5FJAS3";

        private const string AllFeatures = "http://www.gocomics.com/api/features.json";
        private const string AllTimeFeatures = "http://www.gocomics.com/api/featured.json";
        private const string PopularFeatures = "http://www.gocomics.com/api/featured_comics.json";
        private const string NewestFeatures = "http://www.gocomics.com/api/newest.json";
        private const string RecentFeature = "http://www.gocomics.com/api/feature/{0}/recent.xml";

        private const string SignInUrl = "http://www.gocomics.com/api/user/{0}/password/{1}/auth.json";

        private static readonly IDictionary<string, string> Parameters = new Dictionary<string, string>
        {
            { "client_code", ApiKey }
        };

        private static readonly object SyncRoot = new object();
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(TimeOut);

        private static readonly IDictionary<Guid, IDisposable> CurrentSubscriptions =
            new Dictionary<Guid, IDisposable>();

        public void SignIn(string username, string password,
            IObserver<SignInContract> signInObserver,
            IProgress<Tuple<long, long>> progress = null)
        {
            string url = string.Format(SignInUrl, username, password);
            
            SubscribeJson<SignInContract>(signInObserver, url, Parameters, progress);
        }

        public void GetAllFeatures(IObserver<FeatureContract> observer,
            IProgress<Tuple<long, long>> progress = null)
        {
            SubscribeJson<FeatureContract>(observer, AllFeatures, Parameters, progress);
        }

        public void GetPopularFeatures(IObserver<PopularFeatureArrayContract> observer,
            IProgress<Tuple<long, long>> progress = null)
        {
            SubscribeJson(observer, PopularFeatures, Parameters, progress);
        }

        public void GetNewestFeatures(IObserver<NewestFeaturesContract> observer,
            IProgress<Tuple<long, long>> progress = null)
        {
            SubscribeJson(observer, NewestFeatures, Parameters, progress);
        }

        public void GetRecentFeature(int featureId,
            IObserver<FeatureItemContract> observer,
            IProgress<Tuple<long, long>> progress = null)
        {
            string recentUrl = string.Format(RecentFeature, featureId);
            
            SubscribeJson(observer, recentUrl, Parameters, progress);
        }

        public void GetNewestFeatures(IObserver<FeatureContract> observer, IProgress<Tuple<long, long>> progress = null)
        {
            SubscribeJson(observer, NewestFeatures, Parameters, progress);
        }

        public void DownloadComicPageImage(string imageUrl,
            IObserver<MemoryStream> observer,
            IProgress<Tuple<long, long>> progress = null)
        {
            SubscribeImage(imageUrl, observer, Parameters, progress);
        }

        private static void SubscribeImage(string imageUrl,
            IObserver<MemoryStream> observer,
            IDictionary<string, string> parameters = null,
            IProgress<Tuple<long, long>> progress = null)
        {
            var subscriptionKey = Guid.NewGuid();

            var observable = ReactiveClient.GetImage(imageUrl, parameters, progress);
            var disposal = observable.Finally(() => CleanupSubscription(subscriptionKey)).Subscribe(observer);

            lock (SyncRoot)
            {
                CurrentSubscriptions.Add(subscriptionKey, disposal);
            }
        }        

        private static void SubscribeJson<T>(IObserver<T> observer,
            string url,
            IDictionary<string, string> parameters,
            IProgress<Tuple<long, long>> progress = null)
        {
            var subscriptionKey = Guid.NewGuid();

            var observable = ReactiveClient.Get<T>(url, parameters, progress);
            var disposal = observable.Finally(() => CleanupSubscription(subscriptionKey)).Subscribe(observer);

            lock (SyncRoot)
            {
                CurrentSubscriptions.Add(subscriptionKey, disposal);
            }
        }

        private static void CleanupSubscription(Guid key)
        {
            lock (SyncRoot)
            {
                if (!CurrentSubscriptions.ContainsKey(key))
                {
                    Debug.WriteLine(
                        string.Format("GoComicsService::Cleanup - CurrentSubscriptions does not contain key {0}", key));
                    return;
                }

                CurrentSubscriptions[key].Dispose();
                CurrentSubscriptions.Remove(key);
            }
        }
    }
}
