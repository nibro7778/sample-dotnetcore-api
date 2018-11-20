using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Models;

namespace Sample.API.Controllers
{
    [Route("api")]
    public class BranchController : Controller
    {
        [HttpGet("branches")]
        public IActionResult GetBranches()
        {
            return Ok(BranchDataSource.BranchList.Branches);
        }

        [HttpGet("branch/{id}")]
        public IActionResult GetBranch(int id)
        {
            var response = BranchDataSource.BranchList.Branches.Where(x => x.Id == id);
            if(response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{branchId}/contactperson/{id}", Name= "GetContactPerson")]
        public IActionResult GetContactPerson(int branchId, int id)
        {
            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if (branch == null)
            {
                return NotFound();
            }

            var contactPerson = branch.ContactPersons.FirstOrDefault(x => x.Id == id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return Ok(contactPerson);
        }

        [HttpPost("{branchId}/contactperson")]
        public IActionResult CreateContactPerson(int branchId, [FromBody] ContactPersonForCreationDto contactPerson)
        {
            if (contactPerson == null)
            {
                return BadRequest();
            }

            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if(branch == null)
            {
                return NotFound();
            }

            var maxContactPersonId = BranchDataSource.BranchList.Branches.SelectMany(x => x.ContactPersons).Max(c => c.Id);

            var newContactPerson = new ContactPerson()
            {
                Id = maxContactPersonId + 1,
                Name = contactPerson.Name
            };

            branch.ContactPersons.Add(newContactPerson);

            return CreatedAtRoute("GetContactPerson", 
                new { branchId = branchId, id = maxContactPersonId }, newContactPerson);

        }

    }
}