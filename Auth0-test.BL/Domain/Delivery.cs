namespace Auth0_test_solution.BL.Domain
{
    public class Delivery
    {
        public string Name { get; }

        public Delivery(string name)
        {
            this.Name = name;
        }
        public void SendOrderToDeliveryService(Order order)
        {
        }
    }
}