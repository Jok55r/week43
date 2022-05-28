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
        const uint fps = 120;
        bool isCoinSpawned = false;

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


                ball.BallMove(ball, window, player1, player2, coin);
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
            player.dy = 0;

            if (Keyboard.IsKeyPressed(player.up) && !IsOutTheWindow) player.dy++;
            if (Keyboard.IsKeyPressed(player.down) && !IsOutTheWindow) player.dy--;

            if (player.pos.Y < 0) player.pos.Y = 0;
            else if (player.pos.Y > window.Size.Y - 200) player.pos.Y = window.Size.Y - 200;

            player.pos.Y -= player.dy * (1000 / fps) * 2;
            player.playerObj.Position = player.pos;
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