namespace Devpro.Twohire.Client.Infrastructure.RestApi.Dto
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; }
        public object Error { get; set; }
        public T Data { get; set; }
    }
}
