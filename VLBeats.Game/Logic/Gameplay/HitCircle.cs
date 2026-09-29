using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using VLBeats.Game.Logic.Gameplay;

namespace VLBeats.Game.Displays.Gameplay
{
    public partial class HitCircle : CompositeDrawable
    {
        public const float DIAMETER = 50;
        public const double PREEMPT = 800;

        public readonly HitObject HitObject;

        private readonly double timeUntilHit;

        private Circle body = null!;
        private SpriteText label = null!;
        private CircularContainer approachRing = null!;
        private CircularContainer burst = null!;

        public HitCircle(HitObject hitObject, double timeUntilHit)
        {
            HitObject = hitObject;
            this.timeUntilHit = timeUntilHit;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(DIAMETER);
            Origin = Anchor.Centre;
            Position = new Vector2(HitObject.X, HitObject.Y);
            Alpha = 0;

            InternalChildren = new Drawable[]
            {
                burst = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    BorderThickness = 6,
                    BorderColour = Colour4.White,
                    Alpha = 0,
                    Child = new Box { RelativeSizeAxes = Axes.Both, Alpha = 0, AlwaysPresent = true },
                },
                body = new Circle
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Colour4.DeepSkyBlue,
                },
                label = new SpriteText
                {
                    Text = HitObject.Key,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 32),
                    Colour = Colour4.Black,
                },
                approachRing = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    BorderColour = Colour4.White,
                    BorderThickness = 1,
                    Alpha = 0,
                    Child = new Box { RelativeSizeAxes = Axes.Both, Alpha = 0, AlwaysPresent = true },
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            double remaining = Math.Max(timeUntilHit, 0);

            this.FadeIn(Math.Min(300, remaining));
            this.ScaleTo(0.6f).ScaleTo(1f, Math.Min(350, Math.Max(remaining, 1)), Easing.OutBack);

            approachRing.FadeTo(0.9f, Math.Min(300, remaining));
            approachRing.Scale = new Vector2(3);
            approachRing.ScaleTo(1, remaining);
        }

        public void Hit(Judgement judgement)
        {
            approachRing.FadeOut(50);
            label.FadeOut(80);

            body.ScaleTo(1.35f, 250, Easing.OutQuint).FadeOut(250);

            burst.BorderColour = judgement switch
            {
                Judgement.Perfect => Colour4.Gold,
                Judgement.Good => Colour4.LightGreen,
                _ => Colour4.White,
            };
            burst.FadeTo(1f).ScaleTo(1.9f, 350, Easing.OutQuint).FadeOut(350);

            this.Delay(400).Expire();
        }

        public void Miss()
        {
            approachRing.FadeOut(100);
            body.FadeColour(Colour4.Red, 100);
            this.MoveToOffset(new Vector2(0, 12), 400, Easing.In).FadeOut(400);
            this.Delay(450).Expire();
        }
    }
}
