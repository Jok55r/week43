using SFML.Window;
using SFML.Graphics;
using System;
using System.Threading;

namespace week43
{
    public class Game
    {
        public void GameStart()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1000, 1000), "Game window");
            window.Closed += WindowClosed;

            Circle circle = new Circle();
            Player player1 = new Player(10, window.Size.Y / 2);
            Player player2 = new Player(window.Size.X - 20, window.Size.Y / 2);


            SFML.System.Vector2f playerShape = new SFML.System.Vector2f(10, 200);

            RectangleShape player1Shape = new RectangleShape(playerShape);
            RectangleShape player2Shape = new RectangleShape(playerShape);

            CircleShape ball = new CircleShape(circle.size);
            ball.Position = new SFML.System.Vector2f(window.Size.X / 2, window.Size.Y / 2);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(ball);
                window.Draw(player1Shape);
                window.Draw(player2Shape);
                window.Display();

                player1Shape.Position = player1.pos;
                player2Shape.Position = player2.pos;

                ball.Position = new SFML.System.Vector2f
                    (ball.Position.X + circle.xPos * circle.speed, ball.Position.Y + circle.yPos * circle.speed);

                BallColision(ball, circle, window);
                PlayerMove(player1, player2, window);

                circle.speed *= 1.00001f;
            }
        }

        public void PlayerMove(Player player1, Player player2, RenderWindow window)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && player1.pos.Y > 0) player1.pos.Y -= 0.3f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && player1.pos.Y < window.Size.Y - 200) player1.pos.Y += 0.3f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && player2.pos.Y > 0) player2.pos.Y -= 0.3f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && player2.pos.Y < window.Size.Y - 200) player2.pos.Y += 0.3f;
        }

        public void BallColision(CircleShape shape, Circle circle, RenderWindow window)
        {
            if (shape.Position.Y > window.Size.Y - circle.size * 2 || shape.Position.Y < 0)
                circle.yPos = -circle.yPos;

            if (shape.Position.X > window.Size.X - circle.size * 2 || shape.Position.X < 0)
                circle.xPos = -circle.xPos;
        }

        public void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}