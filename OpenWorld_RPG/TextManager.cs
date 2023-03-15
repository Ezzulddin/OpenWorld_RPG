using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG
{
    public class TextManager
    {
        public Vector2 textPosition { get; set; }
        public string textContent { get; set; }
        public SpriteFont font { get; set;}

        public TextManager() { }

        public TextManager(Vector2 textPosition, string textContent, SpriteFont font)
        {
            this.textPosition = textPosition;
            this.textContent = textContent;
            this.font = font;
        }
        public void DrawText(SpriteBatch s)
        {
            s.Begin();
            s.DrawString(font, textContent, textPosition, Color.White);
            s.End();
        }
    }
}
