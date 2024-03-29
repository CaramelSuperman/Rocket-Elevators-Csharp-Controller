using System.Threading;
using System.Collections.Generic;
using System;


namespace Commercial_Controller
{
    public class Elevator
    {
        public string ID;
        public int doorID;

        public string status;
        public int currentFloor = 0;
        public int requestedFloor = 0;
        public Door door;
        public string direction = "";
        //Door door = new Door(ID, status);
        public List<int> floorRequestsList;
        public List<int> completedRequestsList;

        public Elevator(string _elevatorID)
        {
            this.ID = _elevatorID;
            this.status = "online";
            this.currentFloor = 1;
            this.door = new Door(doorID, "open");
            this.floorRequestsList = new List<int>();
            this.completedRequestsList = new List<int>();
        }
        //********************************** here the elevator moves************************************************//
        public void move()
        {
           

            while (this.floorRequestsList.Count != 0)
            {
                int destination = this.floorRequestsList[0];
                floorRequestsList.RemoveAt(0);
                this.status = "moving";

                

                if (this.currentFloor < destination)
                {
                    this.direction = "up";
                    this.sortFloorList();
                    while (this.currentFloor < destination)
                    {
                        this.currentFloor++;
                        
                    }
                    this.completedRequestsList.Add(currentFloor);
                }
                else if (this.currentFloor > destination)
                {
                    this.direction = "down";
                    this.sortFloorList();
                    while (this.currentFloor > destination)
                    {
                        this.currentFloor--;
                        
                    }
                    this.completedRequestsList.Add(currentFloor);
                }
                this.status = "stopped";
                

            }
            this.status = "idle";
        }

        //************here we sort the floor list***************************** */
        public void sortFloorList()
        {
            if (this.direction == "up")
            {
                this.floorRequestsList.Sort();
            }
            else if (this.direction == "down")
            {
                this.floorRequestsList.Reverse();
            }
        }

        //******************************** her ewe add new requests*******************************************//
        public void addNewRequest(int requestedFloor)
        {
            List<int> floorRequestList = new List<int>();
            if (!floorRequestsList.Contains(requestedFloor))
            {
                floorRequestsList.Add(requestedFloor);
            }
            if (this.currentFloor < requestedFloor)
            {
                this.direction = "up";


            }
            else if (this.currentFloor > requestedFloor)
            {
                this.direction = "down";
            }


        }

    }




}
