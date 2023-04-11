using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace OpenWorld_RPG.Map
{
    public class Tile : Sprite
    {

        public Tile(Texture2D inTexture, Vector2 inPosition, Vector2 inSize, Color color)
            : base(inTexture, inPosition, inSize, color)
        {
        }
        
    }
}
