using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;

namespace VLBeats.Game.Displays.Screens
{
    public partial class BeatmapSelect : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            var songList = new FillFlowContainer
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10),
                Padding = new MarginPadding(20),
            };

            for (int i = 0; i < 10; i++)
            {
                int index = i;

                songList.Add(new BasicButton
                {
                    Text = $"Song Name {index + 1}",
                    Size = new Vector2(600, 100),
                    Action = () => this.Push(new BeatmapSelect())
                });
            }

            InternalChildren = new Drawable[]
            {
                new SpriteText
                {
                    Text = "VLBeats Alpha v1.0 Song Selector",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 40)
                },
                songList
            };
        }
    }
}
