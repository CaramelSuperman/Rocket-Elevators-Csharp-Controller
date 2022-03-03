using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {

        // Global variables
        public static int floorRequestButtonID = 1;
        public static int callButtonID = 1;
        public static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Instance variables
        
        public int amountOfColumns;
        public int amountOfFloors;
        public int amountOfBasements;
        public int amountOfElevatorPerColumn;
        public int floor;
        public int buttonFloor;
        public int ID;
        public int columnID = 1;
        public string status;
        public List<Column> columnsList;
        public List<FloorRequestButton> floorRequestButtonsList;
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.ID = _ID;
            this.status = "online";
            this.floorRequestButtonsList = createFloorRequestButtons(_amountOfFloors);
            this.columnsList = createColumns(_amountOfColumns, _amountOfFloors,_amountOfBasements ,_amountOfElevatorPerColumn);
            this.amountOfColumns = _amountOfColumns;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfBasements = _amountOfBasements;
            this.amountOfElevatorPerColumn = _amountOfElevatorPerColumn;
            if(amountOfBasements > 0)
            {
                this.floorRequestButtonsList = createBasementFloorRequestButtons(_amountOfBasements); //parameters?
                this.columnsList = createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                amountOfColumns--;
            }
        }
        //function createBasementColumn
        public List<Column> createBasementColumn(int _amountOfBasements,int _amountOfElevatorPerColumn)
        {
            // INIT servedFloors TO EMPTY ARRAY?
            List<int> servedFloors = new List<int>();
            floor = -1;
            for(int i = 0; i < _amountOfBasements; i++)
            {
                servedFloors.Add(floor);
                floor--;
            }
            var column = new Column(alphabet[this.columnID - 1].ToString(), _amountOfElevatorPerColumn, servedFloors, false);
            this.columnsList.Add(column);
            this.columnID++;
            return columnsList;
        }


        // function createColumns
        public List<Column> createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<Column> columns = new List<Column>();
            var amountOfFloorsPerColumn = Math.Ceiling((decimal)_amountOfFloors / _amountOfColumns);
            
            
            floor = 1;
            for(int i = 0; i < _amountOfColumns; i++)
            {
                // SET servedFloors TO EMPTY ARRAY
                List<int> servedFloors = new List<int>();
                for(int e = 0; e < amountOfFloorsPerColumn; e++)
                {
                    if(floor <= _amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                var column = new Column(alphabet[this.columnID - 1].ToString(), _amountOfElevatorPerColumn, servedFloors, false);
                columns.Add(column);
                this.columnID++;
            }
            return columns;
        }


        // function createFloorRequestButtons
        public List<FloorRequestButton> createFloorRequestButtons(int _amountOfFloors)
        {
            List<FloorRequestButton> requestButtons = new List<FloorRequestButton>();
            buttonFloor = 1;
            for(int i = 0; i < _amountOfFloors; i++)
            {
                var floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "up");
                requestButtons.Add(floorRequestButton);
                buttonFloor++;
                floorRequestButtonID++;
            }
            return requestButtons;
        }
        // function createBasementFloorRequestButtons
        public List<FloorRequestButton> createBasementFloorRequestButtons(int _amountOfBasements)
        {
            buttonFloor = -1;
            for(int i = 0; i < _amountOfBasements; i++)
            {
                var floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "down");
                this.floorRequestButtonsList.Add(floorRequestButton);
                buttonFloor--;
                floorRequestButtonID++;
            }
            return floorRequestButtonsList;
        }
        // function findBestColumn
        public Column findBestColumn(int _requestedFloor)
        {
            Column column = null;
            foreach(Column i in this.columnsList)
            {
                if(i.servedFloorsList.Contains(_requestedFloor))
                {
                    
                    column = i;
                }
            }
            return column;
        }
        
        //Simulate when a user press a button at the lobby
        // function assignElevator
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column choseColumn = findBestColumn(_requestedFloor);
            Elevator choseElevator = choseColumn.findElevator(1, _direction);
            choseElevator.addNewRequest(1);
            choseElevator.door.status = "close";
            choseElevator.move();
            choseElevator.door.status = "open";
            choseElevator.completedRequestsList.Add(1);
            choseElevator.completedRequestsList.Add(20);

            choseElevator.completedRequestsList.Add(_requestedFloor);
            // choseElevator.addNewRequest(_requestedFloor);
            // choseElevator.door.status = "close";
            choseElevator.move();
            // choseElevator.completedRequestsList.Add(_requestedFloor);
            // choseElevator.door.status = "open";
            return (choseColumn, choseElevator);
            
        }
    }
}
    