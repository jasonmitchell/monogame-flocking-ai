using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2DFlocking
{
    public class Sprite : IHasPosition
    {
        private Texture2D texture;

        public Sprite(Color color, float scale)
        {
            Color = color;
            Scale = scale;
        }

        public void LoadContent(Texture2D texture2D)
        {
            texture = texture2D;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(texture != null)
                spriteBatch.Draw(texture, Position, null, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
    }
}
