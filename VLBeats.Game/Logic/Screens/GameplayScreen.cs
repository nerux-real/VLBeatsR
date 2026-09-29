using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using VLBeats.Game.Displays.Gameplay;
using VLBeats.Game.Logic.Gameplay;

namespace VLBeats.Game.Displays.Screens
{
    public partial class GameplayScreen : Screen
    {
        private readonly Beatmap beatmap;
        private readonly List<HitCircle> activeCircles = new List<HitCircle>();

        private Playfield playfield = null!;
        private SpriteText judgementText = null!;
        private SpriteText comboText = null!;

        private double currentTime = -1000; //fake clock later swap to norm audio clock
        private int nextIndex;
        private int combo;

        public GameplayScreen(Beatmap beatmap)
        {
            this.beatmap = beatmap;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Black },
                playfield = new Playfield(),
                judgementText = new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Y = 40,
                    Font = FontUsage.Default.With(size: 40),
                    Alpha = 0,
                },
                comboText = new SpriteText
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Position = new Vector2(20, -30),
                    Font = FontUsage.Default.With(size: 60),
                    Text = "0x",
                },
            };
        }

        protected override void Update()
        {
            base.Update();

            currentTime += Time.Elapsed;

            var objects = beatmap.HitObjects;

            while (nextIndex < objects.Count && objects[nextIndex].Time - currentTime <= HitCircle.PREEMPT)
            {
                var hitObject = objects[nextIndex];
                var circle = new HitCircle(hitObject, hitObject.Time - currentTime);

                playfield.Add(circle);
                activeCircles.Add(circle);
                nextIndex++;
            }

            while (activeCircles.Count > 0 && currentTime - activeCircles[0].HitObject.Time > HitWindows.OK)
            {
                var overdue = activeCircles[0];
                activeCircles.RemoveAt(0);
                overdue.Miss();
                showJudgement(Judgement.Miss);
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Repeat)
                return false;

            string key = e.Key.ToString();

            var target = activeCircles.FirstOrDefault(c =>
                string.Equals(c.HitObject.Key, key, System.StringComparison.OrdinalIgnoreCase));

            if (target == null)
                return false;

            double delta = currentTime - target.HitObject.Time;

            if (delta < -HitWindows.OK)
                return false;

            var judgement = HitWindows.Judge(System.Math.Abs(delta));

            activeCircles.Remove(target);
            target.Hit(judgement);
            showJudgement(judgement);

            Logger.Log($"hit {key} at delta {delta:0}ms -> {judgement}");
            return true;
        }

        private void showJudgement(Judgement judgement)
        {
            if (judgement == Judgement.Miss)
                combo = 0;
            else
                combo++;

            comboText.Text = $"{combo}x";

            judgementText.Text = judgement.ToString();
            judgementText.Colour = judgement switch
            {
                Judgement.Perfect => Colour4.Gold,
                Judgement.Good => Colour4.LightGreen,
                Judgement.Ok => Colour4.LightBlue,
                _ => Colour4.Red,
            };
            judgementText.FadeOutFromOne(600);
        }
    }
}
