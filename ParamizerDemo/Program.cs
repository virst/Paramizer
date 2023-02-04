using Paramizer;
using System;
using System.Text.Json;

namespace ParamizerDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var prm = ParamizerConfigLoader.ParseParams<Param>(fn: args[0]);
                Console.WriteLine(ParamizerConfigUtils.ToString(prm));
            }
        }
    }
}
