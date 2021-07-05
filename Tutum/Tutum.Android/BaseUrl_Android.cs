using Tutum.Droid;
using Tutum.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace Tutum.Droid
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get() => "file:///android_asset/";
    }
}