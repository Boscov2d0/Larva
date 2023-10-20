namespace Larva.Game.Tools
{
    public class States
    {
        public enum GameState
        {
            Null,
            Start,
            Game,
            Pause,
            Lose,
            Exit,
            Restart,
            PreGame
        }
        public enum LarvaState
        {
            Null,
            Play,
            EatGoodFood,
            EatBadFood,
            Death
        }
    }
}