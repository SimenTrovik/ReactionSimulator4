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
            return string.IsNullOrEmpty(name)
                ? throw new ArgumentException()
                : type switch
                {
                    PlayerType.Easy => new EasyPlayer(name, GameConfig.multipliers[type]),
                    PlayerType.Normal => new NormalPlayer(name, GameConfig.multipliers[type]),
                    _ => throw new ArgumentException($"Unable to create a player with the type: {type}")
                };
        }
    }
}
