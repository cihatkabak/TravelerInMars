using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerInMars
{
    class Mars
    {
        Area area;
        Robot robotA;
        Robot robotB;
        string choose;       
        string[] directions = { "N", "E", "S", "W" };
        public  const string ROBOT_A = "robota";
        public const  string ROBOT_B = "robotb";
        public void InputSize()
        {
            try
            {
                Console.Write("Enter the field size in x and y (e.g. 15 15) : ");
                string areaSize = Console.ReadLine();
                string[] splitSize = areaSize.Split(' ');
                area = new Area();
                area.x = Convert.ToInt32(splitSize[0]);
                area.y = Convert.ToInt32(splitSize[1]);
                initRobotLocation();
            }
            catch
            {
                Console.WriteLine("Wrong Type!");
                InputSize();
            }
        }
        
        public void initRobotLocation()
        {
            string[] splitLocation = getLocationArray();
            switch (choose)
            {
                case ROBOT_A:
                    robotA = new Robot();
                    prepareRobot(robotA, splitLocation);
                    break;
                case ROBOT_B:
                    robotB = new Robot();
                    prepareRobot(robotB, splitLocation);
                    break;
            }
        }

        private string[] getLocationArray()
        {
            string locationDirection;
            Console.Write("Enter the " +choose+ " first Location(e.g. 0 5 W) : ");
            string location = Console.ReadLine().ToUpper();
            string[] locationArray = location.Split(' ');
            try
            {
                locationDirection = locationArray[2];
            }
            catch
            {
                Console.WriteLine("Wrong Type! Please enter the x,y and location value!");
                return getLocationArray();
            }
            string robotDirection;
            if (locationDirection == "N" || locationDirection == "E" || locationDirection == "W" || locationDirection == "S")
            {
                robotDirection = locationDirection;
            }
            else
            {
                Console.WriteLine("Wrong Type! Just choose this one of : N, W, E, S");
                return getLocationArray();
            }
            return locationArray;
        }

        public void prepareRobot(Robot robot, string[] locationArray)
        {
            robot.x = Convert.ToInt32(locationArray[0]);
            robot.y = Convert.ToInt32(locationArray[1]);
            robot.direction = locationArray[2];
            InputMoveData();
        }

        public void InputMoveData()
        {
            Console.Write("Enter the move data(e.g. LMLMMRM) : ");
            string moveData = Console.ReadLine().ToUpper();
            char[] splitMoveData = moveData.ToCharArray();
            MovementOperations(splitMoveData);
        }
        public void prepareRobotForMovement(char[] MoveData,Robot robot,string robotname)
        {
            string newDirection;
            for (int i = 0; i < MoveData.Length; i++)
            {
                if (MoveData[i] == 'L')
                {
                    newDirection = DirectionOperations(robot.direction, MoveData[i]);
                    robot.direction = newDirection;
                }
                else if (MoveData[i] == 'R')
                {
                    newDirection = DirectionOperations(robot.direction, MoveData[i]);
                    robot.direction = newDirection;
                }
                else if(MoveData[i]=='M')
                {
                    RobotMotion(robotname);
                }
            }
        }
        public void MovementOperations(char[] MoveData)
        {
            if (choose == ROBOT_A)
            {
                prepareRobotForMovement(MoveData, robotA, ROBOT_A);
            }
            else
            {
                prepareRobotForMovement(MoveData, robotB, ROBOT_B);
            }
        }
        public string DirectionOperations(string oldDirection, char LeftOrRight)
        {
            int oldDirectionValue= Array.IndexOf(directions, oldDirection); ;
            int MoveValue;
            if(LeftOrRight=='L')
            {
                MoveValue = oldDirectionValue - 1;
                if (MoveValue < 0) MoveValue = 4 - MoveValue * (-1);
            }
            else
            {
                MoveValue = oldDirectionValue + 1;
                if (MoveValue > 3) MoveValue = MoveValue % 4;
            }
            return directions[MoveValue];
        }
        public void RobotMotion(string robot)
        {
            if (robot == ROBOT_A)
            {
                moveRobot(robotA);
            }
            if (robot == ROBOT_B)
            {
                moveRobot(robotB);
            } 
        }
        private void moveRobot(Robot robot)
        {
            switch (robot.direction)
            {
                case "N":
                    robot.y = robot.y + 1;
                    if (robot.y > area.y) robot.y = area.y;
                    break;
                case "E":
                    robot.x = robot.x + 1;
                    if (robot.x > area.x) robot.x = area.x;
                    break;
                case "S":
                    robot.y = robot.y - 1;
                    if (robot.y < 0) robot.y = 0;
                    break;
                case "W":
                    robot.x = robot.x - 1;
                    if (robot.x < 0) robot.x = 0;
                    break;

                default:
                    break;
            }
        }
        public void printRobotData()
        {
           Console.WriteLine("RobotA : " + robotA.x + "," + robotA.y + "," + robotA.direction);
           Console.WriteLine("RobotB : " + robotB.x + "," + robotB.y+ "," + robotB.direction);
        }
        static void Main(string[] args)
        {
            Mars p = new Mars();
            p.choose = "robota";
            p.InputSize();
            p.choose = "robotb";
            p.initRobotLocation();
            p.printRobotData();
            Console.ReadKey();
        }
    }
}
