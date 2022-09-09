using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;
using Word_Game.Controls;
using Word_Game.States;
using Word_Game.Sprites;

namespace Word_Game.States
{
    class HighScoreBoard : State
    {
        private List<Component> _components;
        private SpriteFont _font;
        private Texture2D menuBGTexture;
        private Texture2D savedScores;
        private string nameInput, toUpdate;
        KeyboardState newKey, oldKey;
        StringBuilder _Textbox = new StringBuilder();

        public HighScoreBoard(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Entity/Button");
            var buttonFont = _content.Load<SpriteFont>("Font/Arial");

            var saveScore = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Game1.screenWidth/2, Game1.screenHeight - 100),
                Text = "Save Score"
            };
            saveScore.Click += Save_Score_Clicked;

            _components = new List<Component>() {saveScore};

        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;

            if (new Regex(@"[a-zA-Z ]").IsMatch(character.ToString()))
            {
                toUpdate = character.ToString();
            }
        }

        public override void LoadContent()
        {
            menuBGTexture = _content.Load<Texture2D>("Entity/SpaceBackground");
            _font = _content.Load<SpriteFont>("Font/Arial");

            savedScores = new Texture2D(_graphicsDevice, 400, 250);
            Color[] data = new Color[400 * 250];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            savedScores.SetData(data);
        }

        private void Save_Score_Clicked(object sender, EventArgs args)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));

            newKey = Keyboard.GetState();

            if (newKey.IsKeyDown(Keys.Back) && oldKey.IsKeyUp(Keys.Back) && _Textbox.Length >= 1)
            {
                _Textbox.Length--;
                toUpdate = null;
            }

            if (newKey.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
            {

                _Textbox.Clear();
            }
        }

        public override void PostUpdate(GameTime gameTme)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuBGTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(savedScores, new Vector2(100, 300), Color.White);

            using (StreamReader reading = File.OpenText("Scores.txt"))
            {
                string line;
                int i = 0;
                while ((line = reading.ReadLine()) != null)
                {
                    spriteBatch.DrawString(_font, line, new Vector2( 100, 300 + (i * 20)), Color.Black);
                    i++;
                }
            }
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }


    }
}
