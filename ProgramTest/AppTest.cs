using ConsoleApp5;
using Newtonsoft.Json;

namespace ProgramTest
{
    [TestClass]
    public sealed class AppTest
    {
        HurdleHandler testHurdleHandler = new HurdleHandler();

        /*
         * for test configuration
         * grid size : 15 x 15
         * Hurdles :
         *      Rock Type : [2, 2], [3, 3]
         *      Transfer Type : [4, 4], [6, 11]
         *      Rotation Type : [3, 5], Rotation Degree : 270 degree
         */
        [TestMethod]
        public void HurdleTest()
        {
            Startup startup = new Startup(testHurdleHandler, true);
            string configuration = @"[{""robotId"":1,""robotName"":""Robot 1"",""hurdles"":[{""_hurdleName"":""Block"",""_hurdleType"":1},{""_hurdleName"":""Transfer"",""_hurdleType"":2},{""_hurdleName"":""Rotating"",""_hurdleType"":3}],""hurdlesGrid"":[{""Key"":{""Item1"":2,""Item2"":2},""Value"":{""_hurdle"":{""_hurdleName"":""Block"",""_hurdleType"":1},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":3,""Item2"":3},""Value"":{""_hurdle"":{""_hurdleName"":""Block"",""_hurdleType"":1},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":4,""Item2"":4},""Value"":{""_hurdle"":{""_hurdleName"":""Transfer"",""_hurdleType"":2},""_transferCordinate"":{""Item1"":6,""Item2"":11},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":6,""Item2"":11},""Value"":{""_hurdle"":{""_hurdleName"":""Transfer"",""_hurdleType"":2},""_transferCordinate"":{""Item1"":4,""Item2"":4},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":3,""Item2"":5},""Value"":{""_hurdle"":{""_hurdleName"":""Rotating"",""_hurdleType"":3},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":3}}],""xGridSize"":15,""yGridSize"":15}]";
            startup.robotConfigurations = JsonConvert.DeserializeObject<List<RobotConfiguration>>(configuration);
            RobotConfiguration robotConfiguration = startup.robotConfigurations.First();

            //checking full path
            var expected = new List<Tuple<int, int, char>>
            {
                Tuple.Create(1, 2, 'E'),
                Tuple.Create(1, 2, 'N'),
                Tuple.Create(1, 3, 'N'),
                Tuple.Create(1, 4, 'N'),
                Tuple.Create(1, 4, 'E'),
                Tuple.Create(1, 4, 'N'),
                Tuple.Create(1, 5, 'N'),
                Tuple.Create(1, 5, 'E')
            };
            List<Tuple<int,int,char>> path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "LFFRFFLRFL"
                , Tuple.Create(1, 2, 'N'));
            bool isListExpected = true;
            for(int i = 0;i<path.Count;i++)
            {
                if(path[i].Item1 != expected[i].Item1 || path[i].Item2 != expected[i].Item2 || path[i].Item3 != expected[i].Item3)
                {
                    isListExpected = false;
                    break;
                }
            }
            Assert.IsTrue(isListExpected);

            // Rock Not able to move forward
            path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "LFRFFFFFF"
                , Tuple.Create(1, 1, 'N'));
            Tuple<int,int,char> last = path.Last();
            
            Assert.AreEqual(last.Item1, 2);
            Assert.AreEqual(last.Item2, 1);
            Assert.AreEqual(last.Item3, 'N');

            //Transfer type - move to other location
            path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "LFFFRFFF"
                , Tuple.Create(1, 1, 'N'));
            last = path.Last();

            Assert.AreEqual(last.Item1, 6);
            Assert.AreEqual(last.Item2, 11);
            Assert.AreEqual(last.Item3, 'N');

            //Spin type - rotate the robot
            path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "LFFFFRFFFFRFF"
                , Tuple.Create(1, 1, 'N'));
            last = path.Last();

            Assert.AreEqual(last.Item1, 3);
            Assert.AreEqual(last.Item2, 5);
            Assert.AreEqual(last.Item3, 'S');
        }

        [TestMethod]
        public void MoveTest()
        {
            Startup startup = new Startup(testHurdleHandler, true);
            string configuration = @"[{""robotId"":1,""robotName"":""Robot 1"",""hurdles"":[{""_hurdleName"":""Block"",""_hurdleType"":1},{""_hurdleName"":""Transfer"",""_hurdleType"":2},{""_hurdleName"":""Rotating"",""_hurdleType"":3}],""hurdlesGrid"":[{""Key"":{""Item1"":2,""Item2"":2},""Value"":{""_hurdle"":{""_hurdleName"":""Block"",""_hurdleType"":1},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":3,""Item2"":3},""Value"":{""_hurdle"":{""_hurdleName"":""Block"",""_hurdleType"":1},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":4,""Item2"":4},""Value"":{""_hurdle"":{""_hurdleName"":""Transfer"",""_hurdleType"":2},""_transferCordinate"":{""Item1"":6,""Item2"":11},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":6,""Item2"":11},""Value"":{""_hurdle"":{""_hurdleName"":""Transfer"",""_hurdleType"":2},""_transferCordinate"":{""Item1"":4,""Item2"":4},""_rotationDegreeNumber"":0}},{""Key"":{""Item1"":3,""Item2"":5},""Value"":{""_hurdle"":{""_hurdleName"":""Rotating"",""_hurdleType"":3},""_transferCordinate"":{""Item1"":0,""Item2"":0},""_rotationDegreeNumber"":3}}],""xGridSize"":15,""yGridSize"":15}]";
            startup.robotConfigurations = JsonConvert.DeserializeObject<List<RobotConfiguration>>(configuration);
            RobotConfiguration robotConfiguration = startup.robotConfigurations.First();


            //Move Left
            List<Tuple<int, int, char>> path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "L"
                , Tuple.Create(1, 1, 'N'));
            Tuple<int, int, char> last = path.Last();

            Assert.AreEqual(last.Item1, 1);
            Assert.AreEqual(last.Item2, 1);
            Assert.AreEqual(last.Item3, 'E');

            //Move Right
            path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "R"
                , Tuple.Create(1, 1, 'N'));
            last = path.Last();

            Assert.AreEqual(last.Item1, 1);
            Assert.AreEqual(last.Item2, 1);
            Assert.AreEqual(last.Item3, 'W');

            //Move Forward
            path = startup.TraverseRobotOnGrid(robotConfiguration.xGridSize
                , robotConfiguration.yGridSize
                , "F"
                , Tuple.Create(1, 1, 'N'));
            last = path.Last();

            Assert.AreEqual(last.Item1, 1);
            Assert.AreEqual(last.Item2, 2);
            Assert.AreEqual(last.Item3, 'N');
        }
    }
}
