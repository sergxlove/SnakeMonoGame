using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeMonoGame.Core.Models;

namespace SnakeMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Vector2 _position = new Vector2(0, 0);
        private Rectangle[] _frames;
        private int _frameSize = 50;
        private Point _currentFrame = new Point(0, 0);
        private Point _spriteSize = new Point(7, 1);
        private GameField _field;
        private int _needFrame = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _frames = new Rectangle[7];
            _field = new GameField();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("texturesGame");

            for (int i = 0; _currentFrame.X < _spriteSize.X; i++)
            {
                _frames[i] = new Rectangle(_currentFrame.X * _frameSize, _currentFrame.Y * _frameSize, 
                    _frameSize, _frameSize);
                _currentFrame.X++;
            }

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            foreach (var key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.W:
                        break;
                    case Keys.A:
                        break;
                    case Keys.S:
                        break;
                    case Keys.D:
                        break;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _position.X = 0;
            _position.Y = 0;
            for(int i = 0; i < _field.Field.GetLength(0); i++ )
            {
                for(int j = 0; j < _field.Field.GetLength(1); j++ )
                {
                    _needFrame = GameObjectToInt(_field.Field[i, j]);
                    _spriteBatch.Draw(_texture, _position, _frames[_needFrame], Color.White, 0, 
                        Vector2.Zero, 1, SpriteEffects.None, 0);
                    _position.X += _frameSize;
                }
                _position.X = 0;
                _position.Y += _frameSize; 
            }

            _needFrame = 3;
            _position.X = _field.Snake[0].X * _frameSize;
            _position.Y = _field.Snake[0].Y * _frameSize;
            _spriteBatch.Draw(_texture, _position, _frames[_needFrame], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
            _needFrame = 4;
            for (int i = 1; i < _field.Snake.Count; i++)
            {
                _position.X = _field.Snake[i].X * _frameSize;
                _position.Y = _field.Snake[i].Y * _frameSize;
                _spriteBatch.Draw(_texture, _position, _frames[_needFrame], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            _needFrame = 6;
            _position.X = _field.Apple.X * _frameSize;
            _position.Y = _field.Apple.Y * _frameSize;
            _spriteBatch.Draw(_texture, _position, _frames[_needFrame], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private int GameObjectToInt(GameObject gameObject)
        {
            switch (gameObject)
            {
                case GameObject.LightGreen:
                    return 0;
                case GameObject.DarkGreen:
                    return 1;
                case GameObject.Wall:
                    return 2;
                case GameObject.SnakeHead:
                    return 3;
                case GameObject.SnakeBody:
                    return 4;
                case GameObject.SnakeEnd:
                    return 5;
                case GameObject.Apple:
                    return 6;
                default:
                    return 0;
            }
        }
    }
}
