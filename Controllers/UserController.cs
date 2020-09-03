using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;

namespace Lojax.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {

        //=======GET=======
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {

            var users = await context.Users.AsNoTracking().ToListAsync();

            if (users.Count == 0)
                return NotFound(new { message = "Nenhum usuário encontrado." });

            return Ok(users);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetByID(
            int id,
            [FromServices] DataContext context)
        {

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });


            return Ok(user);

        }

        //=======POST=======
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> Post(
            [FromBody] User model,
            [FromServices] DataContext context)
        {
            try
            {
                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Add Categoria
                context.Users.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível cadastrar um usuário" });
            }
        }


        //=======PUT=======
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> Put(
            int id,
            [FromBody] User model,
            [FromServices] DataContext context)
        {
            try
            {
                //valida ID da categoria
                if (id != model.Id)
                    return NotFound(new { message = "Usuário não encontrado" });

                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Atualizar Categoria
                context.Entry<User>(model).State = EntityState.Modified;

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
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }
        }

    }
}