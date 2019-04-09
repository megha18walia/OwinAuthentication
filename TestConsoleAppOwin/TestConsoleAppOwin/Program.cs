using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleAppOwin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<StartUp>("http://localhost:12345"))
            {
                Console.WriteLine("Ready");
                Console.ReadLine();

            }
        }

        public class StartUp
        {
            public void Configuration(IAppBuilder app)
            {
                app.Use(typeof(MW1));
                app.Use<MW2>();
            }
        }

        public class MW1
        {
            Func<IDictionary<string, object>, Task> obj;
            public MW1(Func<IDictionary<string, object>, Task> next)
            {
                obj = next;
            }
            public async Task Invoke(IDictionary<string, object> env)
            {
                //foreach (var d in env)
                //{
                //    Console.WriteLine("{0} : {1}", d.Key, d.Value);
                //    Console.WriteLine("in MW1");
                //}
                Console.WriteLine("In MW1");
                await obj(env);

                Console.WriteLine("ACtion Performed");
              //  env["owin.ResponseStatusCode"] = 404;
            }
        }

        public class MW2
        {
            Func<IDictionary<string, object>, Task> obj;

            public MW2(Func<IDictionary<string, object>, Task> res)
            {
                obj = res;
            }
            public Task Invoke(IDictionary<string, object> env)
            {
                Console.WriteLine("In MW2 Task Terminated");
                return Task.FromResult(0);
            }
        }
    }
}
