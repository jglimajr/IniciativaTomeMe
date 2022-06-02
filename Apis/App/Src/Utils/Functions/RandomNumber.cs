using System;
using InteliSystem.Utils.Extensions;

namespace InteliSystem.Utils.Functions
{

    public static class RandomNumber
    {
        public static string GetNumber(int maxvalue = 99999999)
        {
            Random rd = new Random();
            return rd.Next(1, maxvalue).ZeroToLeft(8);
        }

    }

}