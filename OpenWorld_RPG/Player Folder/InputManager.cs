using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OpenWorld_RPG.PlayerFolder
{
    public class InputManager
    {
        KeyboardState state;

        public void CheckKeys(Player pSprite, GraphicsDeviceManager graphics)
        {
            state = Keyboard.GetState();

            pSprite.goingLeft = false;
            pSprite.goingRight = false;
            pSprite.goingUp = false;
            pSprite.goingDown = false;
            pSprite.sprinting = false;

            if (state.IsKeyDown(Keys.A))
            {
                pSprite.goingLeft = true;
            }
            if (state.IsKeyDown(Keys.D))
            {
                pSprite.goingRight = true;
            }
            if (state.IsKeyDown(Keys.W))
            {
                pSprite.goingUp = true;
            }
            if (state.IsKeyDown(Keys.S))
            {
                pSprite.goingDown = true;
            }
            if (state.IsKeyDown(Keys.LeftShift))
            {
                pSprite.sprinting = true;
            }
        }
    }
}
