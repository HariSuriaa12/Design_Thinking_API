using System.Security.AccessControl;
using backend_API.Database;
using backend_API.Model;
using backend_API.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend_API.Controller.UsersController
{
    [Route("api/[controller]/")]
    public class userRating : ControllerBase
    {
        private readonly Connection conn;
        public userRating(Connection conn)
        {
            this.conn = conn;
        }

        [HttpPut("UpdateAverageRating/{id}")]
        public IActionResult UpdateUserRating(long id, [FromBody] UserRatingDTO userRatingDTO)
        {
            var users = conn.Users.Find(id);

            try
            {
                if (users != null)
                {
                    decimal? fetched_current_rating_from_DB = users.average_rating == null ? 0 : users.average_rating;
                    int? fetched_current_total_user_rating_from_DB = users.total_user_rated == null ? 0 : users.total_user_rated;

                    int user_give_rating = userRatingDTO.rating;
                    decimal? current_raw_rating = (fetched_current_rating_from_DB / 10) * 5;
                    decimal? rating_sum = current_raw_rating * fetched_current_total_user_rating_from_DB;
                    decimal? new_rating_sum = rating_sum + user_give_rating;
                    decimal? new_average_rating = new_rating_sum / (fetched_current_total_user_rating_from_DB + 1);
                    decimal? display_upscaled_rating = (new_average_rating / 5) * 10;

                    users.average_rating = display_upscaled_rating;
                    users.total_user_rated = fetched_current_total_user_rating_from_DB + 1;

                    conn.SaveChanges();

                    MainModel main = new MainModel
                    {
                        success = true,
                        message = "Successfully updated user rating!",
                        data = users
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"user ID {id} not found!",
                        data = new Users()
                    };

                    return NotFound(main);
                }
            }
            catch (Exception ex)
            {
                MainModel main = new MainModel
                {
                    success = false,
                    message = ex.Message,
                    data = new Users()
                };

                return BadRequest(main);
            }
        }
    }
}
