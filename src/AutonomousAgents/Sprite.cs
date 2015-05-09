using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AutonomousAgents
{
    public class Sprite
    {
        private Texture2D texture;
        private Vector2 origin;
        private Vector2 position;

        public Sprite()
        {
            Color = Color.White;
        }

        public void LoadContent(Texture2D texture2D)
        {
            texture = texture2D;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Width = texture.Width;
            Height = texture.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, position, null, Color, Rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Length
        {
            get { return Width > Height ? Width : Height; }
        }

        public float Radius
        {
            get { return Length; }
        }

        public float Rotation { get; set; }
        public Color Color { get; set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}