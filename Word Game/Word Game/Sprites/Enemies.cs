using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Word_Game.States;

namespace Word_Game.Sprites
{
    class Enemies
    {
        public SpriteFont font;
        public Vector2 position;
        public Vector2 velocity;
        public string target;
        public string onTarget = "";
        public bool isVisable = true;
        public bool isShot = false;

        Random random = new Random();

        int positionX, positionY;
 
        public Enemies(SpriteFont newFont, Vector2 newPosition, string targetWord)
        {
            font = newFont;
            position = newPosition;
            target = targetWord;

            positionX = random.Next(-3, 3);
            positionY = random.Next(1, 3);
            

            velocity = new Vector2(positionX, positionY);

        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.Y <= 0 || position.Y >= (graphics.Viewport.Height - 200) - (font.MeasureString(target).Y))
                isVisable = false;

            if (position.X <= 0 || position.X >= graphics.Viewport.Width - (font.MeasureString(target).X))
                velocity.X = -velocity.X;

            int index = target.Length - GameState.currentString.Length;
            if (target.Contains(GameState.currentString, StringComparison.OrdinalIgnoreCase))
            {
                onTarget = target.Remove(GameState.currentString.Length, index);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, target, position, Color.White);
            spriteBatch.DrawString(font, onTarget, position, Color.LimeGreen);
            if(!target.Contains(GameState.currentString, StringComparison.OrdinalIgnoreCase))
            {
                spriteBatch.DrawString(font, onTarget, position, Color.Red);
            }
        }
    }
}
