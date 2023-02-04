using Paramizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamizerDemo
{
    internal class Param
    {
        [Parameter(name: "name", environmentName: "ParamName")]
        public string Name { get; set; }

        [Parameter(name: "num", environmentName: "NumVal")]
        public int NumVal { get; set; }

        [Parameter(name: "val", shortName: 'v', environmentName: "ParamValue")]
        public string Value { get; set; }

        [Parameter(environmentName: "ParamBool")]
        public bool BoolVal { get; set; }
    }
}
