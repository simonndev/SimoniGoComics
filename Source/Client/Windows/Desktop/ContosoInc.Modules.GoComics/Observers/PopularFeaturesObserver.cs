using System.Linq;
using ContosoInc.Modules.GoComics.Models;
using ContosoInc.Modules.GoComics.Models.Contracts;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class PopularFeaturesObserver : FeatureListObserverBase<PopularFeatureArrayContract>
    {
        public PopularFeaturesObserver()
            : base()
        {
        }

        public override void OnNext(PopularFeatureArrayContract value)
        {
            this.Features = value
                .Select(c => new ComicModel
                {
                    FeatureId = c.FeatureId,
                    Title = c.Feature.Title,
                    Author = c.Feature.Author,
                    Icon = c.Feature.IconUrl
                })
                .ToList();
        }
    }
}
