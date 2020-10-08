using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lojax.Data;
using Lojax.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Lojax.Services;

namespace Lojax.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {

        //=======GET=======
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {

            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;    
            var users = await context.Users.AsNoTracking().ToListAsync();

            if (users.Count == 0)
                return NotFound(new { message = "Nenhum usuário encontrado." });

            return Ok(users);
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
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
        [AllowAnonymous]
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
                model.Status = 1;
                model.Role = "Default";
                model.DateCreated = DateTime.Now.ToLocalTime();
                model.DateUpdate = DateTime.Now.ToLocalTime();
                context.Users.Add(model);

                //Salvar no banco e gerar ID
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest(new
                {
                    message =
                "Não foi possível cadastrar um usuário "

                });

            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            var user = await context.Users
                .AsNoTracking()
                //.Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválido" });

            //modelar para trabalhar com o firebase e não esse token aqui
            //var token = TokenService.GenerateToken(user);


            //Depois que fizer o login e der certo, esta sendo retornado um novo objeto com os dados do usuario e o token gerado
            //Se retornar apenas o user, vem inclusive a senha, mas pode ser ocultado.
            //model.Password = "";
            return Ok(model);
        }


        //=======PUT=======
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<User>> Put(
            [FromBody] User model,
            [FromServices] DataContext context)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;


            var checkUser = await context
                .Users
                .AsNoTracking()
                .Where(x => x.Uid == user)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (checkUser == null)
                return NotFound(new { message = "Nenhuma usuario encontrado" });



            try
            {

                // //valida ID da categoria
                // if (user != model.Uid)
                //     return NotFound(new { message = "Usuario que esta tentando alterar é diferente do token" });


                //Valida o model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                //Atualizar usuario
                model.Uid = user;
                model.DateUpdate = DateTime.Now.ToLocalTime();
                model.DateCreated = checkUser.DateCreated;
                model.Status = checkUser.Status;
                model.Role = checkUser.Role;
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