using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TestHelioAppOwin.Startup))]

namespace TestHelioAppOwin
{
    public class Startup
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

        public MW1(Func<IDictionary<string, object>, Task> fnc)
        {
            obj = fnc;
        }
        public async Task Invoke(IDictionary<string, object> env)
        {
            var ctx = new OwinContext(env);

            ctx.Response.Write("<h1>In MW1</h1>");

            await obj(env);

            ctx.Response.Write("Program Terminated");

        }

    }

    public class MW2
    {
        Func<IDictionary<string, object>, Task> obj;

        public MW2(Func<IDictionary<string, object>, Task> fnc)
        {
            obj = fnc;
        }
        public Task Invoke(IDictionary<string, object> env)
        {
            var ctx = new OwinContext(env);

            ctx.Response.Write("<h1>In MW2</h1>");

            return Task.FromResult(0);

        }
    }
}
