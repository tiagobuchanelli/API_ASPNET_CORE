using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;

namespace Lojax.Controllers
{
    [Route("v1/settings")]
    public class Settings : ControllerBase
    {
        //=======GET=======
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Setting>>> Get([FromServices] DataContext context)
        {

            var settings = await context.Settings.AsNoTracking().ToListAsync();

            if (settings.Count == 0)
                return NotFound(new { message = "Nenhuma configuração encontrada." });

            return Ok(settings);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var setting = await context.Settings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (setting == null)
                return NotFound(new { message = "Configuração não encontrada" });


            return Ok(setting);

        }


        //=======POST=======
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Setting model,
            [FromServices] DataContext context)
        {
            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add Categoria
                context.Settings.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível criar a configuração" });
            }
        }



        //=======PUT=======
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(
            int id,
            [FromBody] Setting model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Configuração não encontrada" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Atualizar Categoria
                context.Entry<Setting>(model).State = EntityState.Modified;

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
                return BadRequest(new { message = "Não foi possível atualizar a configuração" });
            }
        }

    }

}