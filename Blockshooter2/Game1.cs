using Blockshooter.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blockshooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Floor floor;

        BasicEffect basicEffect;

        // Geometric info
        VertexPositionColor[] triangleVerticies;
        VertexBuffer vertexBuffer;


        private MouseState mouseLastState;
        private MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            floor = new Floor();

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
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Position.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Position.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera.Position.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.Position.Z -= 1f;
            }


            camera.UpdateCamera();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            basicEffect.Projection = camera.ProjectionMatrix;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.World = camera.WorldMatrix;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, floor.Vertexts, 0, 2);
            }

            base.Draw(gameTime);
        }
    }
}
