namespace SoftwareDesignExam
{
    public abstract class PlayerFactory
    {
        public abstract IPlayer GetPlayer(string name, PlayerType type);
    }
}