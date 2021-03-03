using System;
using Services;
namespace AuthorizationApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var hashPassword = EncryiptionService.GetHashString("password");
        }
    }
}
