using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace VLBeats.Game.Displays.Gameplay
{
    public partial class Playfield : Container
    {
        public static readonly Vector2 BASE_SIZE = new Vector2(512, 384);

        private readonly Container content;
        protected override Container<Drawable> Content => content;

        public Playfield()
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            FillMode = FillMode.Fit;
            FillAspectRatio = BASE_SIZE.X / BASE_SIZE.Y;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black,
                    Alpha = 1 //background alpha later change
                },
                new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    TargetDrawSize = BASE_SIZE,
                    Strategy = DrawSizePreservationStrategy.Minimum,
                    Child = content = new Container { RelativeSizeAxes = Axes.Both }
                }
            };
        }
    }
}
