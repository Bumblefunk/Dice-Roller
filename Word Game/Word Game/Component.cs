using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;
using Word_Game.States;

namespace Word_Game
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gametime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
