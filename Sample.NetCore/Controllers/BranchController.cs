using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Models;

namespace Sample.API.Controllers
{
    public class BranchController : Controller
    {
        [HttpGet("api/branches")]
        public IActionResult GetBranches()
        {
            return Ok(BranchDataSource.BranchList.Branches);
        }

        [HttpGet("api/branch/{id}")]
        public IActionResult GetBranch(int id)
        {
            var response = BranchDataSource.BranchList.Branches.Where(x => x.Id == id);
            if(response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

    }
}