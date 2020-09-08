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
    [Route("v1/stock-mov")]
    public class StockMovController : ControllerBase
    {


        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<StockMov>>> Get([FromServices] DataContext context)
        {

            var stockMov = await context
            .StockMovs
            .Include(x => x.Product)
            .AsNoTracking()
            .ToListAsync();

            if (stockMov.Count == 0)
                return NotFound(new { message = "Nenhum estoque encontrado" });


            return Ok(stockMov);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<StockMov>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var stockMov = await context
            .StockMovs
           .Include(x => x.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (stockMov == null)
                return NotFound(new { message = "Nenhum estoque encontrado" });

            return Ok(stockMov);

        }

        //======Post============
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<StockMov>> Post(
           [FromBody] StockMov model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.StockMovs.Add(model);
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
        public async Task<ActionResult<StockMov>> Put(
            int id,
            [FromBody] StockMov model,
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
                context.Entry<StockMov>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<StockMov>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var stockMov = await context.StockMovs.FirstOrDefaultAsync(x => x.Id == id);
            if (stockMov == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.StockMovs.Remove(stockMov);
                await context.SaveChangesAsync();
                return stockMov;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }
    }

}