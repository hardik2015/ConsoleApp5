using Newtonsoft.Json;


namespace ConsoleApp5
{
    public class Startup
    {

        public List<RobotConfiguration> robotConfigurations;
        private HurdleHandler _hurdleHandler;
        private bool _isTest;

        /*
         * Constructor
         */
        public Startup(HurdleHandler hurdleHandler, bool isTest = false)
        {
            _hurdleHandler = hurdleHandler;
            _isTest = isTest;
        }

        /*
         * Program startup Point
         */
        public void StartProgram()
        {
            ReadConfiguration();
            if (robotConfigurations == null)
                robotConfigurations = new List<RobotConfiguration>();
            Console.WriteLine("Select Robot Configuration for saved once or create new");
            int i = 0;
            if (robotConfigurations.Count > 0)
            {
                for (; i < robotConfigurations.Count; i++)
                {
                    Console.WriteLine((i + 1) + " - " + robotConfigurations[i].robotName);
                }
            }
            Console.WriteLine((i + 1) + " - " + "Create New Robot Configuration");
            int input = Int32.Parse(Console.ReadLine()) - 1; // as we added one for removing zero indexing
            if (input == i)
            {
                RunRobot(NewConfiguration());
            }
            else
            {
                RunRobot(robotConfigurations[input]);
            }

        }

        /*
         * Robot traversal control
         */
        public void RunRobot(RobotConfiguration robot)
        {
            _hurdleHandler.changeList(robot.hurdlesGrid);
            bool isNextPathSquence = true;
            while (isNextPathSquence)
            {
                char[] directions = ['N', 'S', 'W', 'E'];
                Console.WriteLine("Give Starting Point (Example: [1,2] and North Direction as 1 2 N): ");
                string[] currentTupleString = Console.ReadLine().Split(" ");
                int x = 0, y = 0;
                if (currentTupleString.Count() != 3
                    || !int.TryParse(currentTupleString[0], out x)
                    || !int.TryParse(currentTupleString[1], out y)
                    || !directions.Contains(currentTupleString[2].ToUpper().ToCharArray()[0])
                    || _hurdleHandler.hasValue(Tuple.Create(x, y) ))
                {
                    Console.WriteLine("Invalid Starting Point (Example: [1,2] and North Direction as 1 2 N) or Ther is a hurdle at that location ");
                    return;
                }
                Tuple<int, int, char> currentPosition = Tuple.Create(x, y, currentTupleString[2].ToUpper().ToCharArray()[0]); ;
                Console.WriteLine("Give Traversal Root (Example: LFFRFRLFL): ");
                string pathInstructionString = Console.ReadLine();
                var path = TraverseRobotOnGrid(robot.xGridSize, robot.yGridSize, pathInstructionString, currentPosition);
                foreach (var element in path)
                {
                    Console.WriteLine(String.Format("Point : [{0}, {1}]\t Direction : {2}", element.Item1, element.Item2, ShortDirectionToFullForm(element.Item3)));
                }
                Console.WriteLine("Want to give another path sequence( Y/y for yes, Other as No) ");
                if (!Console.ReadLine().ToUpper().Equals("Y"))
                {
                    isNextPathSquence = false;
                }
            }
        }
        
