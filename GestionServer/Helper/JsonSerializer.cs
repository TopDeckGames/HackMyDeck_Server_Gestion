using System;
using Newtonsoft.Json;

namespace GestionServer.Helper
{
    public static class JsonSerializer
    {
        public static string toJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}