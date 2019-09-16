using System;

namespace Auth0_test.Appartments
{
    public class ApplicationDomainException: Exception
    {
        public string[] Messages { get; private set; }
        public ApplicationDomainException(string message) : base(message)
        {
            
        }

        public ApplicationDomainException(string message, string[] data): base(message)
        {
            this.Messages = data;
        }
    }
}