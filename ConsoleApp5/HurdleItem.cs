using Newtonsoft.Json;


namespace ConsoleApp5
{
    public class HurdleItem
    {
        public Hurdle _hurdle { get; private set; }
        public Tuple<int, int> _transferCordinate { get; private set; }
        public int _rotationDegreeNumber { get; private set; }

        [JsonConstructor]
        public HurdleItem(Hurdle _hurdle, Tuple<int, int> _transferCordinate, int _rotationDegreeNumber)
        {
            this._hurdle = _hurdle;
            this._transferCordinate = _transferCordinate;
            this._rotationDegreeNumber = _rotationDegreeNumber;
        }
        public HurdleItem(Hurdle hurdle)
        {
            _hurdle = hurdle;
            _transferCordinate = Tuple.Create(0, 0);
        }
        public HurdleItem(Hurdle hurdle, Tuple<int, int> transferCordinate)
        {
            _hurdle = hurdle;
            _transferCordinate = transferCordinate;
        }
        public HurdleItem(Hurdle hurdle, int rotationDegree)
        {
            _hurdle = hurdle;
            _transferCordinate = Tuple.Create(0, 0);
            _rotationDegreeNumber = rotationDegree % 360;
            _rotationDegreeNumber = _rotationDegreeNumber / 90;
        }
    }
}
