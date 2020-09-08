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
    [Route("v1/sales")]
    public class SaleController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Sale>>> Get([FromServices] DataContext context)
        {

            var sales = await context
            .Sales
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .ToListAsync();

            if (sales.Count == 0)
                return NotFound(new { message = "Nenhuma venda encontrada" });


            return Ok(sales);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Sale>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var sale = await context
            .Sales
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(sale);

        }


        [HttpGet]
        [Route("entity/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<Sale>>> GetByEntity(
            int id,
            [FromServices] DataContext context)
        {
            var sales = await context
            .Sales
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .Where(x => x.CostumerId == id)
            .ToListAsync();

            if (sales.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(sales);

        }


        //======Post============
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Sale>> Post(
           [FromBody] Sale model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.Sales.Add(model);
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
        public async Task<ActionResult<Sale>> Put(
            int id,
            [FromBody] Sale model,
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
                context.Entry<Sale>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<Sale>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (sale == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.Sales.Remove(sale);
                await context.SaveChangesAsync();
                return sale;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }

    }
}