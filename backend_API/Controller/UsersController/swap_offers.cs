using System.Reflection;
using System.Text;
using backend_API.Database;
using backend_API.Model.DTO;
using backend_API.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace backend_API.Controller.UsersController
{
    [Route("api/[controller]/")]
    public class swap_offers : ControllerBase
    {
        private readonly Connection conn;
        public swap_offers(Connection conn)
        {
            this.conn = conn;
        }

        [HttpGet("GetOffer/{id?}")]
        public IActionResult Get(long? id)
        {
            if (id == null)
            {
                var retrieved_Swap_offers = conn.swap_offers
                        .Join(conn.Users,
                            s => s.user_id,
                            u => u.id,
                            (s, u) => new swap_offerDTO
                            {
                                id = s.id,
                                item_name = s.item_name,
                                item_description = s.item_description,
                                user_id = s.user_id,
                                item_image = s.item_image,
                                meetup_location = s.meetup_location,
                                in_return_item_request = s.in_return_item_request,
                                swap_period = s.swap_period,
                                request_expiry_date = s.request_expiry_date,
                                additional_info = s.additional_info,
                                accepted_user_id = s.accepted_user_id,
                                created_at = s.created_at,
                                user = u // ✅ assign user manually
                            })
                        .ToList();

                //var retrieved_Swap_offers = conn.swap_offers.Join(conn.Users, swap_offer => swap_offer.user_id, Users => Users.id, 
                //    (swap_offer, Users) => new
                //    {
                //        swap_offer = swap_offer,
                //        Users = Users
                //    });

                MainModel main = new MainModel
                {
                    success = true,
                    message = $"Success",
                    data = retrieved_Swap_offers
                };

                return Ok(main);
            }
            else
            {
                var retrieved_Swap_offers = conn.swap_offers
                        .Where(s => s.id == id)
                        .Join(conn.Users,
                            s => s.user_id,
                            u => u.id,
                            (s, u) => new swap_offerDTO
                            {
                                id = s.id,
                                item_name = s.item_name,
                                item_description = s.item_description,
                                user_id = s.user_id,
                                item_image = s.item_image,
                                meetup_location = s.meetup_location,
                                in_return_item_request = s.in_return_item_request,
                                swap_period = s.swap_period,
                                request_expiry_date = s.request_expiry_date,
                                additional_info = s.additional_info,
                                accepted_user_id = s.accepted_user_id,
                                created_at = s.created_at,
                                user = u // ✅ assign user manually
                            })
                        .ToList();

                //var retrieved_Swap_offers = conn.swap_offers.ToList().Where(x => x.id == id)
                //    .Join(conn.Users, swap_offer => swap_offer.user_id, Users => Users.id,
                //    (swap_offer, Users) => new
                //    {
                //        swap_offer = swap_offer,
                //        Users = Users
                //    });

                if (retrieved_Swap_offers != null)
                {
                    MainModel main = new MainModel
                    {
                        success = true,
                        message = $"Success",
                        data = retrieved_Swap_offers
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"Offer {id} not found",
                        data = new swap_offer()
                    };

                    return NotFound(main);
                }
            }
        }

        [HttpGet("GetOfferFromAcceptedUser/{id?}")]
        public IActionResult GetBasedOnAcceptedUserId(long? id)
        {
            var retrieved_Swap_offers = conn.swap_offers
                        .Where(s => s.accepted_user_id == id)
                        .Join(conn.Users,
                            s => s.user_id,
                            u => u.id,
                            (s, u) => new swap_offerDTO
                            {
                                id = s.id,
                                item_name = s.item_name,
                                item_description = s.item_description,
                                user_id = s.user_id,
                                item_image = s.item_image,
                                meetup_location = s.meetup_location,
                                in_return_item_request = s.in_return_item_request,
                                swap_period = s.swap_period,
                                request_expiry_date = s.request_expiry_date,
                                additional_info = s.additional_info,
                                accepted_user_id = s.accepted_user_id,
                                created_at = s.created_at,
                                user = u // ✅ assign user manually
                            })
                        .ToList();

            if (retrieved_Swap_offers != null)
            {
                MainModel main = new MainModel
                {
                    success = true,
                    message = $"Success",
                    data = retrieved_Swap_offers
                };

                return Ok(main);
            }
            else
            {
                MainModel main = new MainModel
                {
                    success = false,
                    message = $"Offer {id} not found",
                    data = new swap_offer()
                };

                return NotFound(main);
            }
        }

        [HttpGet("GetOfferFromUserId/{id?}")]
        public IActionResult GetBasedOnUserId(long? id)
        {
            var retrieved_Swap_offers = conn.swap_offers
                        .Where(s => s.user_id == id)
                        .Join(conn.Users,
                            s => s.user_id,
                            u => u.id,
                            (s, u) => new swap_offerDTO
                            {
                                id = s.id,
                                item_name = s.item_name,
                                item_description = s.item_description,
                                user_id = s.user_id,
                                item_image = s.item_image,
                                meetup_location = s.meetup_location,
                                in_return_item_request = s.in_return_item_request,
                                swap_period = s.swap_period,
                                request_expiry_date = s.request_expiry_date,
                                additional_info = s.additional_info,
                                accepted_user_id = s.accepted_user_id,
                                created_at = s.created_at,
                                user = u // ✅ assign user manually
                            })
                        .ToList();

            if (retrieved_Swap_offers != null)
            {
                MainModel main = new MainModel
                {
                    success = true,
                    message = $"Success",
                    data = retrieved_Swap_offers
                };

                return Ok(main);
            }
            else
            {
                MainModel main = new MainModel
                {
                    success = false,
                    message = $"Offer {id} not found",
                    data = new swap_offer()
                };

                return NotFound(main);
            }
        }

        [HttpPost("PostOffer")]
        public IActionResult Post([FromBody] swap_offerDTO swap_offer_DTO)
        {
            swap_offer swap_offer = new swap_offer()
            {
                id = swap_offer_DTO.id,
                item_name = swap_offer_DTO.item_name,
                item_description = swap_offer_DTO.item_description,
                user_id = swap_offer_DTO.user_id,
                item_image = swap_offer_DTO.item_image,
                meetup_location = swap_offer_DTO.meetup_location,
                in_return_item_request = swap_offer_DTO.in_return_item_request,
                swap_period = swap_offer_DTO.swap_period,
                request_expiry_date = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss"),
                additional_info = swap_offer_DTO.additional_info,
                accepted_user_id = swap_offer_DTO.accepted_user_id,
                created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            StringBuilder s = new StringBuilder();
            PropertyInfo[] propertyInfos = swap_offer.GetType().GetProperties();

            s.AppendLine($"Insert into swap_offers ({String.Join(',', propertyInfos.Where(x => (x.Name != "id" && x.Name != "user")).Select(x => x.Name).ToArray())})");
            s.AppendLine("VALUES");
            s.AppendLine("(");

            List<NpgsqlParameter> DBParamList = new List<NpgsqlParameter>();
            int i = 1;

            try
            {

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name == "id" || propertyInfo.Name == "user")
                        continue;

                    if (i == propertyInfos.Count() - 2)
                        s.AppendLine($"@{i}");
                    else
                        s.AppendLine($"@{i},");

                    var value = propertyInfo.GetValue(swap_offer) ?? DBNull.Value;
                    DBParamList.Add(new NpgsqlParameter($"@{i}", value));

                    i++;
                }

                s.AppendLine(")");
                string query = s.ToString();

                conn.SQL_Command_Execute(query, DBParamList);

                MainModel main = new MainModel
                {
                    success = true,
                    message = $"Success",
                    data = swap_offer
                };

                return Ok(main);
            }
            catch (Exception ex)
            {
                MainModel main = new MainModel
                {
                    success = false,
                    message = $"Failed create swap offer, " + ex.Message,
                    data = new swap_offer()
                };

                return NotFound(main);
            }
        }

        [HttpPut("UpdateAcceptedUser/{id}")]
        public IActionResult UpdateAcceptedUser(long id, [FromBody] swap_offerDTO swap_offer_DTO)
        {
            //var swap_offer_model = conn.swap_offers.Where(x => x.id == id).ToList();
            var swap_offer_model = conn.swap_offers.Find(id);

            try
            {
                if (swap_offer_model != null)
                {
                    swap_offer_model.accepted_user_id = swap_offer_DTO.accepted_user_id;

                    conn.SaveChanges();

                    MainModel main = new MainModel
                    {
                        success = true,
                        message = $"Success",
                        data = swap_offer_model
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"Swap offer ID {id} not found!",
                        data = new swap_offer()
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
                    data = new swap_offer()
                };

                return BadRequest(main);
            }
        }

        [HttpPut("UpdateConfirmSwap/{id}")]
        public IActionResult UpdateConfirmSwap(long id, [FromBody] swap_offerDTO swap_offer_DTO)
        {
            //var swap_offer_model = conn.swap_offers.Where(x => x.id == id).ToList();
            var swap_offer_model = conn.swap_offers.Find(id);

            try
            {
                if (swap_offer_model != null)
                {
                    swap_offer_model.isConfirmed = swap_offer_DTO.isConfirmed;

                    conn.SaveChanges();

                    MainModel main = new MainModel
                    {
                        success = true,
                        message = $"Success",
                        data = swap_offer_model
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"Swap offer ID {id} not found!",
                        data = new swap_offer()
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
                    data = new swap_offer()
                };

                return BadRequest(main);
            }
        }

        [HttpDelete("RemoveSwapOffer/{id}")]
        public IActionResult DeleteSwapOffer(long id)
        {
            try
            {
                var swap_offer_model = conn.swap_offers.Find(id);

                if (swap_offer_model != null)
                {
                    conn.Remove(swap_offer_model);
                    conn.SaveChanges();

                    MainModel main = new MainModel
                    {
                        success = true,
                        message = $"Successfully removed swap offer",
                        data = swap_offer_model
                    };

                    return Ok(main);
                }
                else
                {
                    MainModel main = new MainModel
                    {
                        success = false,
                        message = $"Swap offer ID {id} not found!",
                        data = new swap_offer()
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
                    data = new swap_offer()
                };

                return BadRequest(main);
            }
        }
    }
}