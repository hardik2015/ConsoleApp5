using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
