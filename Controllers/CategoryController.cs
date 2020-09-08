using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lojax.Data;
using Lojax.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Lojax.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {

            var categories = await context.Categories.AsNoTracking().ToListAsync();

            if (categories.Count == 0)
                return NotFound(new { message = "Nenhuma categoria encontrada." });

            return Ok(categories);
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Category>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });


            return Ok(category);

        }


        //=======POST=======
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add Categoria
                context.Categories.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível criar a categoria" });
            }
        }



        //=======PUT=======
        [HttpPut]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> Put(
            int id,
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Categoria não encontrada" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Atualizar Categoria
                context.Entry<Category>(model).State = EntityState.Modified;

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
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });
            }
        }

    }

}