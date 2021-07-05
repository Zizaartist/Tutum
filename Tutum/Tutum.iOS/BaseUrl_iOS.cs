using Foundation;
using Tutum.Interfaces;
using Tutum.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace Tutum.iOS
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get() => NSBundle.MainBundle.BundlePath;
    }
}