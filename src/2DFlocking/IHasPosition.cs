using Microsoft.Xna.Framework;

namespace _2DFlocking
{
    public interface IHasPosition
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
    }
}
