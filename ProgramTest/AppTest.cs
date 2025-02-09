using ConsoleApp5;

namespace ProgramTest
{
    [TestClass]
    public sealed class AppTest
    {
        HurdleHandler testHurdleHandler = new HurdleHandler();

        //before running this test run program and
        //generate configuration.json file so
        //all hurdles info is available
        [TestMethod]
        public void testProcess()
        {
            var expected = new List<Tuple<int, int, char>>
            {
                Tuple.Create(1, 2, 'E'),
                Tuple.Create(2, 2, 'E'),
                Tuple.Create(3, 2, 'E'),
                Tuple.Create(3, 2, 'N'),
                Tuple.Create(3, 3, 'N'),
                Tuple.Create(3, 4, 'N'),
                Tuple.Create(3, 4, 'E'),
                Tuple.Create(3, 4, 'N'),
                Tuple.Create(3, 5, 'N'),
                Tuple.Create(3, 5, 'E')
            };
            Startup startup = new Startup(testHurdleHandler);
            
            startup.ReadConfiguration();
            RobotConfiguration robotConfiguration = startup.robotConfigurations.First();
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
            
        }
    }
}
