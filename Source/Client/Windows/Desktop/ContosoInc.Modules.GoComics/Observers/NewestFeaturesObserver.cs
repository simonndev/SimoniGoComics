using ContosoInc.Modules.GoComics.Models;
using ContosoInc.Modules.GoComics.Models.Contracts;
using System.Linq;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class NewestFeaturesObserver : FeatureListObserverBase<NewestFeaturesContract>
    {
        public NewestFeaturesObserver()
            :base()
        {
        }

        public override void OnNext(NewestFeaturesContract value)
        {
            this.Features = value.Features
                .Select(c => new ComicModel
                {
                    FeatureId = c.Id,
                    Title = c.Title,
                    Author = c.Author,
                    Icon = c.IconUrl
                }).ToList();
        }
    }
}
