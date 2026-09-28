using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;

namespace VLBeats.Game.Displays.Screens
{
    internal partial class MainMenuScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[] {
                new SpriteText {
                    Text = "VLBeats Alpha v1.0 Main Menu",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 40)
                },
                new BasicButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y=60,
                    Text = "Start Game",
                    Action = () => {
                        this.Push(new BeatmapSelect());
                    },
                    Size = new osuTK.Vector2(200, 50)
                }
            };
        }
    }
}
