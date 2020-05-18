using Blockshooter.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Blockshooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Floor floor;
        List<Block> blockList;
        int cooldownDefault = 1;
        int blockCooldown = 0;
        int timer;

        float jumpVelocity = 2f;
        float throwVelocityMultiplier = 5f;

        BasicEffect basicEffect;

        // Geometric info
        VertexPositionColor[] triangleVerticies;
        VertexBuffer vertexBuffer;


        private MouseState mouseLastState;
        private MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;

            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;

            triangleVerticies = new VertexPositionColor[3];
            triangleVerticies[0] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Red);
            triangleVerticies[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);
            triangleVerticies[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVerticies);
            camera = new Camera(GraphicsDevice);
            floor = new Floor(GraphicsDevice);
            blockList = new List<Block>();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseLastState = mouseState;
            mouseState = Mouse.GetState();

            

            float mouseAmount = 0.01f;

            Vector3 direction = camera.Target;
            direction.Normalize();

            Vector3 normal = Vector3.Cross(direction, camera.Up);

            //You don't have to have the button pressed for an FPS obviously)`
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                float y = mouseState.Y - mouseLastState.Y;
                float x = mouseState.X - mouseLastState.X;


                camera.Target += x * mouseAmount * normal;

                camera.Target -= y * mouseAmount * camera.Up;
                camera.Target.Normalize();
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (blockCooldown == 0)
                {
                    blockCooldown = cooldownDefault;
                    blockList.Add(new Block(GraphicsDevice, camera.Position + camera.Target*50, camera.Target*throwVelocityMultiplier, Color.Black));
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Position += normal;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Position -= normal;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera.Position.Z = camera.Position.Z + camera.Target.Z;
                camera.Position.X = camera.Position.X + camera.Target.X;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.Position.Z = camera.Position.Z - camera.Target.Z;
                camera.Position.X = camera.Position.X - camera.Target.X;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if(!camera.isJumping)
                {
                    camera.Jump(new Vector3(0, jumpVelocity, 0));
                }
            }
            if(camera.Position.Y >= 50)
            {
                camera.UpdateGravity();
            }
            else
            {
                camera.isJumping = false;
            }
            camera.UpdateCamera();
            foreach (Block block in blockList)
            {
                if (!block.collisionBox.Intersects(floor.collisionBox))
                {
                    block.UpdateGravity();
                }
                foreach (Block secondBlock in blockList)
                {
                    if(object.ReferenceEquals(block, secondBlock))
                    {
                        continue;
                    }
                    if (block.collisionBox.Intersects(secondBlock.collisionBox)) 
                    {
                        block.Freeze();
                    }
                }
            }
            if (timer > 60)
            {
                if(blockCooldown > 0)
                {
                    blockCooldown -= 1;
                }
                timer = 0;
            }
            timer++;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            basicEffect.Projection = camera.ProjectionMatrix;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.World = camera.WorldMatrix;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.SetVertexBuffer(vertexBuffer);
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
                GraphicsDevice.SetVertexBuffer(floor.vertexBuffer);
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 6);
                foreach (Block block in blockList)
                {
                    GraphicsDevice.SetVertexBuffer(block.vertexBuffer);
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
                }
            }

            base.Draw(gameTime);
        }
    }
}
