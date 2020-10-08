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
    [Route("v1/schedules")]
    public class ScheduleController : ControllerBase
    {

        //======Get============
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Schedule>>> Get([FromServices] DataContext context)
        {

            var schedules = await context
            .Schedules
            .Include(x => x.Costumer)
            .Include(x => x.Company)
            .AsNoTracking()
            .ToListAsync();

            if (schedules.Count == 0)
                return NotFound(new { message = "Nenhuma agenda encontrada" });


            return Ok(schedules);

        }

        [HttpGet]
        [Route("company-sc-id/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> GetCompanyScById(
            int id,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Include(x => x.Costumer)
            .Include(x => x.Company)
            .AsNoTracking()
            .Where(x => x.CompanyId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Nenhum agendamento encontrado" });

            return Ok(schedule);

        }

        [HttpGet]
        [Route("user-sc-id/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> GetUserScById(
            int id,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Include(x => x.Costumer)
            .Include(x => x.Company)
            .AsNoTracking()
            .Where(x => x.CostumerId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Nenhum agendamento encontrado" });

            return Ok(schedule);

        }




        [HttpGet]
        [Route("company")]
        [Authorize]
        public async Task<ActionResult<List<Schedule>>> GetByCompany(
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedules = await context
            .Schedules
            .Include(x => x.Costumer)
            .Include(x => x.Company)
            .AsNoTracking()
            .Where(x => x.CompanyId == user)
            .ToListAsync();

            if (schedules.Count == 0)
                return NotFound(new { message = "Nenhum agendamento encontrado" });


            return Ok(schedules);

        }

        [HttpGet]
        [Route("user")]
        [Authorize]
        public async Task<ActionResult<List<Schedule>>> GetByUser(
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedules = await context
            .Schedules
            .Include(x => x.Costumer)
            .Include(x => x.Company)
            .AsNoTracking()
            .Where(x => x.CostumerId == user)
            .ToListAsync();

            if (schedules.Count == 0)
                return NotFound(new { message = "Nenhum agendamento encontrado" });


            return Ok(schedules);

        }


        //======Post============
        [HttpPost]
        [Route("by-user")]
        [Authorize]
        public async Task<ActionResult<Schedule>> PostByUser(
           [FromBody] Schedule model,
           [FromServices] DataContext context
       )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                model.CostumerId = user;
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Schedules.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível realizar o lançamento." });
            }
        }

        [HttpPost]
        [Route("by-company")]
        [Authorize]
        public async Task<ActionResult<Schedule>> PostByCompany(
           [FromBody] Schedule model,
           [FromServices] DataContext context
       )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Add + Salvar DB
                model.CompanyId = user;
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Schedules.Add(model);
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
        [Route("user/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> PutByUser(
            int id,
            [FromBody] Schedule model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Where(x => x.CostumerId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Lançamento não encontrado" });


            try
            {
                // //validar id produto passado
                // if (id != model.Id)
                //     return NotFound(new { message = "Agendamento não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.CostumerId = user;
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Entry<Schedule>(model).State = EntityState.Modified;
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
        [Route("company/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> PutByCompany(
            int id,
            [FromBody] Schedule model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Where(x => x.CompanyId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Lançamento não encontrado" });


            try
            {
                // //validar id produto passado
                // if (id != model.Id)
                //     return NotFound(new { message = "Agendamento não encontrado." });

                //Valida model
                if (!ModelState.IsValid)
                    return BadRequest(model);

                //Update DB
                model.CompanyId = user;
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Entry<Schedule>(model).State = EntityState.Modified;
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
        [Route("user/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> DeleteUser(
            [FromServices] DataContext context,
            int id)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Where(x => x.CostumerId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.Schedules.Remove(schedule);
                await context.SaveChangesAsync();
                return schedule;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }

        //======DELETE============
        [HttpDelete]
        [Route("company/{id:int}")]
        [Authorize]
        public async Task<ActionResult<Schedule>> DeleteCompany(
            [FromServices] DataContext context,
            int id)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            var schedule = await context
            .Schedules
            .Where(x => x.CompanyId == user)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
                return NotFound(new { message = "Lançamento não encontrada" });

            try
            {
                context.Schedules.Remove(schedule);
                await context.SaveChangesAsync();
                return schedule;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o lançamento" });

            }
        }

    }
}