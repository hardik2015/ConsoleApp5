using Newtonsoft.Json;


namespace ConsoleApp5
{
    public class RobotConfiguration
    {
        [JsonProperty("robotId")]
        public int robotId {  get; set; }
        [JsonProperty("robotName")]
        public string robotName {  get; set; }
        [JsonProperty("hurdles")]
        public List<Hurdle> hurdles { get; set; }
        [JsonConverter(typeof(CustomDictionaryConverter<Tuple<int, int>, HurdleItem>))]
        [JsonProperty("hurdlesGrid")]
        public Dictionary<Tuple<int, int>, HurdleItem> hurdlesGrid { get; set; }
        [JsonProperty("xGridSize")]
        public int xGridSize { get; set; }
        [JsonProperty("yGridSize")]
        public int yGridSize { get; set; }
    }
}
