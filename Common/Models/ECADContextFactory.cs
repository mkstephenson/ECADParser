using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
  public class ECADContextFactory : IDesignTimeDbContextFactory<ECADContext>
  {
    public ECADContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ECADContext>();
      optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true");

      return new ECADContext(optionsBuilder.Options);
    }
  }
}
