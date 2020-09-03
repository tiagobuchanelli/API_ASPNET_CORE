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
    [Route("v1/finance-installment")]
    public class FinanceInstallmentController : ControllerBase
    {
        //======Get============
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<FinanceInstallment>>> Get([FromServices] DataContext context)
        {

            var financesI = await context
            .FinanceInstallments
            .Include(x => x.Finance)
            .Include(x => x.Entity)
            .AsNoTracking()
            .ToListAsync();

            if (financesI.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(financesI);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<FinanceInstallment>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            var financeI = await context
            .FinanceInstallments
            .Include(x => x.Finance)
            .Include(x => x.Entity)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (financeI == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(financeI);

        }

        [HttpGet]
        [Route("doc/{id:int}")]
        public async Task<ActionResult<List<FinanceInstallment>>> GetByDoc(
            int id,
            [FromServices] DataContext context)
        {
            var financesI = await context
            .FinanceInstallments
            .Include(x => x.Finance)
            .Include(x => x.Entity)
            .AsNoTracking()
            .Where(x => x.FinanceId == id)
            .ToListAsync();

            if (financesI.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(financesI);

        }

        [HttpGet]
        [Route("entity/{id:int}")]
        public async Task<ActionResult<List<FinanceInstallment>>> GetByEntity(
            int id,
            [FromServices] DataContext context)
        {
            var financesI = await context
            .FinanceInstallments
            .Include(x => x.Finance)
            .Include(x => x.Entity)
            .AsNoTracking()
            .Where(x => x.EntityId == id)
            .ToListAsync();

            if (financesI.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(financesI);

        }


        //======Post============

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<FinanceInstallment>> Post(
           [FromBody] FinanceInstallment model,
           [FromServices] DataContext context
       )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                context.FinanceInstallments.Add(model);
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
        public async Task<ActionResult<FinanceInstallment>> Put(
            int id,
            [FromBody] FinanceInstallment model,
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
                context.Entry<FinanceInstallment>(model).State = EntityState.Modified;
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