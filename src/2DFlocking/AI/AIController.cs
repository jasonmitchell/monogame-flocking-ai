using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2DFlocking.AI
{
    public class AIController
    {
        private List<Agent> agents = new List<Agent>();

        public int AddAgent(Agent agent)
        {
            agents.Add(agent);
            return agents.Count - 1;
        }

        public void Update(GameTime gameTime, Player player)
        {
            agents.ForEach(agent => agent.Update(ref agents, player));
        }
    }
}
