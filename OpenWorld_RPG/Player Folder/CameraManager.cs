using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.PlayerFolder
{
    public class CameraManager
    {
        public Matrix Transform { get; set; }
        public void Follow(Player Target)
        {
            Matrix Position = Matrix.CreateTranslation
                 (-Target.spritePosition.X - (Target.spriteSize.X / 2),
                 -Target.spritePosition.Y - (Target.spriteSize.Y / 2)
                 , 0);
            Matrix offset = Matrix.CreateTranslation(
            Game1.screenWidth / 2,
            Game1.screenHeight / 2,
            0);

            Transform = Position * offset;
        }
    }
}
