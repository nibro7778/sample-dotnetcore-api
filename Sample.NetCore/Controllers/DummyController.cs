using Microsoft.AspNetCore.Mvc;
using Sample.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Controllers
{
    public class DummyController : Controller
    {
        private BranchInfoContext _dbContext;

        public DummyController(BranchInfoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }

    }
}
