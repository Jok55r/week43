using System;
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

        public void BallMove(Ball ball, RenderWindow window, Player player1, Player player2, Coin coin)
        {
            Random rnd = new Random();

            WallCollision(window, ball, rnd);
            PlayerCollision(window, ball, rnd, player1, player2);
            CoinCollision(coin, ball);

            float dx = ball.ballObj.Position.X + ball.xPosDirect * ball.speed * 5000 / fps;
            float dy = ball.ballObj.Position.Y + ball.yPosDirect * ball.speed * 5000 / fps;

            ball.ballObj.Position = new Vector2f(dx, dy);
        }

        public void CoinCollision(Coin coin, Ball ball)
        {
            float coinX = coin.coinObj.Position.X;
            float coinY = coin.coinObj.Position.Y;
            float ballX = ball.ballObj.Position.X;
            float ballY = ball.ballObj.Position.Y;

            if (Math.Abs(coinX - ballX) < 10 + sizeOfBalls && Math.Abs(coinY - ballY) < 10 + sizeOfBalls)
            {
                isCoinSpawned = false;
                ball.xPosDirect = -ball.xPosDirect;
                ball.yPosDirect = -ball.yPosDirect;
                coin.coinObj.Position = new Vector2f(-1000, -1000);
            }
        }

        public void PlayerCollision(RenderWindow window, Ball ball, Random rnd, Player player1, Player player2)
        {
            if (Math.Abs(ball.ballObj.Position.X - player1.pos.X) < 10 &&
                Math.Abs(ball.ballObj.Position.Y - player1.pos.Y - 100) < 100)
            {
                ball.AfterHitPlayer(rnd, player1.dy);
            }

            if (Math.Abs(ball.ballObj.Position.X - player2.pos.X) < 50 &&
                Math.Abs(ball.ballObj.Position.Y - player2.pos.Y - 100) < 100)
            {
                ball.AfterHitPlayer(rnd, player2.dy);
            }
        }

        public void WallCollision(RenderWindow window, Ball ball, Random rnd)
        {
            if (isOutTheWindow(window, ball.ballObj.Position.Y, 0))
            {
                ball.yPosDirect = -ball.yPosDirect;
                ball.xPosDirect += (float)rnd.NextDouble() - 0.5f;
            }

            if (isOutTheWindow(window, ball.ballObj.Position.X, sizeOfBalls * 2))
            {
                ball.speed = 0.1f;
                ball.xPosDirect = (float)rnd.NextDouble() * 3 - 0.5f;
                ball.yPosDirect = (float)rnd.NextDouble() * 3 - 0.5f;
                ball.ballObj.Position = new Vector2f(500, 500);
            }
        }

        public void AfterHitPlayer(Random rnd, int dy)
        {
            xPosDirect = -xPosDirect * 1.1f;
            yPosDirect += (float)rnd.NextDouble() - 0.5f * dy * 2;
        }
    }
}