using Microsoft.EntityFrameworkCore;

namespace SharedTrip
{
    using System.Collections.Generic;

    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
           new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            
        }
    }
}
