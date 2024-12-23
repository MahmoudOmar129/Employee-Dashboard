namespace Demo.BL.Helper
{
    public class ApiResponse<Type>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Type Data { get; set; }
    }
}
