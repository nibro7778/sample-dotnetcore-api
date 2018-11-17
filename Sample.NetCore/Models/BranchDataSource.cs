using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Models
{
    //In memory data source for branch
    public class BranchDataSource
    {
        public static BranchDataSource BranchList { get; } = new BranchDataSource();

        public List<BranchDto> Branches { get; set; }

        public BranchDataSource()
        {
            Branches = new List<BranchDto>()
            {
                new BranchDto()
                {
                    Id = 1,
                    Name = "New York",
                    ContactPersons = new List<ContactPerson>()
                    {
                        new ContactPerson()
                        {
                            Id = 1,
                            Name = "Rob"
                        },
                        new ContactPerson()
                        {
                            Id = 1,
                            Name = "John"
                        }
                    }

                },
                new BranchDto()
                {
                    Id = 2,
                    Name = "Sydney",
                    ContactPersons = new List<ContactPerson>()
                    {
                        new ContactPerson()
                        {
                            Id = 3,
                            Name = "Jems"
                        }
                    }
                }
            };
        }

    }
}
