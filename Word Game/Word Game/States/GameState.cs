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
    public class GameState : State
    {
        private SpriteFont _font;
        private string toUpdate, targetWord;
        private float spawn = 0;
        private float timer = 0;
        private int totalspawn = 1;
        private Texture2D shipTexture;
        private Texture2D healthBox;
        private Vector2 shipPosition;
        KeyboardState newKey, oldKey;
        Scrolling scrolling1;
        Scrolling scrolling2;

        public static string currentString = "";
        public static int screenWidth = 600;
        public static int screenHeight = 720;
        public static int score = 0;
        public int healthCount = 3;

        public static List<string> targetList1 = new List<string>() { "Hello World", "Hi Orb", "Sup Earth", "Howdy Dirt", "Greetings Earthling"};
        public static List<string> targetList2 = new List<string>() { "Hello", "Hi", "Sup", "Howdy", "Meowdy", "HiHi"};
        public static List<string> targetList = new List<string>();

        List<Sprites.Enemies> enemy = new List<Sprites.Enemies>();
        StringBuilder _Textbox = new StringBuilder();

        public static Random random;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            random = new Random();

            game.Window.TextInput += TextInputHandler;
            var ship = new Sprites.Ship(shipTexture, shipPosition);
        }

        public override void LoadContent()
        {
            shipTexture = _content.Load<Texture2D>("Entity/spaceship placeholder");
            shipPosition = new Vector2(screenWidth / 2, screenHeight);
            _font = _content.Load<SpriteFont>("Font/Arial");

            healthBox = new Texture2D(_graphicsDevice, 50, 30);
            Color[] data = new Color[50 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            healthBox.SetData(data);

            scrolling1 = new Scrolling(_content.Load<Texture2D>("Entity/SpaceBackground"), new Rectangle(0, 0, 600, 750));
            scrolling2 = new Scrolling(_content.Load<Texture2D>("Entity/SpaceBackground2"), new Rectangle(0, -600, 600, 750));
            //load in scores here - maybe the enemies too - backgrounds ect
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));

            if (healthCount == 0)
                _game.ChangeState(new HighScoreBoard(_game, _graphicsDevice, _content));

            if (scrolling1.rectangle.Y - scrolling1.texture.Height >= -200)
                scrolling1.rectangle.Y = scrolling2.rectangle.Y - scrolling2.texture.Height;
            if (scrolling2.rectangle.Y - scrolling2.texture.Height >= -200)
                scrolling2.rectangle.Y = scrolling1.rectangle.Y - scrolling1.texture.Height;

            scrolling1.Update();
            scrolling2.Update();

            newKey = Keyboard.GetState();

            if (newKey.IsKeyDown(Keys.Back) && oldKey.IsKeyUp(Keys.Back) && _Textbox.Length >= 1)
            {
                _Textbox.Length--;
                toUpdate = null;
                currentString = _Textbox.ToString();
            }


            if (newKey.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
            {
                IEnumerable<Sprites.Enemies> match = enemy.Where(enemy => enemy.target.Equals(_Textbox.ToString(), StringComparison.OrdinalIgnoreCase));

                foreach (Sprites.Enemies enemy in match)
                {
                    enemy.isShot = true;
                    enemy.isVisable = false;
                    currentString = "";
                    score += enemy.target.Length;
                }
                _Textbox.Clear();
            }

            if (toUpdate != null)
            {
                _Textbox.Append(toUpdate);
                toUpdate = null;
                currentString = _Textbox.ToString();
            }
            oldKey = newKey;

            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Sprites.Enemies enemy in enemy)
                enemy.Update(_graphicsDevice);

            LoadEnemies();
            PostUpdate(gameTime);
            //update scores here - maybe the enemies too - backgrounds ect
        }

        public void LoadEnemies()
        {
            
            int randWord = random.Next(targetList.Count);
            targetWord = targetList[randWord];
            int positionBuffer = Convert.ToInt32(_font.MeasureString(targetWord).X);
            int positionX = random.Next(0 + positionBuffer, 600 - positionBuffer);

            if (timer >= 60)
            {
                timer = 0;
                totalspawn++;
            }

            if (spawn >= 1)
            {
                spawn = 0;
                if (enemy.Count < totalspawn)
                    enemy.Add(new Sprites.Enemies(_font, new Vector2(positionX, 1), targetWord));
            }

            IEnumerable<Sprites.Enemies> attacked = enemy.Where(enemy => !enemy.isShot && !enemy.isVisable);

            foreach (Sprites.Enemies enemy in attacked)
                healthCount--;

            enemy.RemoveAll(enemy => !enemy.isVisable);
        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;

            if (new Regex(@"[a-zA-Z ]").IsMatch(character.ToString()))
                toUpdate = character.ToString();
        }

        public override void PostUpdate(GameTime gameTme)
        {
            //where you put the unloads, the .Removesat/.RemoveAlls ect
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            spriteBatch.Draw(shipTexture, shipPosition, null, Color.White, 0f, new Vector2(shipTexture.Width / 2, shipTexture.Height), Vector2.One, SpriteEffects.None, 0f);
            
            foreach (Sprites.Enemies enemy in enemy)
                enemy.Draw(spriteBatch);
            spriteBatch.DrawString(_font, _Textbox, new Vector2(10, screenHeight - (shipTexture.Height -40)), Color.Red);
            spriteBatch.DrawString(_font, score.ToString(), new Vector2(10, screenHeight - (shipTexture.Height/2)), Color.Purple);
            for (int i = 0; i < healthCount; i++)
            {
                spriteBatch.Draw(healthBox, new Vector2(50 + (i * 100), screenHeight - shipTexture.Height), Color.Red);
            }
            spriteBatch.End();
        }
    }
}
