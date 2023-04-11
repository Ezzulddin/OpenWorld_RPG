using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using OpenWorld_RPG.PlayerFolder;
using Microsoft.Xna.Framework;

namespace OpenWorld_RPG.Json_File
{
    public class JsonFile
    {
        public int swordAmount { get; set; }
        public string weaponName { get; set; }
        public int potionAmount { get; set; }
        public int currenyAmount { get; set; }

        private const string PATH = "items.json";
        public JsonFile Inventory(JsonFile _items,int sword, int potion)
        {
            _items = new JsonFile()
            {
                swordAmount = sword,
                potionAmount = potion,
            };
            Save(_items);
            return _items;
        }
        public JsonFile CurrencySystem(JsonFile _currency,int money)
        {
            _currency = new JsonFile()
            {
                currenyAmount = money,
            };
            Save(_currency);
            return _currency;
        }
        private void Save(JsonFile file)
        {
            string serializedText = JsonSerializer.Serialize<JsonFile>(file);
            Trace.WriteLine(serializedText);
            File.WriteAllText(PATH, serializedText);
        }
        public JsonFile Load()
        {
            string fileContents = File.ReadAllText(PATH);
            return JsonSerializer.Deserialize<JsonFile>(fileContents);
        }
        public void CurrencyCalculator(JsonFile _Currency,bool mKilled,bool bought,int moneyInt,TextManager tManager,string money, SpriteFont currencyFont)
        {
            //_Currency = Load();
            
            if (mKilled)
            {
                moneyInt += 50;
                tManager.textContent = moneyInt.ToString();
                //Save(_Currency);
            }
            if(bought)
            {
                moneyInt = moneyInt- 50;
                tManager.textContent = moneyInt.ToString();
                //Save(_Currency);
            }
            tManager = new TextManager(new Vector2(1900, 0), money, currencyFont);
        }
        //public void ItemCalculator()
        //{
        //    if (Keyboard.GetState().IsKeyDown(Keys.I))
        //    {
        //        swordInt = 1;
        //        items.Inventory(items, swordInt, potionInt);
        //    }
        //}
        public void DisplayOnScreen(JsonFile _items, JsonFile _currency,Player p, SpriteBatch spriteBatch,CameraManager cManager ,Sprite sword, Sprite potion, Texture2D emptyTex)
        {
            _items = Load();
            
            if(_items.swordAmount > 0)
            {
                sword = new Sprite(sword.spriteTexture, new Vector2(p.spritePosition.X - 800, p.spritePosition.Y-400), sword.spriteSize, sword.spriteColour);
                sword.DrawSprite(spriteBatch, sword.spriteTexture, cManager);
            }
            if (_items.potionAmount > 0)
            {
                potion = new Sprite(potion.spriteTexture, new Vector2(p.spritePosition.X - 600, p.spritePosition.Y - 400), potion.spriteSize, potion.spriteColour);
                potion.DrawSprite(spriteBatch, potion.spriteTexture, cManager);
            }
        }
    }
}
