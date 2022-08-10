using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homework_prac4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }   

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<UserModel>>> UpdateUser(UserModel user, int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Age = user.Age;
            dbUser.Gender = user.Gender;

            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserModel>>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(dbUser);

            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        private async Task<List<UserModel>> GetDbUsers()
        {
            return await _context.Users.ToListAsync();
        }

    }
}

