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
using Word_Game.Sprites;

namespace Word_Game.States
{
    class HighScoreBoard : State
    {
        private List<Component> _components;

        private Texture2D menuBGTexture;
        private string NameInput;

        public HighScoreBoard(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;

            if (new Regex(@"[a-zA-Z ]").IsMatch(character.ToString()))
            {
                NameInput = character.ToString();
            }
        }

        public override void LoadContent()
        {
            menuBGTexture = _content.Load<Texture2D>("Entity/SpaceBackground");
        }

        public override void PostUpdate(GameTime gameTme)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }


    }
}
