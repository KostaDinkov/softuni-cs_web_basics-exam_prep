using Microsoft.EntityFrameworkCore;
using SharedTrip.Services;

namespace SharedTrip
{
    using System.Collections.Generic;

    using SUS.HTTP;
    using SUS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ITripsService,TripsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
