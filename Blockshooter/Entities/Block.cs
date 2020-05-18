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
        public int verticiesLen;
        public VertexBuffer vertexBuffer;
        GraphicsDevice GraphicsDevice;
        public Vector3 velocity; 
        public float gravityVelocity = 0.2f;
        public float velocityDropoff = 0.08f;

        public Block (GraphicsDevice graphicsDevice, Vector3 startLocation, Vector3 _velocity, Color color)
        {
            GraphicsDevice = graphicsDevice;
            verticiesLen = 36;
            vertices = new VertexPositionColor[verticiesLen];
            float width = 15;
            float height = 15;
            float depth = 15;
            velocity = _velocity;

            //front left bottom corner
            vertices[0] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //front right lower corner
            vertices[1] = new VertexPositionColor(startLocation + new Vector3(width, 0, 0), color);
            //front right upper corner
            vertices[2] = new VertexPositionColor(startLocation + new Vector3(width, height, 0), color);

            //front left bottom corner
            vertices[3] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //front left upper corner
            vertices[4] = new VertexPositionColor(startLocation + new Vector3(0, height, 0), color);
            //front right upper corner
            vertices[5] = new VertexPositionColor(startLocation + new Vector3(width, height, 0), color);

            //back left bottom corner
            vertices[6] = new VertexPositionColor(startLocation + new Vector3(0, 0, depth), color);
            //back right lower corner
            vertices[7] = new VertexPositionColor(startLocation + new Vector3(width, 0, depth), color);
            //back right upper corner
            vertices[8] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //back left bottom corner
            vertices[9] = new VertexPositionColor(startLocation + new Vector3(0, 0, depth), color);
            //back left upper corner
            vertices[10] = new VertexPositionColor(startLocation + new Vector3(0, height, depth), color);
            //back right upper corner
            vertices[11] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //right right bottom corner
            vertices[12] = new VertexPositionColor(startLocation + new Vector3(width, 0, 0), color);
            //right back lower corner
            vertices[13] = new VertexPositionColor(startLocation + new Vector3(width, 0, depth), color);
            //right back upper corner
            vertices[14] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //right right bottom corner
            vertices[15] = new VertexPositionColor(startLocation + new Vector3(width, 0, 0), color);
            //right left upper corner
            vertices[16] = new VertexPositionColor(startLocation + new Vector3(width, height, 0), color);
            //right right upper corner
            vertices[17] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //left left bottom corner
            vertices[18] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //left back lower corner
            vertices[19] = new VertexPositionColor(startLocation + new Vector3(0, 0, depth), color);
            //left back upper corner
            vertices[20] = new VertexPositionColor(startLocation + new Vector3(0, height, depth), color);

            //left right bottom corner
            vertices[21] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //left left upper corner
            vertices[22] = new VertexPositionColor(startLocation + new Vector3(0, height, 0), color);
            //left right upper corner
            vertices[23] = new VertexPositionColor(startLocation + new Vector3(0, height, depth), color);

            //top left upper corner
            vertices[24] = new VertexPositionColor(startLocation + new Vector3(0, height, 0), color);
            //top back left corner
            vertices[25] = new VertexPositionColor(startLocation + new Vector3(0, height, depth), color);
            //top back upper corner
            vertices[26] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //top right bottom corner
            vertices[27] = new VertexPositionColor(startLocation + new Vector3(0, height, 0), color);
            //top left upper corner
            vertices[28] = new VertexPositionColor(startLocation + new Vector3(width, height, 0), color);
            //top right upper corner
            vertices[29] = new VertexPositionColor(startLocation + new Vector3(width, height, depth), color);

            //lower left bottom corner
            vertices[30] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //lower back left corner
            vertices[31] = new VertexPositionColor(startLocation + new Vector3(0, 0, depth), color);
            //lower back upper corner
            vertices[32] = new VertexPositionColor(startLocation + new Vector3(width, 0, depth), color);

            //lower right bottom corner
            vertices[33] = new VertexPositionColor(startLocation + new Vector3(0, 0, 0), color);
            //lower left upper corner
            vertices[34] = new VertexPositionColor(startLocation + new Vector3(width, 0, 0), color);
            //lower right upper corner
            vertices[35] = new VertexPositionColor(startLocation + new Vector3(width, 0, depth), color);

            collisionBox = new BoundingBox(vertices[0].Position, vertices[6].Position);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), verticiesLen, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);
        }

        public void UpdateGravity()
        {
            velocity -= new Vector3(0, velocityDropoff, 0);
            for (int i = 0; i < vertices.Count(); i++)
            {
                vertices[i].Position = vertices[i].Position - new Vector3(0f, gravityVelocity, 0f) + velocity;
            }
            vertexBuffer.SetData<VertexPositionColor>(vertices);
            collisionBox = new BoundingBox(vertices[0].Position, vertices[8].Position);
        }

        public void Freeze()
        {
            velocity = new Vector3(0, 0, 0);
            gravityVelocity = 0.0f;
            velocityDropoff = 0.0f;
        }
    }
}
