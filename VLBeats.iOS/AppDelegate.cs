using osu.Framework.iOS;
using VLBeats.Game;

namespace VLBeats.iOS
{
    /// <inheritdoc />
    public class AppDelegate : GameApplicationDelegate
    {
        /// <inheritdoc />
        protected override osu.Framework.Game CreateGame() => new VLBeatsGame();
    }
}
