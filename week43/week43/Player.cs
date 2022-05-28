using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace week43
{
    public class Player
    {
        public Vector2f pos;
        public RectangleShape playerObj = new RectangleShape();
        public Keyboard.Key up;
        public Keyboard.Key down;
        public int points = 0;
        public Text pointsText = new Text();
        public int dy = 0;
        //public Text pointsText = new Text("0", new Font("D:/Github/week43/week43/packages/bullpen-3d.zip/Bullpen3D.ttf"));

        public Player(uint xPos, uint yPos, uint xSize, uint ySize, Keyboard.Key aUp, Keyboard.Key aDown)
        {
            pointsText.DisplayedString = points.ToString();
            pointsText.Position = new Vector2f(xPos, yPos);
            pointsText.Scale = new Vector2f(100, 100);
            pointsText.CharacterSize = 100;

            up = aUp;
            down = aDown;

            Vector2f playerShape = new Vector2f(xSize, ySize);
            playerObj = new RectangleShape(playerShape);

            pos.X = xPos;
            pos.Y = yPos;
        }
    }
}