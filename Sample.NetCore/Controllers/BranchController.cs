using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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
            if (response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{branchId}/contactperson/{id}", Name = "GetContactPerson")]
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

            if (!ModelState.IsValid)
            {
                //You can add your custom message
                ModelState.AddModelError("Error", "Please provide required field value");
                return BadRequest(ModelState);
            }

            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if (branch == null)
            {
                return NotFound();
            }

            var maxContactPersonId = BranchDataSource.BranchList.Branches.SelectMany(x => x.ContactPersons).Max(c => c.Id);

            var newContactPerson = new ContactPerson()
            {
                Id = maxContactPersonId + 1,
                Name = contactPerson.Name,
                ContactNo = contactPerson.ContactNo
            };

            branch.ContactPersons.Add(newContactPerson);

            return CreatedAtRoute("GetContactPerson",
                new { branchId = branchId, id = maxContactPersonId }, newContactPerson);

        }

        [HttpPut("{branchId}/contactperson/{id}")]
        public IActionResult UpdateContactPerson(int branchId, int id, [FromBody] ContactPersonForUpdateDto contactPerson)
        {
            if(contactPerson == null)
            {
                return BadRequest();
            }

            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if (branch == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                //You can add your custom message
                ModelState.AddModelError("Error", "Please provide required field value");
                return BadRequest(ModelState);
            }

            var contactPersonFromStore = branch.ContactPersons.FirstOrDefault(x => x.Id == id);

            if(contactPersonFromStore ==  null)
            {
                return NotFound();
            }

            contactPersonFromStore.Name = contactPerson.Name;
            contactPersonFromStore.ContactNo = contactPerson.ContactNo;

            return NoContent();
        }

        [HttpPatch("{branchId}/contactperson/{id}")]
        public IActionResult UpdateContactPerson(int branchId, int id, [FromBody] JsonPatchDocument<ContactPersonForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if (branch == null)
            {
                return NotFound();
            }

            var contactPersonFromStore = branch.ContactPersons.FirstOrDefault(x => x.Id == id);

            if (contactPersonFromStore == null)
            {
                return NotFound();
            }

            var contactPersonToPatch = new ContactPersonForUpdateDto()
            {
                Name = contactPersonFromStore.Name,
                ContactNo = contactPersonFromStore.ContactNo
            };

            patchDoc.ApplyTo(contactPersonToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contactPersonFromStore.Name = contactPersonToPatch.Name;
            contactPersonFromStore.ContactNo = contactPersonToPatch.ContactNo;

            return NoContent();
        }
    }
}