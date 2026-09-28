using osu.Framework.Platform;
using osu.Framework;
using VLBeats.Game;

namespace VLBeats.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"VLBeats"))
            using (osu.Framework.Game game = new VLBeatsGame())
                host.Run(game);
        }
    }
}
