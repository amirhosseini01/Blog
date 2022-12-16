using Microsoft.EntityFrameworkCore;
using Site.Areas_Admin_Pages;
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
        [Fact]
        public async Task GetById_StatusCode_Ok()
        {
            IMenuRep menuRep = new MenuRep(dbContext);
            MenusModel menusModel = new(menuRep);

            var result = await menusModel.OnGetGetById(1);

            Assert.False(result.StatusCode == 404, "status code is not successfull status code");
        }
    }
}
