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
    [Route("v1/sales-products")]
    public class SaleProductController : ControllerBase
    {
        //======Get============
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<SaleProduct>>> Get([FromServices] DataContext context)
        {

            var salesP = await context
            .SaleProducts
            .Include(x => x.Product)
            .Include(x => x.Sale)
            .AsNoTracking()
            .ToListAsync();

            if (salesP.Count == 0)
                return NotFound(new { message = "Nenhuma lançamento encontrado" });


            return Ok(salesP);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<SaleProduct>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var saleP = await context
            .SaleProducts
            .Include(x => x.Product)
            .Include(x => x.Sale)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (saleP == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(saleP);

        }


        [HttpGet]
        [Route("product/{id:int}")]
        public async Task<ActionResult<List<SaleProduct>>> GetByProduct(
            int id,
            [FromServices] DataContext context)
        {
            var salesP = await context
            .SaleProducts
            .Include(x => x.Product)
            .Include(x => x.Sale)
            .AsNoTracking()
            .Where(x => x.ProductId == id)
            .ToListAsync();

            if (salesP.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(salesP);

        }


        //======Post============
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<SaleProduct>> Post(
           [FromBody] SaleProduct model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.SaleProducts.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível realizar o lançamento." });
            }
        }

        //======Put============
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<SaleProduct>> Put(
            int id,
            [FromBody] SaleProduct model,
            [FromServices] DataContext context)
        {
            try
            {
                //validar id produto passado
                if (id != model.Id)
                    return NotFound(new { message = "Lançamento não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                context.Entry<SaleProduct>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model); //poderia retornar uma mensagem de sucesso.

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o lançamento" });
            }
        }

        //======DELETE============
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<SaleProduct>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var saleP = await context.SaleProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (saleP == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.SaleProducts.Remove(saleP);
                await context.SaveChangesAsync();
                return saleP;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }
    }
}