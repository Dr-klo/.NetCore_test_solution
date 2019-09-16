using Microsoft.Extensions.Options;

namespace Auth0_test_solution.Models
{
    public class UpdateOrderProduct
    {
        public struct Errors
        {
            public const string InvalidRequest = "Request contains incorrect article or count.";
        }
        public int article { get; set; }
        public int count { get; set; }

        public bool Validate()
        {
            return this.article > 0 && this.count != 0;
        }
    }
}