using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Challenges.Monty_Hall_Problem
{
    public class Strategy
    {
        public Strategy(){}

        public enum DoorChoice
        {
            DOOR_ONE,
            DOOR_TWO,
            DOOR_THREE,
            RANDOM
        }

        public enum DoorChoiceExp
        {
            DOOR_ONE,
            DOOR_TWO,
            DOOR_THREE,
            RANDOM,
            STICK,
            SWITCH
        }

        public DoorChoice FirstDoor { get; set; }
        public DoorChoiceExp SecondDoor { get; set; }
    }
}
