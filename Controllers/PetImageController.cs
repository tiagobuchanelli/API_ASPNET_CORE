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

    [Route("v1/pets-image")]
    public class PetImageController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<PetImage>>> Get([FromServices] DataContext context)
        {

            var petI = await context
            .PetImages
            .AsNoTracking()
            .ToListAsync();

            if (petI.Count == 0)
                return NotFound(new { message = "Nenhuma imagem encontrada" });


            return Ok(petI);

        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<PetImage>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var petI = await context
            .PetImages
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (petI == null)
                return NotFound(new { message = "Nenhuma imagem encontrada" });

            return Ok(petI);

        }


        [HttpGet]
        [Route("pets/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<PetImage>>> GetByCategory(
            int id,
            [FromServices] DataContext context)
        {
            var petI = await context
            .PetImages
            .AsNoTracking()
            .Where(x => x.PetId == id)
            .ToListAsync();

            if (petI.Count == 0)
                return NotFound(new { message = "Nenhuma imagem encontrada" });


            return Ok(petI);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<PetImage>> Post(
            [FromBody] PetImage model,
            [FromServices] DataContext context
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                //Add + Salvar DB
                context.PetImages.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar a imagem do produto." });
            }
        }


        //======Put============
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<PetImage>> Put(
            int id,
            [FromBody] PetImage model,
            [FromServices] DataContext context)
        {
            try
            {
                //validar id produto passado
                if (id != model.Id)
                    return NotFound(new { message = "Nenhuma imagem encontrada" });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                context.Entry<PetImage>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model); //poderia retornar uma mensagem de sucesso.

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a imagem do produto" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<PetImage>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var petI = await context.PetImages.FirstOrDefaultAsync(x => x.Id == id);
            if (petI == null)
                return NotFound(new { message = "Imagem não encontrada" });

            try
            {
                context.PetImages.Remove(petI);
                await context.SaveChangesAsync();
                return petI;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a imagem" });

            }
        }

    }
}