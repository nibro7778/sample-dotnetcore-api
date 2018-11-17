using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Models
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int NumberofContactPerson { get
            {
                return ContactPersons.Count;
            } }

        public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
    }
}
