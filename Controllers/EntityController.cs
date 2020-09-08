using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lojax.Controllers
{
    [Route("v1/entities")]
    public class EntityController : ControllerBase
    {

        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Entity>>> Get([FromServices] DataContext context)
        {

            var entities = await context.Entities.AsNoTracking().ToListAsync();

            if (entities.Count == 0)
                return NotFound(new { message = "Nenhum usuário encontrado." });

            return Ok(entities);
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Entity>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var entity = await context.Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return NotFound(new { message = "Usuário não encontrado" });


            return Ok(entity);

        }

        //=======POST=======
        [HttpPost]
        [Route("")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Entity>> Put(
            int id,
            [FromBody] Entity model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Usuário não encontrado" });

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
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }
        }

    }
}