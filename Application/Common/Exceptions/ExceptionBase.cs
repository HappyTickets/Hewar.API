namespace Application.Common.Exceptions
{
    public abstract class ExceptionBase: Exception
    {
        public abstract int Status { get; }

        protected ExceptionBase(string message): base(message)
        {
        }
    }
}
