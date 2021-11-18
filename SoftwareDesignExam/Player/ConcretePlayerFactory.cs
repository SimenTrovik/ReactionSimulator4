using System;

namespace SoftwareDesignExam
{
    public class ConcretePlayerFactory : PlayerFactory
    {
        // Chooses which IPlayer subtype to use
        public override IPlayer GetPlayer(string name, PlayerType type)
        {
            return string.IsNullOrEmpty(name)
                ? throw new ArgumentException()
                : type switch
                {
                    PlayerType.Easy => new EasyPlayer(name, GameConfig.Multipliers[type]),
                    PlayerType.Normal => new NormalPlayer(name, GameConfig.Multipliers[type]),
                    _ => throw new ArgumentException($"Unable to create a player with the type: {type}")
                };
        }
    }
}