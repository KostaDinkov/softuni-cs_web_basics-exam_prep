using BattleCards.Data;
using BattleCards.Services;
using Microsoft.EntityFrameworkCore;

namespace BattleCards
{
    using System.Collections.Generic;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService,UserService>();
            serviceCollection.Add<ICardService,CardService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
