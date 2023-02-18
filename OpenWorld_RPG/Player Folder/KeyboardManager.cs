using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.PlayerFolder
{
    public class KeyboardManager
    {
        private float speed = 2.5f;
        private float sprintingSpeed = 3.5f;
        
        public void movement(Player p)
        {
            if (p.goingUp)
            {
                goUp(p);
            }
            if (p.goingDown)
            {
                goDown(p);
            }
            if (p.goingLeft)
            {
                goLeft(p);
            }
            if (p.goingRight)
            {
                goRight(p);
            }
            if (p.sprinting)
            {
                goSprinting(p);
            }
        }

        public void goLeft(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X - speed, playerSprite.spritePosition.Y);
        }
        public void goRight(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X + speed, playerSprite.spritePosition.Y);
        }
        public void goUp(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y - speed);
        }
        public void goDown(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y + speed);
        }

        public void goSprinting(Player playerSprite)
        {
            if (playerSprite.goingRight)
            {
                playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X + sprintingSpeed, playerSprite.spritePosition.Y);
            }
            if (playerSprite.goingLeft)
            {
                playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X - sprintingSpeed, playerSprite.spritePosition.Y);
            }
            if (playerSprite.goingUp)
            {
                playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y - sprintingSpeed);
            }
            if (playerSprite.goingDown)
            {
                playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y + sprintingSpeed);
            }
        }
    }
}
