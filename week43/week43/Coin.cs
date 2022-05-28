using SFML.Graphics;

namespace week43
{
    public class Coin
    {
        const int size = 25;
        public CircleShape coinObj = new CircleShape();

        public Coin(uint size)
        {
            coinObj = new CircleShape(size);
            coinObj.FillColor = Color.Green;
        }
    }
}
