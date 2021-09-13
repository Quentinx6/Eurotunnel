using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;
using API.Data;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ITAccountController : ControllerBase
    {
        private readonly IITAccountRepository _repo;

        public ITAccountController(IITAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult>Resgister(ITAccountForRegisterDto iTAccountForRegisterDto)
        {
            var iTAccountToCreate = new ITAccount
            {
                Matricule = iTAccountForRegisterDto.Matricule,
                ADAccountID = iTAccountForRegisterDto.ITAccountID,
                CurrentStep = iTAccountForRegisterDto.CurrentStep
            };

            var creatediTAccount = await _repo.Register(iTAccountToCreate);

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult>GetAllITAccount()
        {
            var accountsFromDb = await _repo.GetAllAccounts();
            
            return Ok(accountsFromDb);
        }
    }
}