using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System;
using System.Threading;

namespace week43
{
    public class Game
    {
        const uint sizeOfBalls = 25;
        const uint fps = 60;
        bool isCoinSpawned = false;
        int dy = 0;

        public void GameStart()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1000, 1000), "Game window");
            window.Closed += WindowClosed;

            Ball ball = new Ball(window.Size.X / 2, window.Size.Y / 2, sizeOfBalls);
            Player player1 = new Player(10, window.Size.Y / 2, 10, 200, Keyboard.Key.W, Keyboard.Key.S);
            Player player2 = new Player(window.Size.X - 20, window.Size.Y / 2, 10, 200, Keyboard.Key.Up, Keyboard.Key.Down);
            Coin coin = new Coin(sizeOfBalls);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(0, 40, 40));
                window.Draw(ball.ballObj);
                window.Draw(player1.playerObj);
                window.Draw(player2.playerObj);
                window.Draw(player1.pointsText);
                window.Draw(player2.pointsText);
                window.Draw(coin.coinObj);
                window.Display();


                BallMove(ball, window, player1, player2, coin);
                PlayerMove(player1, window, ball);
                PlayerMove(player2, window, ball);

                ball.speed *= 1.00001f;

                SpawnCoin(coin);

                Thread.Sleep((int)(1000 / fps));
            }
        }

        public void SpawnCoin(Coin coin)
        {
            Random rnd = new Random();

            if (!isCoinSpawned)
            {
                isCoinSpawned = true;
                coin.coinObj.Position = new Vector2f(rnd.Next(100, 900), rnd.Next(100, 900));
            }
        }

        public void PlayerMove(Player player, RenderWindow window, Ball ball)
        {
            bool IsOutTheWindow = window.Size.Y - 200 - player.pos.Y < 0;
            dy = 0;

            if (Keyboard.IsKeyPressed(player.up) && !IsOutTheWindow) dy++;
            if (Keyboard.IsKeyPressed(player.down) && !IsOutTheWindow) dy--;

            if (player.pos.Y < 0) player.pos.Y = 0;
            else if (player.pos.Y > window.Size.Y - 200) player.pos.Y = window.Size.Y - 200;

            player.pos.Y -= dy * (1000 / fps) * 2;
            player.playerObj.Position = player.pos;
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

            if (coinX - ballX - sizeOfBalls < 10 && coinY - ballY - sizeOfBalls < 10)
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
                Math.Abs(ball.ballObj.Position.Y - player1.pos.Y - 100) < 100 ||
                Math.Abs(ball.ballObj.Position.X - player2.pos.X) < 50 &&
                Math.Abs(ball.ballObj.Position.Y - player2.pos.Y - 100) < 100)
            {
                ball.xPosDirect = -ball.xPosDirect * 1.1f;
                ball.yPosDirect += (float)rnd.NextDouble() - 0.5f * dy * 2;
            }

            /*if (ball.ballObj.Position.X - player2.pos.X < 10 &&
                ball.ballObj.Position.Y - player2.pos.Y - 100 < 100)
            {
                ball.xPosDirect = -ball.xPosDirect * 1.1f;
                ball.yPosDirect += (float)rnd.NextDouble() - 0.5f * dy * 2;
            }*/
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

        public bool isOutTheWindow(RenderWindow window, float ballPos, float distance)
        {
            if (window.Size.Y - sizeOfBalls * 2 - ballPos < -distance || ballPos < -distance)
                return true;
            return false;
        }

        public void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}