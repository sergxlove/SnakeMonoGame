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
}
