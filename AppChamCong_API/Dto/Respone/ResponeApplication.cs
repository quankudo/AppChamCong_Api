namespace AppChamCong_API.Dto.Respone
{
    public class ResponeApplication<T>
    {
        public ResponeApplication()
        {
            
        }
        public ResponeApplication(string mess, T datas, Boolean succ)
        {
            message = mess;
            data = datas;
            success = succ;
        }
        public string message { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public T data { get; set; }
        public Boolean success { get; set; }
    }
}
