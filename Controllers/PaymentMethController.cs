using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lojax.Controllers
{
    [Route("v1/payment-methods")]
    public class PaymentMethController : ControllerBase
    {
        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<PaymentMethod>>> Get([FromServices] DataContext context)
        {

            var payments = await context.PaymentMethods.AsNoTracking().ToListAsync();

            if (payments.Count == 0)
                return NotFound(new { message = "Nenhuma forma de pagamento encontrada." });

            return Ok(payments);
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<PaymentMethod>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var payment = await context.PaymentMethods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null)
                return NotFound(new { message = "Forma de Pagamento não encontrada" });


            return Ok(payment);

        }

        //=======POST=======
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<PaymentMethod>> Post(
            [FromBody] PaymentMethod model,
            [FromServices] DataContext context)
        {
            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add Categoria
                context.PaymentMethods.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível criar a forma de pagamento" });
            }
        }


        //=======PUT=======
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<PaymentMethod>> Put(
            int id,
            [FromBody] PaymentMethod model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Forma de pagamento não encontrada" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Atualizar Categoria
                context.Entry<PaymentMethod>(model).State = EntityState.Modified;

                //Salvar no banco 
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateConcurrencyException) //Verifica se existe um dado sendo atualizado ao mesmo tempo.
            {

                return BadRequest(new { message = "Esse registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a forma de pagamento" });
            }
        }
    }
}