using DbTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest
{
    internal class CoreContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        public CoreContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CoreContext>();
            builder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DbTest;Integrated Security=True");


            return new CoreContext(null, builder.Options);
        }
    }
}
