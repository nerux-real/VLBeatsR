using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using VLBeats.Game.Logic.Gameplay;

namespace VLBeats.Game.Displays.Screens
{
    public partial class BeatmapSelect : Screen
    {
        private static Beatmap loadTestBeatmap()
        {
            var map = Beatmap.Parse(new[]
            {
                "1000 256 192 A",
                "1500 100 100 B",
                "2000 100 100 C",
                "2500 100 100 D",
                "3000 100 100 E",
                "3500 100 100 F",
                "4000 100 100 G",
                "4500 100 100 H",
                "5000 100 100 I",
                "5500 100 100 J",
                "6000 100 100 K",
                "6500 100 100 L",
                "7000 100 100 M",
                "7500 100 100 N",
                "8000 100 100 B",
                "8500 100 100 B",
                "9000 100 100 B",
                "9500 100 100 B",
                "10000 100 100 B",
                "10500 100 100 B",
                "11000 100 100 B",
                "11500 100 100 B",
                "12000 100 100 B",
                "12500 100 100 B",
                "13000 100 100 B",
                "13500 100 100 B",
                "14000 100 100 B",
                "14500 100 100 B",
                "15000 100 100 B",
            });

            Logger.Log($"Loaded beatmap with {map.HitObjects.Count} hit objects.");
            return map;
        }
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
                    CornerRadius = 10,
                    Action = () => this.Push(new GameplayScreen(loadTestBeatmap()))
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
