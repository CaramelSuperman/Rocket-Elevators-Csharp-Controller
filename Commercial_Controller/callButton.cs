namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public int ID = 1;
        public string status = "online";
        public int floor;
        public string direction = "up";
        public CallButton(int _floor, string _direction)
        { 
            this.direction = _direction;
            this.floor = _floor;
        System.Console.WriteLine(_direction);
            
        }
        
    }
}