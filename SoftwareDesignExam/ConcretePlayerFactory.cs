using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public class ConcretePlayerFactory : PlayerFactory
    {
        public ConcretePlayerFactory()
        {
        }
        public override IPlayer GetPlayer(string name, PlayerType type)
        {
            return type switch
            {
                PlayerType.Easy => new EasyPlayer(name),
                PlayerType.Normal => new NormalPlayer(name),
                _ => throw new ArgumentException($"Unable to create a player with the type: {type}")
            };
        }
    }
}
