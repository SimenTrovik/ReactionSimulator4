using System;

namespace SoftwareDesignExam
{
    public class ConcretePlayerFactory : PlayerFactory
    {
        public ConcretePlayerFactory()
        {
        }
        public override IPlayer GetPlayer(string name, PlayerType type)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException();
         
            return type switch
            {
                PlayerType.Easy => new EasyPlayer(name),
                PlayerType.Normal => new NormalPlayer(name),
                _ => throw new ArgumentException($"Unable to create a player with the type: {type}")
            };
        }
    }
}
