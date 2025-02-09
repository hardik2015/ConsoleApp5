

namespace ConsoleApp5
{
    public class HurdleHandler
    {
        private Dictionary<Tuple<int, int>, HurdleItem> _hurdleItems;
        public HurdleHandler() {
            _hurdleItems = new Dictionary<Tuple<int, int>, HurdleItem>();
        }

        public Tuple<int, int, char> CheckForHurdleChange(Tuple<int, int, char> currentLocation, Tuple<int, int, char> previousLocation)
        {
            var hurdleList = _hurdleItems.Where(x => x.Key.Item1 == currentLocation.Item1 && x.Key.Item2 == currentLocation.Item2).ToList();
            if (hurdleList.Count == 0)
            {
                return currentLocation;
            }
            HurdleItem hurdleItem = hurdleList.SingleOrDefault().Value;
            if (hurdleItem._hurdle._hurdleType == HurdleType.BlockType)
            {
                return previousLocation;
            }
            else if (hurdleItem._hurdle._hurdleType == HurdleType.TransferType)
            {
                return new Tuple<int, int, char>(hurdleItem._transferCordinate.Item1, hurdleItem._transferCordinate.Item2, currentLocation.Item3);
            }
            else if (hurdleItem._hurdle._hurdleType == HurdleType.RotationType)
            {
                char finalDiraction = currentLocation.Item3;
                for (var i = 0; i < hurdleItem._rotationDegreeNumber; i++)
                {
                    switch (finalDiraction)
                    {
                        case 'N':
                            finalDiraction = 'E';
                            break;
                        case 'S':
                            finalDiraction = 'W';
                            break;
                        case 'E':
                            finalDiraction = 'S';
                            break;
                        case 'W':
                            finalDiraction = 'N';
                            break;
                    }
                }
                return new Tuple<int, int, char>(currentLocation.Item1, currentLocation.Item2, finalDiraction);
            }
            return currentLocation;

        }

        public void addToList(Tuple<int, int>location,  HurdleItem hurdleItem)
        {
            _hurdleItems.Add(location, hurdleItem);
        }

        public void changeList(Dictionary<Tuple<int, int>, HurdleItem> hurdleItems)
        {
            _hurdleItems = hurdleItems;
        }

        public Dictionary<Tuple<int, int>, HurdleItem> getList()
        {
            return _hurdleItems;
        }

        public bool hasValue(Tuple<int, int> tuple)
        {
            return _hurdleItems.Keys.Contains(tuple);
        }
    }
}
