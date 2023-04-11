using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using OpenWorld_RPG.PlayerFolder;

namespace OpenWorld_RPG
{
    public class Sprite
    {
        public Texture2D spriteTexture { get; set; }
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteSize { get; set; }
        public Color spriteColour { get; set; }

        public Sprite(Texture2D tex, Vector2 pos, Vector2 size, Color colour)
        {
            this.spriteTexture = tex;
            this.spritePosition = pos;
            this.spriteSize = size;
            this.spriteColour = colour;
        }

        public void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, CameraManager camera)
        {
            spriteTexture = texture;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                null, null, null, transformMatrix: camera.Transform);
            
            spriteBatch.Draw(spriteTexture, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), spriteColour);
            
            spriteBatch.End();
        }
      //  public void AnimateSprite(SpriteBatch spriteBatch, Texture2D texture, CameraManager camera, Rectangle[,] aRectnagle, int direct, int currentAnimationIndex, Player p)
      //  {
      //      spriteBatch.Begin(SpriteSortMode.Deferred,
      //BlendState.AlphaBlend,
      //SamplerState.PointClamp,
      //null, null, null, transformMatrix: camera.Transform);
      //      spriteBatch.Draw(texture, new Rectangle((int)p.spritePosition.X, (int)p.spritePosition.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y), aRectnagle[direct, currentAnimationIndex], Color.White);
      //      spriteBatch.End();
      //  }
    }
}
