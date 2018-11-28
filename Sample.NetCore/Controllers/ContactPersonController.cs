using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.API.Models;
using Sample.API.Services;

namespace Sample.API.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ContactPersonController : Controller
    {
        private ILogger<ContactPersonController> _logger;
        private IMailService _mailService;

        public ContactPersonController(ILogger<ContactPersonController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }


        [HttpGet("{branchId}/contactperson/{id}", Name = "GetContactPerson")]
        public IActionResult GetContactPerson(int branchId, int id)
        {
            var branch = BranchDataSource.BranchList.Branches.FirstOrDefault(x => x.Id == branchId);

            if (branch == null)
            {
                _logger.LogInformation($"Branch with {branchId} is not found");
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

            var newContactPerson = new ContactPersonDto()
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
            if (contactPerson == null)
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

            if (contactPersonFromStore == null)
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
            if (patchDoc == null)
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

        [HttpDelete("{branchId}/contactperson/{id}")]
        public IActionResult DeleteContactPerson(int branchId, int id)
        {
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

            branch.ContactPersons.Remove(contactPersonFromStore);

            _mailService.Send("Deleted", "Contact person deleted");

            return NoContent();
        }
    }
}