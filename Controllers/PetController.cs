using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Models;
using Lojax.Data;
using System;

namespace Lojax.Controllers
{

    [Route("v1/pets")]
    public class PetController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Pet>>> Get([FromServices] DataContext context)
        {

            var pets = await context
            .Pets
            .Include(x => x.User) //Como foi inserido o objeto completo da categoria no model do produto, agora é possivel recuperá-lo com include.
            .AsNoTracking()
            .ToListAsync();

            if (pets.Count == 0)
                return NotFound(new { message = "Nenhum pet encontrado" });


            return Ok(pets);

        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Pet>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var pet = await context
            .Pets
            .Include(x => x.User) //Como foi inserido o objeto completo da categoria no model do produto, agora é possivel recuperá-lo com include.
            .AsNoTracking()
            .Where(x => x.UserId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (pet == null)
                return NotFound(new { message = "Nenhum pet encontrado" });

            return Ok(pet);

        }


        [HttpGet]
        [Route("user")]
        [Authorize]
        public async Task<ActionResult<List<Pet>>> GetByUser(
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var pets = await context
            .Pets
            .Include(x => x.User)
            .AsNoTracking()
            .Where(x => x.UserId == user)
            .ToListAsync();

            if (pets.Count == 0)
                return NotFound(new { message = "Nenhum pet encontrado" });


            return Ok(pets);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Pet>> Post(
            [FromBody] Pet model,
            [FromServices] DataContext context
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                //Add + Salvar DB
                model.UserId = user;
                context.Pets.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar o pet." });
            }
        }


        //======Put============
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Pet>> Put(
            int id,
            [FromBody] Pet model,
            [FromServices] DataContext context)
        {

            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            try
            {
                //validar id produto passado
                if (id != model.Id)
                    return NotFound(new { message = "Pet não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.UserId = user;
                context.Entry<Pet>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model); //poderia retornar uma mensagem de sucesso.

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o pet" });
            }
        }

    }
}