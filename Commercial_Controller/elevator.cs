using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        string id = "";
        string status = "";
        int currentFloor = 0;
        string direction = "";
        //Door door = new Door(ID, status);
        List<int> floorRequestList = new List<int>();
         List<int> completedRequestList = new List<int>();

        public Elevator(string _elevatorID)
        {
            
        }
        public void move()
        {

        }
        
    }
}