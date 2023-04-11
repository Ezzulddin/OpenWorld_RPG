using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG
{
    public class Title
    {
        public Texture2D tex { get; set; }
        public Vector2 pos { get; set; }
        public Vector2 size { get; set; }
        public Color colour { get; set; }

        public Title(Texture2D tex, Vector2 pos, Vector2 size, Color colour)
        {
            this.tex = tex;
            this.pos = pos;
            this.size = size;
            this.colour = colour;
        }

        public void DrawSprite(SpriteBatch spriteBatch,Texture2D texture)
        {
            texture = tex;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), colour);
            spriteBatch.End();
        }

    }
}
