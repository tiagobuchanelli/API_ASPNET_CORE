using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Lojax.Controllers
{
    [Route("v1/companies")]
    public class CompanyController : ControllerBase
    {

        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Company>>> Get([FromServices] DataContext context)
        {


            var entities = await context
            .Companies
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();

            if (entities.Count == 0)
                return NotFound(new { message = "Nenhuma empresa encontrada." });

            return Ok(entities);
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Company>> GetByID(
            int id,
            [FromServices] DataContext context)
        {


            var entity = await context
            .Companies
            .Include(x => x.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound(new { message = "Empresa não encontrada" });


            return Ok(entity);

        }

        //=======POST=======
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Company>> Post(
            [FromBody] Company model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var userCheck = await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Uid == user);

            if (userCheck == null)
                return NotFound(new { message = "usuário não encontrado, não será possível cadastrar da empresa" });

            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Add Empresa
                model.UserId = userCheck.Id;
                model.Uid = user;
                model.Status = 1;
                model.EntityType = 1;
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Companies.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível cadastrar uma empresa" });
            }
        }


        //=======PUT=======
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Company>> Put(

            [FromBody] Company model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var checkCompany = await context
                .Companies
                .AsNoTracking()
                .Where(x => x.Uid == user)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (checkCompany == null)
                return NotFound(new { message = "Nenhuma empresa encontrada" });


            try
            {


                //valida ID da categoria
                // if (user != model.CpnyUid)
                //     return NotFound(new { message = "Usuario que esta tentando alterar é diferente do token" });


                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Atualizar empresa
                model.Uid = user;
                model.Status = checkCompany.Status;
                model.EntityType = checkCompany.EntityType;
                model.DateCreated = checkCompany.DateCreated;
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Entry<Company>(model).State = EntityState.Modified;

                //Salvar no banco 
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateConcurrencyException) //Verifica se existe um dado sendo atualizado ao mesmo tempo.
            {

                return BadRequest(new { message = "Esse registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }
        }







    }
}