using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;

namespace Lojax.Controllers
{
    [Route("v1/costumers")]
    public class CostumerController : ControllerBase
    {

        //=======GET=======
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Entity>>> Get([FromServices] DataContext context)
        {

            var costumers = await context.Entities.AsNoTracking().ToListAsync();

            if (costumers.Count == 0)
                return NotFound(new { message = "Nenhum cliente encontrado." });

            return Ok(costumers);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Entity>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var costumer = await context.Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (costumer == null)
                return NotFound(new { message = "Cliente não encontrado" });


            return Ok(costumer);

        }

        //=======POST=======
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Entity>> Post(
            [FromBody] Entity model,
            [FromServices] DataContext context)
        {
            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add Categoria
                context.Entities.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível cadastrar um usuário" });
            }
        }


        //=======PUT=======
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Entity>> Put(
            int id,
            [FromBody] Entity model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Cliente não encontrado" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Atualizar Categoria
                context.Entry<Entity>(model).State = EntityState.Modified;

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
                return BadRequest(new { message = "Não foi possível atualizar o cliente" });
            }
        }

    }
}