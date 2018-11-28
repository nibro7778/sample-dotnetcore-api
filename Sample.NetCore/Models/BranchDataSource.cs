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
                    ContactPersons = new List<ContactPersonDto>()
                    {
                        new ContactPersonDto()
                        {
                            Id = 1,
                            Name = "Rob",
                            ContactNo = "0470237375"
                        },
                        new ContactPersonDto()
                        {
                            Id = 2,
                            Name = "John",
                            ContactNo = "0470237376"
                        }
                    }

                },
                new BranchDto()
                {
                    Id = 2,
                    Name = "Sydney",
                    ContactPersons = new List<ContactPersonDto>()
                    {
                        new ContactPersonDto()
                        {
                            Id = 3,
                            Name = "Jems",
                            ContactNo = "0470237376"
                        }
                    }
                }
            };
        }

    }
}
