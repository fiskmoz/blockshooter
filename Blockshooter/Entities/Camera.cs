using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockshooter.Entities
{
    class Camera
    {
        public Vector3 Target;
        public Vector3 Position;
        public Vector3 Up;

        // WHAT THE CAMERA CAN SEE
        public Matrix ProjectionMatrix;
        // CAMERA POSITION IN SPACE
        public Matrix ViewMatrix;
        // POSITION IN WORLD
        public Matrix WorldMatrix;

        public Camera(GraphicsDevice graphicsDevice)
        {
            // SETUP CAMERA
            Target = new Vector3(0f, 0f, -1f);
            Position = new Vector3(0f, 40f, 20f);
            Up = Vector3.Up;
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);
            ViewMatrix = Matrix.CreateLookAt(Position, Target, Up);
            WorldMatrix = Matrix.CreateWorld(Target, Vector3.Forward, Up);

        }

        public void UpdateCamera()
        {
            ViewMatrix = Matrix.CreateLookAt(Position, Position + Target, Up);
        }
    }
}
