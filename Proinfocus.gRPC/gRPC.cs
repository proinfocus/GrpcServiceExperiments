using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Proinfocus
{
    public static class gRPC
    {
        public static void CreateEndpoints<T>() where T : class
        {
            var service = Activator.CreateInstance(typeof(T));
            var methods = service.GetType().GetMethods().Where(a => a.DeclaringType != typeof(object));

            string path = Path.Combine(Environment.CurrentDirectory, "Endpoints");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string fileName = Path.Combine(path, service.GetType().Name + "Endpoints.cs");


            string template = GetContent("template.txt");
            string templateContent = GetContent("template-content.txt");

            template = template.Replace("{SERVICE}", service.GetType().Name);

            var data = new StringBuilder();

            foreach (var method in methods)
            {
                var serviceMethod = templateContent;
                serviceMethod = serviceMethod.Replace("{SERVICE}", service.GetType().Name.Replace("Service",""));
                serviceMethod = serviceMethod.Replace("{METHOD}", method.Name);                
                var args = method.GetParameters();
                foreach (var arg in args.Where(a => !a.ParameterType.Name.Contains("ServerCallContext")))
                    serviceMethod = serviceMethod.Replace("{REQUEST}", arg.ParameterType.Name);

                data.AppendLine(serviceMethod);
                data.AppendLine();
            }

            template = template.Replace("{NAMESPACE}", Assembly.GetEntryAssembly().GetName().Name);
            template = template.Replace("{ENDPOINTS}", data.ToString());

            File.WriteAllText(fileName, template);
        }

        private static string GetContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}." + fileName;

            string resource = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using StreamReader reader = new StreamReader(stream);
                resource = reader.ReadToEnd();
            }
            return resource;
        }
    }

    
}
