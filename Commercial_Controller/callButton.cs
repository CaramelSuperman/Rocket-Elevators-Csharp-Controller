namespace Commercial_Controller
{
    //***************************************** creation of the lobby buttons*****************************************//
    public class CallButton
    {
        public int ID = 1;
        public string status = "online";
        public int floor;
        public string direction = "up";
        public CallButton(int _id, string _status, int _floor, string _direction)
        { 
            this.direction = _direction;
            this.floor = _floor;
            this.status = _status;
            this.ID = _id;
            
        }
        
    }
}