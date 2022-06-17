using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Features.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteTest.Areas.Pages
{
    public class MenusTest
    {
        DataBaseContext dbContext;

        public MenusTest()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                 .UseInMemoryDatabase(databaseName: "FakeDatabase")
                 .Options;

            dbContext = new DataBaseContext(options);
        }
    }
}
