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
        private SpriteFont _font;
        private Texture2D _texture;
        private Texture2D _textureMenu;
        private Vector2 _position = new Vector2(0, 0);
        private Rectangle[] _frames;
        private Rectangle[] _framesMenu;
        private int _frameSize = 50;
        private Point _currentFrame = new Point(0, 0);
        private Point _spriteSize = new Point(7, 1);
        private GameField _field;
        private int _needFrame = 0;
        private VariableMove _currentMove = VariableMove.Rigth;
        private int _speedSnake = 7;
        private int _cuurentSpeedSnake = 0;
        private bool _isEatApple = false;
        private bool _isEnableLoseWindow = false;
        private bool _isEnableWinWindows = false;
        private bool _isEnableMenu = false;
        private bool _isGameStop = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 800;
            _frames = new Rectangle[7];
            _framesMenu = new Rectangle[8];
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
            _textureMenu = Content.Load<Texture2D>("menuSprite");
            _font = Content.Load<SpriteFont>("Arial");

            for (int i = 0; _currentFrame.X < _spriteSize.X; i++)
            {
                _frames[i] = new Rectangle(_currentFrame.X * _frameSize, _currentFrame.Y * _frameSize, 
                    _frameSize, _frameSize);
                _currentFrame.X++;
            }
            CreateRectangleMenu();
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
                        if (_isGameStop) _isGameStop = false;
                        if (_currentMove == VariableMove.Up) continue;
                        if (_currentMove == VariableMove.Down) continue;
                        _currentMove = VariableMove.Up;
                        break;
                    case Keys.A:
                        if (_isGameStop) _isGameStop = false;
                        if (_currentMove == VariableMove.Left) continue;
                        if (_currentMove == VariableMove.Rigth) continue;
                        _currentMove = VariableMove.Left;
                        break;
                    case Keys.S:
                        if (_isGameStop) _isGameStop = false;
                        if (_currentMove == VariableMove.Down) continue;
                        if (_currentMove == VariableMove.Up) continue;
                        _currentMove = VariableMove.Down;
                        break;
                    case Keys.D:
                        if (_isGameStop) _isGameStop = false;
                        if (_currentMove == VariableMove.Rigth) continue;
                        if (_currentMove == VariableMove.Left) continue;
                        _currentMove = VariableMove.Rigth;
                        break;
                    case Keys.R:
                        _field.RestartGame();
                        _currentMove = VariableMove.Rigth;
                        break;
                }
            }
            if (_isGameStop) return;
            if (_field.CheckMoveSnake(_currentMove))
            {
                if (_cuurentSpeedSnake == _speedSnake)
                {
                    _isEatApple = CollideApple();
                    _field.MoveSnake(_currentMove, _isEatApple);
                    if (_isEatApple) _field.GenerateApple();
                    _cuurentSpeedSnake = 0;
                    _isEatApple = false;
                }
                else
                {
                    _cuurentSpeedSnake++;
                }
            }
            else
            {
                _isEnableLoseWindow = true;
            }
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_framesMenu[4].Contains(mouseState.X, mouseState.Y)) _isEnableMenu = true;
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

            _needFrame = 6;
            _position.X = _field.Apple.X * _frameSize;
            _position.Y = _field.Apple.Y * _frameSize;
            _spriteBatch.Draw(_texture, _position, _frames[_needFrame], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);

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

            _spriteBatch.Draw(_textureMenu, new Vector2(100, 630), _framesMenu[4], Color.White,
                0, Vector2.Zero, 1, SpriteEffects.None, 0);

            _spriteBatch.DrawString(_font, $"Счет: {_field.SizeSnake}", 
                new Vector2(502, 602), Color.Black);
            _spriteBatch.DrawString(_font, $"Счет: {_field.SizeSnake}", 
                new Vector2(500, 600), Color.Gold);
            if(_isGameStop)
            {
                _spriteBatch.DrawString(_font, "Нажмите WASD для старта", 
                    new Vector2(200, 200), Color.White);
            }

            if(_isEnableLoseWindow)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(100, 100), _framesMenu[1], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(200, 100), _framesMenu[3], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if(_isEnableWinWindows)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(100, 100), _framesMenu[2], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if(_isEnableMenu)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(100, 100), _framesMenu[0], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(150, 250), _framesMenu[5], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(150, 350), _framesMenu[6], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(150, 450), _framesMenu[7], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected int GameObjectToInt(GameObject gameObject)
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

        protected bool CollideApple()
        {
            Rectangle headSnakeRectangle = new Rectangle(_field.Snake[0].X * _frameSize, 
                _field.Snake[0].Y * _frameSize, _frameSize, _frameSize);
            Rectangle appleRectangle = new Rectangle(_field.Apple.X * _frameSize,
                _field.Apple.Y * _frameSize, _frameSize, _frameSize);
            return headSnakeRectangle.Intersects(appleRectangle);

        }

        protected void CreateRectangleMenu()
        {
            _framesMenu[0] = new Rectangle(0, 0, 600, 450);
            _framesMenu[1] = new Rectangle(0, 450, 500, 350);
            _framesMenu[2] = new Rectangle(0, 800, 500, 350);
            _framesMenu[3] = new Rectangle(600, 0, 300, 100);
            _framesMenu[4] = new Rectangle(600, 100, 300, 100);
            _framesMenu[5] = new Rectangle(600, 200, 200, 75);
            _framesMenu[6] = new Rectangle(600, 275, 200, 75);
            _framesMenu[7] = new Rectangle(600, 350, 200, 75);
        }
    }
}
