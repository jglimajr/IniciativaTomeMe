using System;

namespace InteliSystem.Utils.GlobalClasses
{
    public class EnvironmentVariables
    {
        public EnvironmentVariables() { }
        public EnvironmentVariables(string ourEnvironment, string urlApiImage)
        {
            OurEnvironment = ourEnvironment;
            UrlApiImage = urlApiImage;
        }

        public string OurEnvironment { get; set; }
        public string UrlApiImage { get; set; }
    }
}