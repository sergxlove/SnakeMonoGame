using GO = SnakeMonoGame.Core.Models.GameObject;

namespace SnakeMonoGame.Core.Models
{
    public class GameField
    {
        public GameObject[,] Field = new GameObject[12, 14]
        {
            {(GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2 },
            {(GO)3, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)3 },
            {(GO)3, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2, (GO)1, (GO)2 },
            {(GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3, (GO)3 },
        };


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
