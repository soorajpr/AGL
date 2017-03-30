namespace GroupedPetsList.Shared
{
    public class CustomException
    {
        public CustomException()
        {
            
        }

        public CustomException(string message) : this()
        {
            Message = message;
        }

        public CustomException(System.Exception ex) : this(ex.Message)
        {
            StackTrace = ex.StackTrace;
        }

        public override string ToString()
        {
            return Message + StackTrace;
        }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string RequestUrl { get; set; }

        public string MethodName { get; set; }
    }
}
