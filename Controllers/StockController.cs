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
    [Route("v1/stock")]
    public class StockController : ControllerBase
    {
        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Stock>>> Get([FromServices] DataContext context)
        {

            var stocks = await context
            .Stocks
            .Include(x => x.Product)
            .AsNoTracking()
            .ToListAsync();

            if (stocks.Count == 0)
                return NotFound(new { message = "Nenhum estoque encontrado" });


            return Ok(stocks);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Stock>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var stock = await context
            .Stocks
           .Include(x => x.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
                return NotFound(new { message = "Nenhum estoque encontrado" });

            return Ok(stock);

        }

        //======Post============
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Stock>> Post(
           [FromBody] Stock model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.Stocks.Add(model);
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
        public async Task<ActionResult<Stock>> Put(
            int id,
            [FromBody] Stock model,
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
                context.Entry<Stock>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<Stock>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.Stocks.Remove(stock);
                await context.SaveChangesAsync();
                return stock;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }
    }

}