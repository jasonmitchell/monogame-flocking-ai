using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2DFlocking.AI
{
    public class Agent
    {
        private const int SeparationWeight = 4;
        private const int CohesionWeight = 6;
        private const int AlignmentWeight = 2;

        private readonly IHasPosition entity;
        private readonly int index;

        public Agent(AIController aiController, IHasPosition gameObject)
        {
            index = aiController.AddAgent(this);
            entity = gameObject;
        }

        public void Update(ref List<Agent> agents, Player player)
        {
            if(!EvadePlayer(player))
                ReactToAgents(ref agents);
        }

        private bool EvadePlayer(Player player)
        {
            Vector2 vectorToPlayer = player.Position - entity.Position;
            int distance = (int)vectorToPlayer.Length();

            if (distance < player.Radius)
            {
                float relativity = player.Radius/ distance;
                vectorToPlayer.Normalize();
                entity.Velocity = -vectorToPlayer * (2 * relativity);

                return true;
            }

            return false;
        }

        public void ReactToAgents(ref List<Agent> agents)
        {
            Vector2 separationForce = Vector2.Zero;
            Vector2 cohesionForce = Vector2.Zero;
            Vector2 alignmentForce = Vector2.Zero;

            for(int i = 0; i < agents.Count; i++)
            {
                if(index != i)
                {
                    Vector2 separation = entity.Position - agents[i].entity.Position;

                    if(separation.Length() < SensorDistance)
                    {
                        alignmentForce += agents[i].entity.Velocity;

                        float distance = Math.Abs(separation.Length());
                        if (distance == 0)
                            distance = 1;

                        cohesionForce += agents[i].entity.Position;

                        separation.Normalize();
                        separationForce += separation / distance;
                    }
                }
            }

            if(alignmentForce.LengthSquared() != 0)
                alignmentForce.Normalize();

            if(cohesionForce.LengthSquared() != 0)
                cohesionForce.Normalize();

            cohesionForce /= agents.Count;
            entity.Position += (separationForce*SeparationWeight) + (cohesionForce*CohesionWeight) + (alignmentForce*AlignmentWeight);
        }

        public int SensorDistance { get; set; }
    }
}
