using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MDMWebApiTemplate.DbModels
{
  public class MDMWebApiTemplateDbContext : DbContext
  {
    public MDMWebApiTemplateDbContext(DbContextOptions<MDMWebApiTemplateDbContext> options)
      : base(options)
    {
    }

  }
}
