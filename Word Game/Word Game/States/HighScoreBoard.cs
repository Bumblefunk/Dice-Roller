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
        private Texture2D toSave;
        private string toUpdate;
        private int playerScore = GameState.score;
        KeyboardState newKey, oldKey;
        StringBuilder _Textbox = new StringBuilder();

        public HighScoreBoard(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Entity/Button");
            var buttonFont = _content.Load<SpriteFont>("Font/Arial");
            game.Window.TextInput += TextInputHandler;

            var saveScore = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Game1.screenWidth/2, Game1.screenHeight - 100),
                Text = "Save and Exit"
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
            Color[] scoreColorData = new Color[400 * 250];
            for (int i = 0; i < scoreColorData.Length; ++i) scoreColorData[i] = Color.White;
            savedScores.SetData(scoreColorData);

            toSave = new Texture2D(_graphicsDevice, 400, 100);
            Color[] saveColorData = new Color[400 * 100];
            for (int i = 0; i < saveColorData.Length; ++i) saveColorData[i] = Color.White;
            toSave.SetData(saveColorData);
        }

        private void Save_Score_Clicked(object sender, EventArgs args)
        {
            if(playerScore != 0)
            {
                using (StreamWriter writing = File.AppendText("Scores.txt"))
                {
                    writing.WriteLine(_Textbox.ToString() + " : " + playerScore.ToString());
                }
            }
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
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
                using (StreamWriter writing = File.AppendText("Scores.txt"))
                {
                    writing.WriteLineAsync(_Textbox.ToString() + " : " + playerScore.ToString());
                }
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }

            if (toUpdate != null)
            {
                _Textbox.Append(toUpdate);
                toUpdate = null;
            }
            oldKey = newKey;

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTme)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuBGTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(savedScores, new Vector2(100, 300), Color.White);
            spriteBatch.Draw(toSave, new Vector2(100, 100), Color.White);
            if (playerScore == 0)
            {
                spriteBatch.DrawString(_font, "Below are scores previously saved", new Vector2(100, 100), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(_font, "Please Type your name to save your score!", new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(_font, "Your score was: " + playerScore.ToString(), new Vector2(100, 120), Color.Black);
                spriteBatch.DrawString(_font, "Player name: " + _Textbox, new Vector2(100, 140), Color.Black);
            }
            using (StreamReader reading = File.OpenText("Scores.txt"))
            {
                string line;
                int i = 0;
                while ((line = reading.ReadLine()) != null && i < 10)
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
