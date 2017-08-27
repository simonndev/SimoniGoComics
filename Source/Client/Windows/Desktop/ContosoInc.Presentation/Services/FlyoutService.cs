using ContosoInc.Presentation.Views;
using Prism.Regions;
using System.Linq;

namespace ContosoInc.Presentation.Services
{
    public interface IFlyoutService
    {
        void Toggle(string flyoutName, bool hide = false);
    }

    public class FlyoutService : IFlyoutService
    {
        private readonly IRegionManager _regionManager;

        public FlyoutService(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public void Toggle(string flyoutName, bool hide = false)
        {
            IRegion region = _regionManager.Regions[RegionNames.FlyoutRegion];

            if (region != null)
            {
                var flyout = region.Views
                    .Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutName))
                    .FirstOrDefault() as MahApps.Metro.Controls.Flyout;

                if (flyout != null)
                {
                    flyout.IsOpen = hide ? false : !flyout.IsOpen;
                }
            }
        }
    }
}
