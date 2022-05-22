namespace week43
{
    public class Player
    {
        public SFML.System.Vector2f pos;

        public Player(uint xPos, uint yPos)
        {
            pos.X = xPos;
            pos.Y = yPos;
        }
    }
}