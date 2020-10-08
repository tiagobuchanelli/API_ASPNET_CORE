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

    [Route("v1/products")]
    public class Productcontroller : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {

            var products = await context
            .Products
            .Include(x => x.Category) //Como foi inserido o objeto completo da categoria no model do produto, agora é possivel recuperá-lo com include.
            .Include(x => x.Cpny)
            .AsNoTracking()
            .ToListAsync();

            if (products.Count == 0)
                return NotFound(new { message = "Nenhum produto encontrado" });


            return Ok(products);

        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Product>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var product = await context
            .Products
            .Include(x => x.Category) //Como foi inserido o objeto completo da categoria no model do produto, agora é possivel recuperá-lo com include.
            .Include(x => x.Cpny)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound(new { message = "Nenhum produto encontrado" });

            return Ok(product);

        }


        [HttpGet]
        [Route("categories/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<Product>>> GetByCategory(
            int id,
            [FromServices] DataContext context)
        {
            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var products = await context
            .Products
            .Include(x => x.Category)
            .Include(x => x.Cpny)
            .AsNoTracking()
            .Where(x => x.CategoryId == id)
            .ToListAsync();

            if (products.Count == 0)
                return NotFound(new { message = "Nenhum produto encontrado" });


            return Ok(products);

        }

        [HttpGet]
        [Route("my-company")]
        [Authorize]
        public async Task<ActionResult<List<Product>>> GetByCompany(
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var products = await context
            .Products
            .Include(x => x.Category)
            .Include(x => x.Cpny)
            .AsNoTracking()
            .Where(x => x.CpnyUid == user)
            .ToListAsync();

            if (products.Count == 0)
                return NotFound(new { message = "Nenhum produto encontrado" });


            return Ok(products);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Product>> Post(
            [FromBody] Product model,
            [FromServices] DataContext context
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var checkCompany = await context
                .Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Uid == user);

            if (checkCompany == null)
                return NotFound(new { message = "Nenhuma empresa encontrada" });


            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                //Add + Salvar DB
                model.CpnyId = checkCompany.Id;
                model.CpnyUid = checkCompany.Uid;
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Products.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar o produto." });
            }
        }


        //======Put============ 
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Product>> Put(
            [FromBody] Product model,
            [FromServices] DataContext context)
        {

            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var checkProd = await context
                .Products
                .AsNoTracking()
                .Where(x => x.CpnyUid == user)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (checkProd == null)
                return NotFound(new { message = "Nenhuma empresa encontrada" });

            try
            {
                //validar id produto passado
                // if (id != model.Id)
                //     return NotFound(new { message = "Produto não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.CpnyId = checkProd.CpnyId;
                model.CpnyUid = checkProd.CpnyUid;
                model.DateCreated = checkProd.DateCreated;
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Entry<Product>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model); //poderia retornar uma mensagem de sucesso.

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o produto" });
            }
        }

    }
}