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

    [Route("v1/products-image")]
    public class ProductsImageController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<ProductsImage>>> Get([FromServices] DataContext context)
        {

            var productsI = await context
            .ProductsImage
            .AsNoTracking()
            .ToListAsync();

            if (productsI.Count == 0)
                return NotFound(new { message = "Nenhuma imagem encontrada" });


            return Ok(productsI);

        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ProductsImage>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var productI = await context
            .ProductsImage
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (productI == null)
                return NotFound(new { message = "Nenhuma imagem encontrada" });

            return Ok(productI);

        }


        [HttpGet]
        [Route("product/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<ProductsImage>>> GetByCategory(
            int id,
            [FromServices] DataContext context)
        {
            var productsI = await context
            .ProductsImage
            .AsNoTracking()
            .Where(x => x.ProductId == id)
            .ToListAsync();

            if (productsI.Count == 0)
                return NotFound(new { message = "Nenhuma imagem encontrada" });


            return Ok(productsI);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<ProductsImage>> Post(
            [FromBody] ProductsImage model,
            [FromServices] DataContext context
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                //Add + Salvar DB
                context.ProductsImage.Add(model);
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
        public async Task<ActionResult<ProductsImage>> Put(
            int id,
            [FromBody] ProductsImage model,
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
                context.Entry<ProductsImage>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<ProductsImage>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var productsI = await context.ProductsImage.FirstOrDefaultAsync(x => x.Id == id);
            if (productsI == null)
                return NotFound(new { message = "Imagem não encontrada" });

            try
            {
                context.ProductsImage.Remove(productsI);
                await context.SaveChangesAsync();
                return productsI;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });

            }
        }

    }
}