using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;

namespace Word_Game.Sprites
{
    class Ship
    {
        public Texture2D texture;
        public Vector2 position;

        public Ship(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
        }

        public void Update(GraphicsDevice graphics)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}