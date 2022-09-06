using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;
using Word_Game.Controls;


namespace Word_Game.States
{
    class MenuState : State
    {
        private List<Component> _components;

        private Texture2D menuBGTexture;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Entity/Button");
            var buttonFont = _content.Load<SpriteFont>("Font/Arial");
            

            var playButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Play"
            };
            playButton.Click += Button_Play_Clicked;

            var exitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 450),
                Text = "Exit"
            };
            exitButton.Click += Button_Exit_Clicked;

            var difficultyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Easy Mode"
            };
            difficultyButton.Click += Button_Difficulty_Clicked;

            _components = new List<Component>() { playButton, exitButton, difficultyButton};
        }

        public override void LoadContent()
        {
            menuBGTexture = _content.Load<Texture2D>("Entity/SpaceBackground");
            //var menuBGTexture = background texture to define here;
        }

        private void Button_Play_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            GameState.targetList = GameState.targetList1;
        }

        private void Button_Difficulty_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            GameState.targetList = GameState.targetList2;
        }

        private void Button_Exit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
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

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }


    }
}
