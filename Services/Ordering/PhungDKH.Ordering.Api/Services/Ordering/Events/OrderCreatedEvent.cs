namespace PhungDKH.Ordering.Service.Categories.Events
{
    using PhungDKH.EvenBus.Events;

    public class OrderCreatedEvent : IntegrationEvent
    {
        public string Name { get; set; }

        public OrderCreatedEvent(string name)
        {
            this.Name = name;
            this.RoutingKey = "ordering_created";
        }
    }
}
