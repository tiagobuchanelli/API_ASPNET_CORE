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
    [Route("v1/order-products")]
    public class OrderProductController : ControllerBase
    {
        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<OrderProduct>>> Get([FromServices] DataContext context)
        {

            var salesP = await context
            .OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order)
            .AsNoTracking()
            .ToListAsync();

            if (salesP.Count == 0)
                return NotFound(new { message = "Nenhuma lançamento encontrado" });


            return Ok(salesP);

        }

        [HttpGet]
        [Route("order/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<OrderProduct>>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var saleP = await context
            .OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order)
            .AsNoTracking()
            .Where(x => x.OrderId == id)
            .ToListAsync();

            if (saleP == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(saleP);

        }


        [HttpGet]
        [Route("product/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<OrderProduct>>> GetByProduct(
            int id,
            [FromServices] DataContext context)
        {
            var salesP = await context
            .OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order)
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
        [Authorize]
        public async Task<ActionResult<OrderProduct>> Post(
           [FromBody] OrderProduct model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.OrderProducts.Add(model);
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
        [Authorize]
        public async Task<ActionResult<OrderProduct>> Put(
            int id,
            [FromBody] OrderProduct model,
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
                context.Entry<OrderProduct>(model).State = EntityState.Modified;
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
        [Authorize]
        public async Task<ActionResult<OrderProduct>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var saleP = await context.OrderProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (saleP == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.OrderProducts.Remove(saleP);
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