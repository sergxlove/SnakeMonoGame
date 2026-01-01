using GO = SnakeMonoGame.Core.Models.GameObject;

namespace SnakeMonoGame.Core.Models
{
    public class GameField
    {
        public GameObject[,] Field { get; set; } = new GameObject[12, 14]
        {
            {(GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)3 },
            {(GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3 },
        };

        public List<PairCoordinate> Snake { get; set; } = new List<PairCoordinate>()
        { 
            new(4, 5), 
            new(3, 5),
            new(2, 5)
        };
        public PairCoordinate Apple { get; set; } = new PairCoordinate(10, 5);
        public int SizeSnake { get; set; } = 3;
        public int NeedAppleToWin { get; set; } = 20;
        private Random _random = new Random();

        public void MoveSnake(VariableMove move, bool isEatApple)
        {
            List<PairCoordinate> newSnake = new List<PairCoordinate>(Snake.Count);
            PairCoordinate positionHead;
            switch (move)
            {
                case VariableMove.Up:
                    positionHead = new PairCoordinate(Snake[0].X, Snake[0].Y - 1);
                    break;
                case VariableMove.Down:
                    positionHead = new PairCoordinate(Snake[0].X, Snake[0].Y + 1);
                    break;
                case VariableMove.Left:
                    positionHead = new PairCoordinate(Snake[0].X - 1, Snake[0].Y);
                    break;
                case VariableMove.Rigth:
                    positionHead = new PairCoordinate(Snake[0].X + 1, Snake[0].Y);
                    break;
                default:
                    positionHead = new PairCoordinate(0, 0);
                    break;
            }
            newSnake.Add(positionHead);
            if(isEatApple)
            {
                newSnake.AddRange(Snake);
                SizeSnake++;
            }
            else
            {
                for (int i = 0; newSnake.Count != Snake.Count; i++)
                {
                    newSnake.Add(Snake[i]);
                }
            }
            Snake = newSnake;
        }

        public bool CheckMoveSnake(VariableMove move)
        {
            PairCoordinate positionHead;
            switch (move)
            {
                case VariableMove.Up:
                    positionHead = new PairCoordinate(Snake[0].X, Snake[0].Y - 1);
                    break;
                case VariableMove.Down:
                    positionHead = new PairCoordinate(Snake[0].X, Snake[0].Y + 1);
                    break;
                case VariableMove.Left:
                    positionHead = new PairCoordinate(Snake[0].X - 1, Snake[0].Y);
                    break;
                case VariableMove.Rigth:
                    positionHead = new PairCoordinate(Snake[0].X + 1, Snake[0].Y);
                    break;
                default:
                    positionHead = new PairCoordinate(0, 0);
                    break;
            }
            if (Field[positionHead.Y, positionHead.X] == GameObject.Wall) return false;
            foreach(PairCoordinate pr in  Snake)
            {
                if (pr.X == positionHead.X && pr.Y == positionHead.Y) return false;
            }
            return true;
        }

        public bool CheckWin()
        {
            if (SizeSnake >= NeedAppleToWin) return true;
            return false;
        }

        public void GenerateApple()
        {
            bool isGenerate = false;
            int x = 0;
            int y = 0;
            while(!isGenerate)
            {
                x = _random.Next(1, 13);
                y = _random.Next(1, 11);
                isGenerate = true;
                foreach(PairCoordinate pr in Snake)
                {
                    if(pr.X == x && pr.Y == y)
                    {
                        isGenerate = false;
                        break;
                    }
                }
            }
            Apple.X = x;
            Apple.Y = y;
        }

        public void RestartGame()
        {
            Snake = new List<PairCoordinate>()
            {
                new(4, 5),
                new(3, 5),
                new(2, 5)
            };
            Apple = new PairCoordinate(10, 5);
            SizeSnake = 3;
        }
    }

    public class PairCoordinate
    {
        public PairCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum GameObject
    {
        None = 0,
        LightGreen = 1,
        DarkGreen = 2,
        Wall = 3,
        SnakeHead = 4,
        SnakeBody = 5,
        SnakeEnd = 6,
        Apple = 7
    }

    public enum VariableMove
    {
        None = 0,
        Up = 1,
        Left = 2, 
        Down = 3, 
        Rigth = 4
    }
}
