namespace EmailProvider.Enums
{
    public class EmailDirectionProvider
    {
        private enum EmailDirections
        {
            EmailDirectionIn = 1,
            EmailDirectionOut = 2,
            EmailDirectionInner = 3,
        }

        public static int GetEmailDirectionIn()
        {
            return (int)EmailDirections.EmailDirectionIn;
        }

        public static int GetEmailDirectionOut()
        {
            return (int)EmailDirections.EmailDirectionOut;
        }

        public static int GetEmailDirectionInner()
        {
            return (int)EmailDirections.EmailDirectionInner;
        }
    }
}
