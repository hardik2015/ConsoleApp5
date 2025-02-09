using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public struct Hurdle
    {
        public string _hurdleName { get; private set; }
        public HurdleType _hurdleType { get; private set; }

        [JsonConstructor]
        public Hurdle(string _hurdleName, HurdleType _hurdleType)
        {
            this._hurdleName = _hurdleName;
            this._hurdleType = _hurdleType;
        }

    }
}
