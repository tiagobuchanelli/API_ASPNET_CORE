using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lojax.Data;
using Lojax.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Lojax.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices] DataContext context)
        {

            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var categories = await context
            .Categories
            .Include(x => x.Cpny)
            .AsNoTracking()
            .ToListAsync();

            if (categories.Count == 0)
                return NotFound(new { message = "Nenhuma categoria encontrada." });

            return Ok(categories);
        }


        [HttpGet]
        [Route("{id:int}")]
        // [Authorize]
        public async Task<ActionResult<Category>> GetByID(
            int id,
            [FromServices] DataContext context)
        {


            var category = await context
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

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

            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var userCompany = await context
            .Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Uid == user);

            if (userCompany == null)
                return NotFound(new { message = "empresa não encontrada, não será possível cadastrar a categoria" });

            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add Categoria
                model.CpnyId = userCompany.Id;
                model.CpnyUid = userCompany.Uid;
                model.Status = 1;
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
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
        [Authorize]
        public async Task<ActionResult<Category>> Put(
            int id,
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            //validar se a categoria informada pertence ao usuario
            var categ = await context
            .Categories
            .AsNoTracking()
            .Where(x => x.CpnyUid == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (categ == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Categoria não encontrada" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Atualizar Categoria
                model.Status = 1;
                model.DateCreated = categ.DateCreated;
                model.DateUpdate = DateTime.Now.ToLocalTime();
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