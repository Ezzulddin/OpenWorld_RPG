using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Customise_Screen
{
    public class TextInputManager
    {
        private Keys[] lastPressedKeys = new Keys[5];

        public string myName { get; set; }
        public SpriteFont sf;
        public Vector2 Position { get; set; }
        
        private StreamWriter writer;
        private StreamReader reader;
        
        public TextInputManager(string name, SpriteFont sf, Vector2 pos)
        {
            this.myName = name;
            this.sf = sf;
            this.Position = pos;
        }
        public void LoadContent(ContentManager Content)
        {
            sf = Content.Load<SpriteFont>("Name");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(sf, myName, Position, Color.Black);
            spriteBatch.End();
        }
        public void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastPressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyUp(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyDown(key);
                }
            }
            lastPressedKeys = pressedKeys;
        }
        public void OnKeyUp(Keys key)
        {

        }
        public void OnKeyDown(Keys key)
        {

            if (key == Keys.Back && myName.Length > 0)
            {
                myName = myName.Remove(myName.Length - 1);
            }
            else if (key == Keys.Space && myName.Length > 0)
            {
                myName = new string(myName + " ");
            }
            else if (key == Keys.LeftShift && myName.Length > 0)
            {
                myName = myName.ToString().ToUpper();
            }
            else
            {
                myName += key.ToString().ToLower();
            }

        }
        public void WriteToFile()
        {
            writer = File.CreateText("../../../Name.txt");

            writer.Write(myName);

            writer.Close();
        }
        public string ReadFromFile()
        {
            reader = new StreamReader("../../../Name.txt");

            string line = reader.ReadLine();

            reader.Close();

            return line;
        }
    }
}
