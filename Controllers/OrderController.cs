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
    [Route("v1/orders")]
    public class OrderController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Order>>> Get(
            [FromServices] DataContext context)
        {

            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sales = await context
            .Orders
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
        public async Task<ActionResult<Order>> GetById(
            int id,
            [FromServices] DataContext context)
        {

            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sale = await context
            .Orders
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null)
                return NotFound(new { message = "Nenhum lançamento encontrado" });

            return Ok(sale);

        }


        [HttpGet]
        [Route("by-company")]
        [Authorize]
        public async Task<ActionResult<List<Order>>> GetByCompany(

            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sales = await context
            .Orders
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .Where(x => x.CpnyUid == user)
            .ToListAsync();

            if (sales.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(sales);

        }

        [HttpGet]
        [Route("by-costumer")]
        [Authorize]
        public async Task<ActionResult<List<Order>>> GetByCostumer(

            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sales = await context
            .Orders
            .Include(x => x.Costumer)
            .Include(x => x.Payment)
            .AsNoTracking()
            .Where(x => x.CostumerUid == user)
            .ToListAsync();

            if (sales.Count == 0)
                return NotFound(new { message = "Nenhum lançamento encontrado" });


            return Ok(sales);

        }


        //======Post============
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Order>> Post(
           [FromBody] Order model,
           [FromServices] DataContext context
       )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                model.CpnyUid = user; //sempre parte do usuario a criação da Venda
                context.Orders.Add(model);
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
        [Route("company-order-id")]
        [Authorize]
        public async Task<ActionResult<Order>> PutCompanyOrderId(
            [FromBody] Order model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var checkCompany = await context
                .Orders
                .AsNoTracking()
                .Where(x => x.CpnyUid == user)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (checkCompany == null)
                return NotFound(new { message = "Nenhuma venda encontrada" });


            try
            {
                // //validar id produto passado
                // if (id != model.Id)
                //     return NotFound(new { message = "Lançamento não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.CpnyUid = user;
                context.Entry<Order>(model).State = EntityState.Modified;
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


        [HttpPut]
        [Route("costumer-order-id")]
        [Authorize]
        public async Task<ActionResult<Order>> PutCostumerOrderId(
            [FromBody] Order model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var checkCompany = await context
                .Orders
                .AsNoTracking()
                .Where(x => x.CostumerUid == user)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (checkCompany == null)
                return NotFound(new { message = "Nenhuma venda encontrada" });


            try
            {
                // //validar id produto passado
                // if (id != model.Id)
                //     return NotFound(new { message = "Lançamento não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.CpnyUid = user;
                context.Entry<Order>(model).State = EntityState.Modified;
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
        [Route("company-del-order/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Order>> DeleteCompanyOrder(
            [FromServices] DataContext context,
            int id)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sale = await context
            .Orders
            .Where(x => x.CpnyUid == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null)
                return NotFound(new { message = "Lançamento não encontrado" });

            try
            {
                context.Orders.Remove(sale);
                await context.SaveChangesAsync();
                return sale;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }

        [HttpDelete]
        [Route("costumer-del-order/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Order>> DeleteCostumerOrder(
            [FromServices] DataContext context,
            int id)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var sale = await context
            .Orders
            .Where(x => x.CostumerUid == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null)
                return NotFound(new { message = "Lançamento não encontrado" });

            try
            {
                context.Orders.Remove(sale);
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