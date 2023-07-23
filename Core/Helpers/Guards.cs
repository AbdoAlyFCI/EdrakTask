using EdrakTask.Core.Exceptions;

namespace EdrakTask.Core.Helpers
{
    public static class Guards
    {
        public static void ArgumentNotNull(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
        public static void EntityNotFound(object value)
        {
            if (value == null)
            {
                throw new EntityNotFoundException(nameof(value));
            }
        }
        public static void EntityNotFound(bool value)
        {
            if (value == false)
            {
                throw new EntityNotFoundException(nameof(value));
            }
        }

        public static void SomeEntityNotFound(int found, int expected)
        {
            if(found != expected)
            {
                throw new EntityNotFoundException("Some Entity Not Found");

            }
        }

        public static void PrdouctAmountNotEnough(int amount,int take)
        {
            if(amount-take <0)
            {
                throw new InvalidOperationException();
            }
        }

        public static void GuidNotEmpty(Guid guid)
        {
            if(guid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(guid));
            }
        }
    }
}
