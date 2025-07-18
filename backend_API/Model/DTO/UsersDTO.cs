namespace backend_API.Model.DTO
{
    public class UsersDTO
    {
        public long id { get; set; }
        public string? full_name { get; set; }
        public string? nick_name { get; set; }
        public string? ic { get; set; }
        public string? email { get; set; }
        public string? phone_no { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? post_code { get; set; }
        public string? unit_no { get; set; }
        public string? street_address_1 { get; set; }
        public string? street_address_2 { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? user_image { get; set; }
        public string created_at { get; set; }
        public decimal? average_rating { get; set; }
        public int? total_user_rated { get; set; }
    }
}
