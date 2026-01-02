using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeMonoGame.Core.Models;
using System;

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
        private Rectangle[] _buttonRectangle;
        private int _frameSize = 50;
        private GameField _field;
        private int _needFrame = 0;
        private VariableMove _currentMove = VariableMove.Rigth;
        private int _speedSnake = 12;
        private int _cuurentSpeedSnake = 0;
        private bool _isEatApple = false;
        private bool _isEnableLoseWindow = false;
        private bool _isEnableWinWindows = false;
        private bool _isEnableMenu = false;
        private bool _isGameStop = true;
        private MouseState _currentClick;
        private MouseState _previousClick;
        private float _keyPressCooldown = 0f;
        private KeyboardState _previousKey;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 800;
            _frames = new Rectangle[7];
            _framesMenu = new Rectangle[8];
            _buttonRectangle = new Rectangle[5];
            _field = new GameField();
        }

        protected override void Initialize()
        {
            _previousClick = Mouse.GetState();
            _previousKey = Keyboard.GetState();
            CreateFrame();
            CreateRectangleMenu();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("texturesGame");
            _textureMenu = Content.Load<Texture2D>("menuSprite");
            _font = Content.Load<SpriteFont>("Arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (_keyPressCooldown > 0)
            {
                _keyPressCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if (_keyPressCooldown <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.W) && _previousKey.IsKeyUp(Keys.W))
                {
                    if (_isGameStop && !_isEnableMenu && !_isEnableLoseWindow && !_isEnableWinWindows)
                        _isGameStop = false;

                    if (_currentMove != VariableMove.Up && _currentMove != VariableMove.Down)
                    {
                        _currentMove = VariableMove.Up;
                        _keyPressCooldown = 0.2f;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.A) && _previousKey.IsKeyUp(Keys.A))
                {
                    if (_isGameStop && !_isEnableMenu && !_isEnableLoseWindow && !_isEnableWinWindows)
                        _isGameStop = false;

                    if (_currentMove != VariableMove.Left && _currentMove != VariableMove.Rigth)
                    {
                        _currentMove = VariableMove.Left;
                        _keyPressCooldown = 0.2f;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.S) && _previousKey.IsKeyUp(Keys.S))
                {
                    if (_isGameStop && !_isEnableMenu && !_isEnableLoseWindow && !_isEnableWinWindows)
                        _isGameStop = false;
                    if (_currentMove != VariableMove.Down && _currentMove != VariableMove.Up)
                    {
                        _currentMove = VariableMove.Down;
                        _keyPressCooldown = 0.2f;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.D) && _previousKey.IsKeyUp(Keys.D))
                {
                    if (_isGameStop && !_isEnableMenu && !_isEnableLoseWindow && !_isEnableWinWindows)
                        _isGameStop = false;
                    if (_currentMove != VariableMove.Rigth && _currentMove != VariableMove.Left)
                    {
                        _currentMove = VariableMove.Rigth;
                        _keyPressCooldown = 0.2f;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.R) && _previousKey.IsKeyUp(Keys.R))
                {
                    _field.RestartGame();
                    _currentMove = VariableMove.Rigth;
                    _keyPressCooldown = 0.5f;
                }
                else if (keyboardState.IsKeyDown(Keys.M) && _previousKey.IsKeyUp(Keys.M))
                {
                    _isEnableMenu = !_isEnableMenu;
                    _isGameStop = true;
                    _keyPressCooldown = 0.5f;
                }
            }

            _previousClick = _currentClick;
            _currentClick = Mouse.GetState();
            if (_currentClick.LeftButton == ButtonState.Pressed &&
                _previousClick.LeftButton == ButtonState.Released)
            {
                if (_buttonRectangle[0].Contains(_currentClick.X, _currentClick.Y))
                {
                    _isEnableMenu = !_isEnableMenu;
                    _isGameStop = true;
                }
                if (_isEnableLoseWindow && _buttonRectangle[4].Contains(_currentClick.X, _currentClick.Y))
                {
                    _field.RestartGame();
                    _isEnableLoseWindow = false;
                    _isGameStop = true;
                }
                if (_isEnableWinWindows && _buttonRectangle[4].Contains(_currentClick.X, _currentClick.Y))
                {
                    _field.RestartGame();
                    _isEnableWinWindows = false;
                    _isGameStop = true;
                }
                if(_isEnableMenu)
                {
                    if (_buttonRectangle[1].Contains(_currentClick.X, _currentClick.Y))
                    {
                        _field.NeedAppleToWin = 20;
                        _speedSnake = 12;
                        _cuurentSpeedSnake = 0;
                        _currentMove = VariableMove.Rigth;
                        _isEnableMenu = false;
                        _field.RestartGame();
                    }
                    if (_buttonRectangle[2].Contains(_currentClick.X, _currentClick.Y))
                    {
                        _field.NeedAppleToWin = 40;
                        _speedSnake = 9;
                        _cuurentSpeedSnake = 0;
                        _currentMove = VariableMove.Rigth;
                        _isEnableMenu = false;
                        _field.RestartGame();
                    }
                    if (_buttonRectangle[3].Contains(_currentClick.X, _currentClick.Y))
                    {
                        _field.NeedAppleToWin = 50;
                        _speedSnake = 7;
                        _cuurentSpeedSnake = 0;
                        _currentMove = VariableMove.Rigth;
                        _isEnableMenu = false;
                        _field.RestartGame();
                    }
                }
            }
            if (_isGameStop) return;
            if (_cuurentSpeedSnake == _speedSnake)
            {
                if (_field.CheckMoveSnake(_currentMove))
                {
                    _isEatApple = CollideApple();
                    _field.MoveSnake(_currentMove, _isEatApple);
                    if (_isEatApple) _field.GenerateApple();
                    _cuurentSpeedSnake = 0;
                    _isEatApple = false;
                    if(_field.CheckWin())
                    {
                        _isEnableWinWindows = true;
                        _isGameStop = true;
                        _currentMove = VariableMove.Rigth;
                    }
                }
                else
                {
                    _isEnableLoseWindow = true;
                    _isGameStop = true;
                    _currentMove = VariableMove.Rigth;
                }
            }
            else
            {
                _cuurentSpeedSnake++;
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

            _spriteBatch.Draw(_textureMenu, new Vector2(_buttonRectangle[0].X, _buttonRectangle[0].Y),
                _framesMenu[4], Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            _spriteBatch.DrawString(_font, $"Счет: {_field.SizeSnake}", 
                new Vector2(502, 602), Color.Black);
            _spriteBatch.DrawString(_font, $"Счет: {_field.SizeSnake}", 
                new Vector2(500, 600), Color.Gold);

            if(_isGameStop)
            {
                _spriteBatch.DrawString(_font, "Нажмите WASD для старта", 
                    new Vector2(202, 152), Color.Black);
                _spriteBatch.DrawString(_font, "Нажмите WASD для старта",
                    new Vector2(200, 150), Color.Gold);
                _spriteBatch.DrawString(_font, $"Нужно набрать {_field.NeedAppleToWin} яблок", 
                    new Vector2(202, 182), Color.Black);
                _spriteBatch.DrawString(_font, $"Нужно набрать {_field.NeedAppleToWin} яблок",
                    new Vector2(200, 180), Color.Gold);
            }

            if(_isEnableLoseWindow)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(100, 100), _framesMenu[1], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(200, 300), _framesMenu[3], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if(_isEnableWinWindows)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(100, 100), _framesMenu[2], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(200, 300), _framesMenu[3], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if(_isEnableMenu)
            {
                _spriteBatch.Draw(_textureMenu, new Vector2(50, 100), _framesMenu[0], Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(_buttonRectangle[1].X, _buttonRectangle[1].Y),
                    _framesMenu[5], Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(_buttonRectangle[2].X, _buttonRectangle[2].Y),
                    _framesMenu[6], Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                _spriteBatch.Draw(_textureMenu, new Vector2(_buttonRectangle[3].X, _buttonRectangle[3].Y),
                    _framesMenu[7], Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
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

            _buttonRectangle[0] = new Rectangle(40, 615, 300, 100);
            _buttonRectangle[1] = new Rectangle(150, 200, 200, 75);
            _buttonRectangle[2] = new Rectangle(150, 300, 200, 75);
            _buttonRectangle[3] = new Rectangle(150, 400, 200, 75);
            _buttonRectangle[4] = new Rectangle(200, 300, 300, 100);
        }

        protected void CreateFrame()
        {
            Point currentFrame = new Point(0, 0);
            Point spriteSize = new Point(7, 1);
            for (int i = 0; currentFrame.X < spriteSize.X; i++)
            {
                _frames[i] = new Rectangle(currentFrame.X * _frameSize, currentFrame.Y * _frameSize,
                    _frameSize, _frameSize);
                currentFrame.X++;
            }
        }
    }
}
