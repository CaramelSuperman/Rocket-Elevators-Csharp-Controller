namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    { public int id = 1;
      public string status = "online";
      public int floor;
      public string direction = "up";
        public FloorRequestButton(int _floor, string _direction)
        {
            
        }
    }
}