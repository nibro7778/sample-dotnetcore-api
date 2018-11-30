using Sample.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Models
{
    public static class BranchInfoExtensions
    {
        public static void EnsureSeedDataForContext(this BranchInfoContext context)
        {
            if(context.Branches.Any())
            {
                return;
            }

            var branches = new List<Branch>()
            {
                new Branch()
                {
                    Name = "Sydney",
                    ContactPersons = new List<ContactPerson>()
                    {
                        new ContactPerson()
                        {
                            Name = "John",
                            ContactNo = "0470123456"
                        }
                    }
                }
            };
            context.Branches.AddRange(branches);
            context.SaveChanges();
        }
    }
}
