using SFML.Graphics;
using SFML.System;

namespace week43
{
    public class Ball
    {
        const int size = 25;
        public float speed = 0.1f;
        public float xPosDirect = -3f;
        public float yPosDirect = 0f;
        public CircleShape ballObj = new CircleShape();

        public Ball(uint x, uint y, uint size)
        {
            ballObj = new CircleShape(size);

            ballObj.Position = new Vector2f(x, y);
        }
    }
}