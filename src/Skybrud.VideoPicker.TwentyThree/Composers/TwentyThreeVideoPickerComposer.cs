using Skybrud.VideoPicker.Composers;
using Skybrud.VideoPicker.Providers;
using Skybrud.VideoPicker.TwentyThree.Provider;
using Umbraco.Core.Composing;

namespace Skybrud.VideoPicker.TwentyThree.Composers
{
    [ComposeBefore(typeof(VideoPickerComposer))]
    public class TwentyThreeVideoPickerComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.VideoPickerProviders().Append<TwentyThreeVideoProvider>();
        }
    }
}
