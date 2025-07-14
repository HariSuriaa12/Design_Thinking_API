using backend_API.Database;
using backend_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend_API.Controller.UsersController
{
    [Route("api/[controller]/{username?}")] //? Here allows an optional API parameter in URL
    [ApiController]
    public class GetUsers : ControllerBase
    {
        private readonly Connection conn;

        public GetUsers(Connection conn)
        {
            this.conn = conn;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get([FromRoute] string? username)
        {
            if (username == null)
            {
                var allUsers = conn.Users.ToList();

                MainModel main = new MainModel
                {
                    success = true,
                    message = $"Success",
                    data = allUsers
                };

                return Ok(main);
            }
            else
            {
                var User = conn.Users.ToList().Where(x => x.username == username).FirstOrDefault();

                if (User != null)
                {
                    MainModel main = new MainModel
                    {
                        success = true,
                        message = $"Success",
                        data = User
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"User {username} not found!",
                        data = new Users()
                    };

                    return NotFound(main);
                }
            }
        }
    }
}