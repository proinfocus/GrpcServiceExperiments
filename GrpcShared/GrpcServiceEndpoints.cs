using System;
using System.Linq;

namespace GrpcService.Shared
{
    public class GrpcServiceEndpoints
    {
        public static void CreateEndpoints<T>(this WebApplication app) where T : class
        {
            var service = Activator.CreateInstance(typeof(T));
            var methods = service.GetType().GetMethods( ).Where(a => a.DeclaringType != typeof(object));

            foreach(var method in methods)
            {
                Console.WriteLine(method.Name);
                var args = method.GetParameters();
                foreach(var arg in args.Where(a => !a.ParameterType.Name.Contains("ServerCallContext")))
                    Console.WriteLine(arg.ParameterType.Name);
            }
            
        }
    }
}
