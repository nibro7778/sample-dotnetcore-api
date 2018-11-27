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

        
    }
}