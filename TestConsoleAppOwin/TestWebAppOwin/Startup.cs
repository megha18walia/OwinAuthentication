using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TestWebAppOwin.Startup))]

namespace TestWebAppOwin
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(MW1));
            app.Map("/foo", fooApp =>
            {
                fooApp.Use<MW2>();
            });
            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("<h1>MW3</h2>");
                await next();
            });

           
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
