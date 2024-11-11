namespace WebApp.Middleware
{
    public static class Middleware
    {
        public static void UseMiddlewareFilter(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var userId = context.Session.GetString("userId");
                var path = context.Request.Path;
                if (!string.IsNullOrEmpty(userId))
                {
                    //var role = context.Session.GetString("role");
                    //if (!string.IsNullOrEmpty(role))
                    //{
                    //    if (int.Parse(role) == 0)
                    //    {
                    //        await next(context);
                    //    }
                    //    else
                    //    {
                    //        if (path.StartsWithSegments("/Admin"))
                    //        {
                    //            context.Response.Redirect("/Homepage");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    await next(context);
                    //}
                    await next(context);
                    
                }
                else
                {
                    if (path.StartsWithSegments("/Login") || path.StartsWithSegments("/Homepage") || path.StartsWithSegments("/"))
                    {
                        await next(context);
                    }
                    else
                    {
                        context.Response.Redirect("/Login");
                    }
                }
            });
        }
    }
}
