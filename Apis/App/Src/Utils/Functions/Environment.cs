using System.Collections.Specialized;
using System.Configuration;
using InteliSystem.Utils.Extensions;
using Microsoft.Extensions.Configuration;

namespace InteliSystem.Utils.Functions
{
    public class Environment
    {
        private readonly IConfiguration _appSettings;
        public Environment(IConfiguration appSettings)
        {
            this._appSettings = appSettings;
        }

        public string GetEnviroment => GetOurEnvironment("EnvironmentVariables:OurEnvironment");
        public string GetUrlApiImage => GetOurEnvironment("EnvironmentVariables:UrlApiImage");
        public string GetDataBasePortal => GetOurEnvironment("ConnectionStrings:DefaultConnection");
        public string GetDataBaseApp => GetOurEnvironment("ConnectionStrings:DefaultConnectionApp");
        public double GetRadius => GetOurEnvironment("Location:Radius").ObjectToString().ToDouble();
        private string GetOurEnvironment(string key)
        {
            return _appSettings[key].ToString();
        }
    }
}