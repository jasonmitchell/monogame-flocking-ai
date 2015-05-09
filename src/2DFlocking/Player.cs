using Microsoft.Xna.Framework;

namespace _2DFlocking
{
    public class Player
    {
        public Player(float radius)
        {
            Radius = radius;
        }

        public Vector2 Position { get; set; }
        public float Radius { get; private set; }
    }
}
