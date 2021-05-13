using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Challenges.Monty_Hall_Problem
{
    public class Game
    {
        private List<Door> doors;
        Random rand = new Random();

        public Game()
        {
            doors = new List<Door>();

            doors.Add(new Door());
            doors.Add(new Door());
            doors.Add(new Door());

            doors[rand.Next(3)].HasPrize = true;
        }

        public void Restart()
        {
            for(var i = 0; i<doors.Count; i++)
            {
                doors[i].HasPrize = false; doors[i].Opened = false; doors[i].PlayerSelected = false;
            }
            doors[rand.Next(3)].HasPrize = true;
        }

        public void PlayerSelect(Strategy strategy)
        {
            switch(strategy.FirstDoor)
            {
                case Strategy.DoorChoice.DOOR_ONE:
                    doors[0].PlayerSelected = true;
                    break;
                case Strategy.DoorChoice.DOOR_TWO:
                    doors[1].PlayerSelected = true;
                    break;
                case Strategy.DoorChoice.DOOR_THREE:
                    doors[2].PlayerSelected = true;
                    break;
                case Strategy.DoorChoice.RANDOM:
                    doors[rand.Next(3)].PlayerSelected = true;
                    break;
            }
        }

        public void GameRemoveDoor()
        {
            var remDoor = doors.FindAll(x => !x.HasPrize && !x.PlayerSelected);
            var openDoor = -1;

            if(remDoor.Count == 1)
            {
                openDoor = doors.IndexOf(remDoor[0]);
            }
            else
            {
                openDoor = doors.IndexOf(remDoor[rand.Next(2)]);
            }

            doors[openDoor].Opened = true;
        }

        public void PlayerFinalDecision(Strategy strategy)
        {
            switch(strategy.SecondDoor)
            {
                case Strategy.DoorChoiceExp.DOOR_ONE:
                    if(!doors[0].Opened)
                    {
                        ClearPlayerSelection();
                        doors[0].PlayerSelected = true;
                    }
                    break;
                case Strategy.DoorChoiceExp.DOOR_TWO:
                    if (!doors[1].Opened)
                    {
                        ClearPlayerSelection();
                        doors[1].PlayerSelected = true;
                    }
                    break;
                case Strategy.DoorChoiceExp.DOOR_THREE:
                    if (!doors[2].Opened)
                    {
                        ClearPlayerSelection();
                        doors[2].PlayerSelected = true;
                    }
                    break;
                case Strategy.DoorChoiceExp.STICK:
                    break;
                case Strategy.DoorChoiceExp.SWITCH:
                    var oldDoor = doors.IndexOf(doors.Find(x => !x.Opened && !x.PlayerSelected));
                    ClearPlayerSelection();
                    doors[oldDoor].PlayerSelected = true;
                    break;
                case Strategy.DoorChoiceExp.RANDOM:
                    var stickOrSwitch = rand.Next(2);

                    if (stickOrSwitch == 0) //Stick
                    {

                    }
                    else //Switch
                    {
                        var oldDoorSOS = doors.IndexOf(doors.Find(x => !x.Opened && !x.PlayerSelected));
                        ClearPlayerSelection();
                        doors[oldDoorSOS].PlayerSelected = true;
                    }
                    break;
            }
        }

        public bool PlayerWon()
        {
            return doors.FindAll(x => x.PlayerSelected && x.HasPrize).Count > 0;
        }

        private void ClearPlayerSelection()
        {
            for(var i = 0; i<doors.Count; i++)
            {
                doors[i].PlayerSelected = false;
            }
        }
    }
}
