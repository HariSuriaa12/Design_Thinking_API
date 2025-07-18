using System.Reflection;
using System.Text;
using backend_API.Database;
using backend_API.Model;
using backend_API.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend_API.Controller.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertUsers : ControllerBase
    {
        private readonly Connection conn;

        public InsertUsers(Connection conn)
        {
            this.conn = conn;
        }

        [HttpPost]
        public IActionResult AddUsers([FromBody] UsersDTO usersDTO)
        {
            Users insertUsers = new Users()
            {
                id = usersDTO.id,
                full_name = usersDTO.full_name,
                nick_name = usersDTO.nick_name,
                ic = usersDTO.ic,
                email = usersDTO.email,
                phone_no = usersDTO.phone_no,
                country = usersDTO.country,
                state = usersDTO.state,
                post_code = usersDTO.post_code,
                unit_no = usersDTO.unit_no,
                street_address_1 = usersDTO.street_address_1,
                street_address_2 = usersDTO.street_address_2,
                username = usersDTO.username,
                password = usersDTO.password,
                user_image = usersDTO.user_image,
                created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            //StringBuilder s = new StringBuilder();
            //PropertyInfo[] propertyInfos = insertUsers.GetType().GetProperties();

            //s.AppendLine($"Insert into \"Users\" ({String.Join(',', propertyInfos.Where(x => x.Name != "id").Select(x => x.Name).ToArray())})");
            //s.AppendLine("VALUES");
            //s.AppendLine("(");

            //List<NpgsqlParameter> DBParamList = new List<NpgsqlParameter>();
            //int i = 1;

            try
            {
                conn.Users.Add(insertUsers);
                conn.SaveChanges();

                //foreach (PropertyInfo propertyInfo in propertyInfos)
                //{
                //    if (propertyInfo.Name == "id")
                //        continue;

                //    if (i == propertyInfos.Count() - 1)
                //        s.AppendLine($"@{i}");
                //    else
                //        s.AppendLine($"@{i},");

                //    var value = propertyInfo.GetValue(insertUsers) ?? DBNull.Value; ;
                //    DBParamList.Add(new NpgsqlParameter($"@{i}", value));

                //    i++;
                //}

                //s.AppendLine(")");

                //string query = s.ToString();

                //conn.SQL_Command_Execute(query, DBParamList);

                MainModel main = new MainModel
                {
                    success = true,
                    message = $"SUCCESS INSERT USER IC : {usersDTO.ic}",
                    data = new Users()
                };

                return Ok(main);
            }
            catch (Exception ex)
            {
                string errMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                MainModel main = new MainModel
                {
                    success = false,
                    message = errMsg,
                    data = new Users()
                };

                return BadRequest(main);
            }
        }
    }
}