using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockshooter.Entities
{
    class Floor
    {
        public VertexPositionColor[] Vertexts;
        public BoundingBox collisionBox;
        public VertexBuffer vertexBuffer;
        GraphicsDevice GraphicsDevice;
        public Floor(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            Vertexts = new VertexPositionColor[6];
            Vertexts[0].Position = new Vector3(-200, 0, -200);
            Vertexts[0].Color = Color.White;
            Vertexts[1].Position = new Vector3(-200, 0, 200);
            Vertexts[1].Color = Color.White;
            Vertexts[2].Position = new Vector3(200, 0, -200);
            Vertexts[2].Color = Color.White;
            Vertexts[3].Position = Vertexts[1].Position;
            Vertexts[3].Color = Color.White;
            Vertexts[4].Position = new Vector3(200, 0, 200);
            Vertexts[4].Color = Color.White;
            Vertexts[5].Position = Vertexts[2].Position;
            Vertexts[5].Color = Color.White;

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(Vertexts);
            collisionBox = new BoundingBox(new Vector3(-200, -10, -200), new Vector3(200, 0, 200));
        }
    }
}
