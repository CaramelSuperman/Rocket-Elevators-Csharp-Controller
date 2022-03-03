using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class BestElevatorInformations
    {
        public int referenceGap;
        public int bestScore;
        public Elevator bestElevator;

        public BestElevatorInformations(Elevator bestElevator, int bestScore, int referenceGap)
        {
            this.bestElevator = bestElevator;
            this.bestScore = bestScore;
            this.referenceGap = referenceGap;

        }
    }



    public class Column
    {

        public int requestedFloor = 0;
        public int currentFloor = 0;
        public string ID;
        public int callButtonId;
        public string elevatorID;
        public string status = "";
        public int amountOfFloors;
        public int amountOfElevators;
        public Boolean isBasement;
        public List<Elevator> elevatorsList;


        public List<CallButton> callbuttonList;
        public List<int> servedFloorsList;


        public Column(string _id, int _amountOfElevators, List<int> _servedFloorsList, bool _isBasement)
        {
            this.ID = _id;
            this.amountOfElevators = _amountOfElevators;
            this.servedFloorsList = _servedFloorsList;
            this.isBasement = _isBasement;
            this.elevatorsList = createElevators(_amountOfElevators);
        }

        public List<CallButton> createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if (_isBasement == true)
            {
                int buttonFloor = -1;
                for (int i = 0; i <= _amountOfFloors; i++)
                {
                    var callButton = new CallButton(callButtonId, "off", buttonFloor, "up");
                    this.callbuttonList.Add(callButton);
                    buttonFloor--;
                    callButtonId++;
                }

            }
            else
            {
                int buttonFloor = 1;
                for (int i = 0; i <= _amountOfFloors; i++)
                {
                    var callButton = new CallButton(callButtonId, "off", buttonFloor, "down");
                    this.callbuttonList.Add(callButton);
                    buttonFloor++;
                    callButtonId++;


                }

            }
                Console.WriteLine("my buttons" + callbuttonList);
            return callbuttonList;


        }

        public List<Elevator> createElevators(int _amountOfElevators)
        {
            List<Elevator> elevators = new List<Elevator>();
            for (int i = 1; i <= _amountOfElevators; i++)
            {
                var elevator = new Elevator(this.ID + i);
                elevators.Add(elevator);
            }

            return elevators;
        }

        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = this.findElevator(userPosition, direction);
            elevator.addNewRequest(requestedFloor);
            elevator.door.status = "close";
            elevator.move();
            elevator.door.status = "open";
            elevator.addNewRequest(1);
            elevator.door.status = "close";
            elevator.move();
            elevator.door.status = "open";

            return elevator;
        }




        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            var bestElevatorInformations = new BestElevatorInformations(null, 6, 10000000);

            for (int i = 0; i < elevatorsList.Count; ++i)
            {
                Elevator elevator = elevatorsList[i];

                if (requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)
                {
                    bestElevatorInformations.bestElevator = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                }
                else if (requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == elevator.direction)
                {
                    bestElevatorInformations.bestElevator = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                }
                else if (requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == elevator.direction)
                {
                    bestElevatorInformations.bestElevator = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                }
                else if (elevator.status == "idle")
                {
                    bestElevatorInformations.bestElevator = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                }
                else
                {
                    bestElevatorInformations.bestElevator = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                }
            }
            return bestElevatorInformations.bestElevator;


        }

        public Elevator checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, BestElevatorInformations bestElevatorInformations, int floor)
        {

            var theElevator = new Elevator("");

            if (scoreToCheck < bestElevatorInformations.bestScore)
            {
                bestElevatorInformations.bestScore = scoreToCheck;
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestElevatorInformations.bestScore == scoreToCheck)
            {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if (bestElevatorInformations.referenceGap > gap)
                {
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = gap;
                    bestElevatorInformations.bestScore = scoreToCheck;
                }
                theElevator = bestElevatorInformations.bestElevator;
            }
            return theElevator;

        }
    }

}








