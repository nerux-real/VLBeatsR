using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace VLBeats.Game.Tests
{
    public partial class VLBeatsTestBrowser : VLBeatsGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();
            Host.Window.Title = "VLBeats Alpha v1.0";

            AddRange(new Drawable[]
            {
                new TestBrowser("VLBeats"),
                new CursorContainer()
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
