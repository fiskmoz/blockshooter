using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockshooter.Entities
{
    class Block
    {
        public BoundingBox collisionBox;
        public VertexPositionColor[] vertices;
        public VertexBuffer vertexBuffer;
        GraphicsDevice GraphicsDevice;

        public Block (GraphicsDevice graphicsDevice, Vector3 startLocation, Color color)
        {
            GraphicsDevice = graphicsDevice;
            vertices = new VertexPositionColor[8];
            float width = 5;
            float height = 5;
            float depth = 5;

            //front left bottom corner
            vertices[0] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //front left upper corner
            vertices[1] = new VertexPositionColor(startLocation + new Vector3(width, 0, 0), color);
            //front right upper corner
            vertices[2] = new VertexPositionColor(startLocation + new Vector3(width, -height, 0), color);
            //front lower right corner
            vertices[3] = new VertexPositionColor(startLocation + new Vector3(0, -height, 0), color);
            //back left lower corner
            vertices[4] = new VertexPositionColor(startLocation + new Vector3(0, 0, depth), color);
            //back left upper corner
            vertices[5] = new VertexPositionColor(startLocation + new Vector3(width, 0, depth), color);
            //back right upper corner
            vertices[6] = new VertexPositionColor(startLocation + new Vector3(width, -height, depth), color);
            //back right lower corner
            vertices[7] = new VertexPositionColor(startLocation + new Vector3(0, -height, depth), color);

            collisionBox = new BoundingBox(vertices[0].Position, vertices[6].Position);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);
        }

        public void UpdateGravity()
        {
            for (int i = 0; i < vertices.Count(); i++)
            {
                vertices[i].Position = vertices[i].Position - new Vector3(0f, 0.1f, 0f);
            }
        }
    }
}
