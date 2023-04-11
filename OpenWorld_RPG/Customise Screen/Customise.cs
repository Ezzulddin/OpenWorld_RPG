using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Customise_Screen
{
    public class Customise
    {
        public Texture2D currentTex { get; set; }
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteScale { get; set; }
        public Color spriteColour { get; set; }

        private StreamWriter writer;
        private StreamReader reader;

        public Customise()
        {

        }
        public Customise(Texture2D currentTex, Vector2 spritePosition, Vector2 spriteScale, Color spriteColour)
        {
            this.currentTex = currentTex;
            this.spritePosition = spritePosition;
            this.spriteScale = spriteScale;
            this.spriteColour = spriteColour;
        }
        public Customise Customisation(int hairInt, int shirtInt, Customise player, Vector2 pos, Vector2 size, Texture2D green, Texture2D red,
            Texture2D black, Texture2D blackBlueShirt, Texture2D blackRedShirt, Texture2D greenBlueShirt, Texture2D greenRedShirt,
            Texture2D redBlueShirt, Texture2D redRedShirt)
        {
            if (hairInt == 0 && shirtInt == 0)
            {
                player = new Customise(black, pos, size, Color.White);
            }
            if (hairInt == 1 && shirtInt == 0)
            {
                player = new Customise(green, pos, size, Color.White);
            }
            if (hairInt == 2 && shirtInt == 0)
            {
                player = new Customise(red, pos, size, Color.White);
            }
            if (hairInt == 0 && shirtInt == 1)
            {
                player = new Customise(blackBlueShirt, pos, size, Color.White);
            }
            if (hairInt == 1 && shirtInt == 1)
            {
                player = new Customise(greenBlueShirt, pos, size, Color.White);
            }
            if (hairInt == 2 && shirtInt == 1)
            {
                player = new Customise(redBlueShirt, pos, size, Color.White);
            }
            if (hairInt == 0 && shirtInt == 2)
            {
                player = new Customise(blackRedShirt, pos, size, Color.White);
            }
            if (hairInt == 1 && shirtInt == 2)
            {
                player = new Customise(greenRedShirt, pos, size, Color.White);
            }
            if (hairInt == 2 && shirtInt == 2)
            {
                player = new Customise(redRedShirt, pos, size, Color.White);
            }

            return player;
        }
        public void WriteToFile(Texture2D customTexture)
        {
            writer = File.CreateText("../../../CustomTexture.txt");

            writer.Write(customTexture);

            writer.Close();
        }
        public string ReadFromFile()
        {
            reader = new StreamReader("../CustomTexture.txt");

            string line = reader.ReadLine();

            reader.Close();

            return line;
        }
        public void DrawCharacter(SpriteBatch spriteBatch, Texture2D Tex)
        {
            currentTex = Tex;
            spriteBatch.Begin();

            spriteBatch.Draw(currentTex, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteScale.X, (int)spriteScale.Y), spriteColour);

            spriteBatch.End();
        }
    }
}
