using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace Word_Game.Controls
{
    class Button : Component
    {
        private MouseState _newMouse;

        private SpriteFont _font;

        private bool isHovering;

        private MouseState _oldMouse;

        private Texture2D _texture;

        public EventHandler Click;

        public bool Clicked { get; private set; }

        public float Layers { get; set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public string Text;

        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - ((int)Origin.X), (int)Position.Y - ((int)Origin.Y), _texture.Width, _texture.Height);
            }
        }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if(isHovering)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(_texture, Position, null, colour, 0f, Origin, 1f, SpriteEffects.None, Layers);

            if(!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layers + 0.01f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _oldMouse = _newMouse;
            _newMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_newMouse.X, _newMouse.Y, 1, 1);
            isHovering = false;

            if(mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if(_newMouse.LeftButton == ButtonState.Released && _oldMouse.LeftButton == ButtonState.Pressed)
                {
                    Click.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
