namespace SoftwareDesignExam
{
    public abstract class PlayerFactory
    {
        public enum PlayerType
        {
            Normal,
            Easy
        }

        public abstract IPlayer GetPlayer(string name, PlayerType type);
    }
}