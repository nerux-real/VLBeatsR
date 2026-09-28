using osu.Framework.Testing;

namespace VLBeats.Game.Tests.Visual
{
    public abstract partial class VLBeatsTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new VLBeatsTestSceneTestRunner();

        private partial class VLBeatsTestSceneTestRunner : VLBeatsGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
