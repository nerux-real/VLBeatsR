using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;
using osu.Framework.Allocation;

namespace VLBeats.Game.Displays.Screens
{
    public partial class IntroScreen : Screen
    {
        private SpriteText welcomeText=null;
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                },
                welcomeText = new SpriteText {
                    Text = "Welcome to VLBeats Alpha v1.0\nBy Limro Studios",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 40)
                },
            };
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            base.OnEntering(e);

            welcomeText
                .FadeIn(0, Easing.OutQuad)
                .Then(3500)
                .FadeOut(2000, Easing.InQuint)
                .Finally(_ => this.Push(new MainMenuScreen()));
        }
    }
}
