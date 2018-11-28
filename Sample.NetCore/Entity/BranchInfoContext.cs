using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Entity
{
    public class BranchInfoContext: DbContext
    {
        public BranchInfoContext(DbContextOptions<BranchInfoContext> option)
            : base(option)
        {
            Database.Migrate();
        }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<ContactPerson> ContactPerson { get; set; }

    }
}
