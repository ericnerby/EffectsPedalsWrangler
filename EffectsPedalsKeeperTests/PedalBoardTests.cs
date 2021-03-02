using Xunit;

namespace EffectsPedalsKeeper.Tests
{
    public class PedalBoardTests
    {
        private PedalBoard _pedalBoard;
        private string _boardName = "Cover Band Board";

        public PedalBoardTests()
        {
            _pedalBoard = new PedalBoard(_boardName);
        }

        [Fact()]
        public void ToStringTest()
        {
            var target = _pedalBoard.ToString();
            var expected = _boardName;

            Assert.Contains(expected, target);
        }
    }
}