using System.Runtime.Serialization;

namespace ConsoleApp5
{
    public enum HurdleType
    {
        [EnumMember(Value = "1")]
        BlockType=1,
        [EnumMember(Value = "2")]
        TransferType = 2,
        [EnumMember(Value = "3")]
        RotationType = 3
    }
}
