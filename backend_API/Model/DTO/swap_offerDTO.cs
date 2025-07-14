namespace backend_API.Model.DTO
{
    public class swap_offerDTO
    {
        public long id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public long user_id { get; set; }
        public string item_image { get; set; }
        public string meetup_location { get; set; }
        public string in_return_item_request { get; set; }
        public string swap_period { get; set; }
        public string request_expiry_date { get; set; }
        public string additional_info { get; set; }
        public long? accepted_user_id { get; set; }
        public string created_at { get; set; }
        public int? isConfirmed { get; set; }
        public Users user { get; set; }
    }
}
