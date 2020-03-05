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
        public VertexPositionTexture[] Vertexts;
        public Floor()
        {
            Vertexts = new VertexPositionTexture[6];
            Vertexts[0].Position = new Vector3(-20, -20, 0);
            Vertexts[1].Position = new Vector3(-20, 20, 0);
            Vertexts[2].Position = new Vector3(20, -20, 0);
            Vertexts[3].Position = Vertexts[1].Position;
            Vertexts[4].Position = new Vector3(20, 20, 0);
            Vertexts[5].Position = Vertexts[2].Position;
        }
    }
}
