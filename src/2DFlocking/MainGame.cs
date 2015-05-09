using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2DFlocking.AI;

namespace _2DFlocking
{
    public class MainGame : Game
    {
        private const int PopulationSize = 300;
        private const int SensorDistance = 100;
        private const int MinColor = 25;
        private const int MaxColor = 180;
        private const float MinScale = 1f;
        private const float MaxScale = 10f;
        private const float MaxVelocity = 1f;
        private const float PlayerRadius = 100f;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly List<Sprite> sprites = new List<Sprite>();
        private readonly Random random = new Random();
        private readonly AIController aiController = new AIController();

        private readonly Player player = new Player(PlayerRadius);

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Color color = new Color(random.Next(MinColor, MaxColor), random.Next(MinColor, MaxColor), random.Next(MinColor, MaxColor));
                float scale = (float) (random.NextDouble() + MinScale) * MaxScale;
                Vector2 velocity = new Vector2((float)random.NextDouble() * MaxVelocity, 0);
                float angle = MathHelper.ToRadians(random.Next(360));

                Sprite sprite = new Sprite(color, scale);
                sprite.Velocity = Vector2.Transform(velocity, Matrix.CreateRotationZ(angle));
                sprite.Position = new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height));
                sprites.Add(sprite);

                Agent agent = new Agent(aiController, sprite);
                agent.SensorDistance = SensorDistance;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D texture = TextureGenerator.CreateBlankTexture(GraphicsDevice, Color.White, 1, 1);
            sprites.ForEach(sprite => sprite.LoadContent(texture));
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 position = new Vector2(mouseState.X, mouseState.Y);
            player.Position = position;

            sprites.ForEach(sprite => UpdateSprite(sprite, gameTime));
            aiController.Update(gameTime, player);

            base.Update(gameTime);
        }

        private void UpdateSprite(Sprite sprite, GameTime gameTime)
        {
            sprite.Update(gameTime);
            WrapAround(sprite);
        }

        private void WrapAround(Sprite sprite)
        {
            Vector2 spritePosition = sprite.Position;

            if (spritePosition.X < 0 - sprite.Scale)
                spritePosition.X = GraphicsDevice.Viewport.Width + sprite.Scale;
            else if (spritePosition.X > GraphicsDevice.Viewport.Width + sprite.Scale)
                spritePosition.X = 0 - sprite.Scale;

            if (spritePosition.Y < 0 - sprite.Scale)
                spritePosition.Y = GraphicsDevice.Viewport.Height + sprite.Scale;
            else if (spritePosition.Y > GraphicsDevice.Viewport.Height + sprite.Scale)
                spritePosition.Y = 0 - sprite.Scale;

            sprite.Position = spritePosition;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            sprites.ForEach(sprite => sprite.Draw(spriteBatch));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
