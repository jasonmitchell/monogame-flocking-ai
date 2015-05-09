using System;
using System.Collections.Generic;
using AutonomousAgents.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomousAgents
{
    public class Game1 : Game
    {
        private const int PopulationSize = 175;
        private const int SensorDistance = 50;
        private const float MaxSpeed = 2f;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly List<Agent> agents = new List<Agent>(); 
        private readonly List<Sprite> sprites = new List<Sprite>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Random random = new Random();

            for (int i = 0; i < PopulationSize; i++)
            {
                Vector2 velocity = new Vector2((float)random.NextDouble() * MaxSpeed, 0);
                float angle = MathHelper.ToRadians(random.Next(360));

                Sprite sprite = new Sprite();
                sprite.Position = new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height));
                sprites.Add(sprite);

                Agent agent = new Agent(sprite, SensorDistance, MaxSpeed);
                agent.Velocity = Vector2.Transform(velocity, Matrix.CreateRotationZ(angle));
                agents.Add(agent);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D texture = Content.Load<Texture2D>("Arrow");

            foreach(Sprite sprite in sprites)
            {
                sprite.LoadContent(texture);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(Agent agent in agents)
            {
                agent.Update(agents);
                WrapAround(agent.Entity);
            }

            base.Update(gameTime);
        }


        private void WrapAround(Sprite sprite)
        {
            Vector2 newPosition = sprite.Position;

            if (sprite.Position.X < -sprite.Length)
                newPosition.X = GraphicsDevice.Viewport.Width + sprite.Length;
            else if (sprite.Position.X > GraphicsDevice.Viewport.Width + sprite.Length)
                newPosition.X = -sprite.Length;

            if (sprite.Position.Y < -sprite.Length)
                newPosition.Y = GraphicsDevice.Viewport.Height + sprite.Length;
            else if (sprite.Position.Y > GraphicsDevice.Viewport.Height + sprite.Length)
                newPosition.Y = -sprite.Length;

            sprite.Position = newPosition;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            foreach(Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
