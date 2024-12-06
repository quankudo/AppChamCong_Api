namespace AppChamCong_API.Dto.Respone
{
    public class UserRespone
    {
        public UserRespone()
        {

        }
        public UserRespone(int id, string hoten, string email, string sdt)
        {

        }
        public int Iduser { get; set; }

        public string? Hovaten { get; set; }

        public string? Email { get; set; }

        public string? Sdt { get; set; }

        public bool? Trangthai { get; set; }
    }
}
