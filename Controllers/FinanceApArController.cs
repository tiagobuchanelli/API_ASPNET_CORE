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
    [Route("v1/finance")]
    public class FinanceController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<FinanceApAr>>> Get([FromServices] DataContext context)
        {

            var finances = await context
            .FinanceApArs
            .Include(x => x.Entity)
            .AsNoTracking()
            .ToListAsync();

            if (finances.Count == 0)
                return NotFound(new { message = "Nenhuma conta encontrada" });


            return Ok(finances);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<FinanceApAr>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var finance = await context
            .FinanceApArs
            .Include(x => x.Entity)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (finance == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(finance);

        }


        [HttpGet]
        [Route("entity/{id:int}")]
        public async Task<ActionResult<List<FinanceApAr>>> GetByEntity(
            int id,
            [FromServices] DataContext context)
        {
            var finance = await context
            .FinanceApArs
            .Include(x => x.Entity)
            .AsNoTracking()
            .Where(x => x.EntityId == id)
            .ToListAsync();

            if (finance.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(finance);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<FinanceApAr>> Post(
           [FromBody] FinanceApAr model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.FinanceApArs.Add(model);
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
        public async Task<ActionResult<FinanceApAr>> Put(
            int id,
            [FromBody] FinanceApAr model,
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
                context.Entry<FinanceApAr>(model).State = EntityState.Modified;
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

    }
}