        /*
         * Setup robot configuration
         */
        public RobotConfiguration NewConfiguration()
        {
            Console.WriteLine("---------- Start Robot Configuration Wizard ---------- ");
            Console.WriteLine("Grid size (give as 20X20 as 20 20) : ");
            string[] grid = Console.ReadLine().Split(" ");
            int xLength = 0;
            int yLength = 0;
            if (!(grid.Count() == 2
                && Int32.TryParse(grid[0], out xLength)
                && Int32.TryParse(grid[1], out yLength)))
            {
                Console.WriteLine("Closing Program as Grid input is Invalid");
                return null;
            }


            Console.WriteLine("---------- Start Hurdle Configuration Wizard ---------- ");
            // adding hurdles
            List<Hurdle> hurdles = new List<Hurdle>();
            bool isShouldContinue = true;
            Console.WriteLine("Start adding Different Hurdles ");
            while(isShouldContinue)
            {
                Console.WriteLine("Select Hurdle Type : \n" +
                    "1 - Block Type Hurdle by which Robot cannot go to that Point\n" +
                    "2 - Transfer Type Hurdle by which Robot will be transfered to other Point\n" +
                    "3 - Rotating Type Hurdle which will change diration of Robot\n" +
                    "Any Other - Exit Adding");
                string hurdleTypeSelect = Console.ReadLine();
                if (hurdleTypeSelect == "1")
                {
                    Console.WriteLine("Hurdle Name (Block): ");
                    string hurdleNameSelect = Console.ReadLine().Trim();
                    Hurdle hurdleType = new Hurdle(hurdleNameSelect.Length != 0 ? hurdleNameSelect : "Block" , HurdleType.BlockType);
                    hurdles.Add(hurdleType);

                }
                else if (hurdleTypeSelect == "2")
                {
                    Console.WriteLine("Hurdle Name (Transfer): ");
                    string hurdleNameSelect = Console.ReadLine().Trim();
                    Hurdle hurdleType = new Hurdle(hurdleNameSelect.Length != 0 ? hurdleNameSelect : "Transfer", HurdleType.TransferType);
                    hurdles.Add(hurdleType);

                }
                else if (hurdleTypeSelect == "3")
                {
                    Console.WriteLine("Hurdle Name (Rotating): ");
                    string hurdleNameSelect = Console.ReadLine().Trim();
                    Hurdle hurdleType = new Hurdle(hurdleNameSelect.Length != 0 ? hurdleNameSelect : "Rotating", HurdleType.RotationType);
                    hurdles.Add(hurdleType);

                }
                else 
                {
                    Console.WriteLine("Are you sure you want to complete adding hurdles? (Y/y for yes anyother for N)");
                    string input = Console.ReadLine();
                    if (input.ToUpper().Equals("Y"))
                    {
                        isShouldContinue = false;
                    }

                }
            }

            //adding hurdle items with parameters and into th grid
            List<Hurdle> hurdleTypes = new List<Hurdle>();
            Console.WriteLine("Add Hurdle Items");
            bool shouldContinue = true;
            while (shouldContinue)
            {
                Console.WriteLine("Select Hurdle From Below List : ");
                int counter = 1;
                hurdles.ForEach(x => Console.WriteLine(counter++ + " - " + x._hurdleName));
                Console.WriteLine("Press any other key to Complete Adding.");
                string hurdleItemSelect = Console.ReadLine();
                int hurdleIndex;
                int.TryParse(hurdleItemSelect, out hurdleIndex);
                if (hurdleIndex == 0 || hurdleIndex > hurdles.Count)
                {
                    Console.WriteLine("Are you sure you want to complete adding? (Y/y for yes anyother for N)");
                    string input = Console.ReadLine();
                    if (input.ToUpper().Equals("Y"))
                    {
                        shouldContinue = false;
                        continue;
                    }
                }
                Hurdle selectedHurdle = hurdles[hurdleIndex - 1];
                HurdleItem selectedHurdleItem = null;
                if (selectedHurdle._hurdleType == HurdleType.TransferType)
                {
                    Console.WriteLine("Transfer Location (Format : [5, 6] as 5 6 ) : ");
                    string[] transferLocation = Console.ReadLine().Split(" ");
                    int xCord = 0, yCord = 0;
                    if (transferLocation.Count() != 2
                        || !Int32.TryParse(transferLocation[0], out xCord) || xCord > xLength
                        || !Int32.TryParse(transferLocation[1], out yCord) || yCord > yLength)
                    {
                        Console.WriteLine("Invalid Format Default to [0, 0]: ");
                        selectedHurdleItem = new HurdleItem(selectedHurdle, Tuple.Create(0, 0));
                    }
                    else
                    {
                        selectedHurdleItem = new HurdleItem(selectedHurdle, Tuple.Create(xCord, yCord));
                    }
                }
                else if (selectedHurdle._hurdleType == HurdleType.RotationType)
                {
                    Console.WriteLine("Roltation Dgree (Format : 260 )(Default is 0) : ");
                    string rotationDegree = Console.ReadLine();
                    int degree = 0;
                    if (Int32.TryParse(rotationDegree, out degree) && degree > 0)
                    {
                        selectedHurdleItem = new HurdleItem(selectedHurdle, degree);
                    }
                    else
                    {
                        selectedHurdleItem = new HurdleItem(selectedHurdle, 0);
                    }
                }
                else
                {
                    selectedHurdleItem = new HurdleItem(selectedHurdle);
                }
                Console.WriteLine("Hurdle Location (Format : [5, 6] as 5 6 ): ");
                string[] inputLocation = Console.ReadLine().Split(" ");
                int x = 0, y = 0;
                if (inputLocation.Count() == 2
                    && Int32.TryParse(inputLocation[0], out x)
                    && Int32.TryParse(inputLocation[1], out y)
                    && x <= xLength && y <= yLength)
                {
                    if (_hurdleHandler.hasValue(Tuple.Create(x, y)))
                    {
                        Console.WriteLine("On that Posotion we have another hurdle so add to other place");
                    }
                    else
                    {
                        _hurdleHandler.addToList(Tuple.Create(x, y), selectedHurdleItem);
                        if (selectedHurdle._hurdleType == HurdleType.TransferType)
                            _hurdleHandler.addToList(selectedHurdleItem._transferCordinate, new HurdleItem(selectedHurdle, Tuple.Create(x, y)));
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Grid Position ");
                }

            }

            Console.WriteLine("---------- End Hurdle Configuration Wizard ---------- ");
            Console.WriteLine("---------- End Robot Configuration Wizard ---------- ");
            var hurdleGridDetails = _hurdleHandler.getList();
            var lastRobotConfig = robotConfigurations.LastOrDefault();
            int Id = lastRobotConfig == null ? 1 : lastRobotConfig.robotId + 1;
            var config = new RobotConfiguration { robotId = Id, robotName = "Robot " + Id, hurdles = hurdles, xGridSize = xLength, yGridSize = yLength, hurdlesGrid = hurdleGridDetails };
            robotConfigurations.Add(config);
            SaveConfiguration();
            return config;
        }

        /*
         * actual traversing done in this method
         */
        public List<Tuple<int, int, char>> TraverseRobotOnGrid(int xLength, int yLength, string pathInstructionString, Tuple<int, int, char> currentPosition)
        {
            if (_isTest)
            {
                _hurdleHandler.changeList(robotConfigurations.First().hurdlesGrid);
            }
            Tuple<int, int, char> previousPosition = null;
            List<Tuple<int, int, char>> path = new List<Tuple<int, int, char>>();
            foreach (char s in pathInstructionString.ToCharArray())
            {
                previousPosition = currentPosition;
                if (s.Equals('F'))
                {
                    if (currentPosition.Item3.Equals('N') && currentPosition.Item2 < yLength)
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 + 1, currentPosition.Item3);
                    }
                    else if (currentPosition.Item3.Equals('S') && currentPosition.Item2 > 1)
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 - 1, currentPosition.Item3);
                    }
                    else if (currentPosition.Item3.Equals('E') && currentPosition.Item1 < xLength)
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1 + 1, currentPosition.Item2, currentPosition.Item3);
                    }
                    else if (currentPosition.Item3.Equals('W') && currentPosition.Item1 > 1)
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1 - 1, currentPosition.Item2, currentPosition.Item3);
                    }
                    currentPosition = _hurdleHandler.CheckForHurdleChange(currentPosition, previousPosition);
                }
                else if (s.Equals('L'))
                {
                    if (currentPosition.Item3.Equals('N'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'E');
                    }
                    else if (currentPosition.Item3.Equals('S'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'W');
                    }
                    else if (currentPosition.Item3.Equals('E'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'S');
                    }
                    else if (currentPosition.Item3.Equals('W'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'N');
                    }
                }
                else if (s.Equals('R'))
                {
                    if (currentPosition.Item3.Equals('N'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'W');
                    }
                    else if (currentPosition.Item3.Equals('S'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'E');
                    }
                    else if (currentPosition.Item3.Equals('E'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'N');
                    }
                    else if (currentPosition.Item3.Equals('W'))
                    {
                        currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2, 'S');
                    }
                }
                var lastElement = path.LastOrDefault();
                if (lastElement == null || lastElement != currentPosition)
                    path.Add(currentPosition);
            }
            return path;
        }

        /*
         * reading previous configuration from file configuration.json
         */
        public void ReadConfiguration()
        {
            if (!File.Exists("configurations.json"))
            {
                File.Create("configurations.json");
                robotConfigurations = new List<RobotConfiguration>();
                return;
            }
            string[] configurations = File.ReadAllLines("configurations.json");
            string configuration = "";
            foreach (var item in configurations)
            {
                configuration += item;
            }
            robotConfigurations = JsonConvert.DeserializeObject<List<RobotConfiguration>>(configuration);
        }

        /*
         * saving configuration in file configuration.json
         */
        public void SaveConfiguration()
        {
            string config = JsonConvert.SerializeObject(robotConfigurations);
            File.WriteAllLines("configurations.json", [config]);
        }


        /*
         * Short direction to Full diration string
         */
        private string ShortDirectionToFullForm(char direction)
        {
            switch (direction)
            {
                case 'N':
                    return "North";
                case 'S':
                    return "South";
                case 'W':
                    return "West";
                case 'E':
                    return "East";
                default:
                    return "Undefiened";
            }
        }
    }
}
