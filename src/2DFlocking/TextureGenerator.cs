using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2DFlocking
{
    public static class TextureGenerator
    {
        public static Texture2D CreateBlankTexture(GraphicsDevice graphicsDevice, Color color, int width, int height)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            
            Color[] colorData = new Color[width * height];
            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = color;

            texture.SetData(colorData);

            return texture;
        }
    }
}
