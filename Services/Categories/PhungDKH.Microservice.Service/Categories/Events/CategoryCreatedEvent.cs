namespace PhungDKH.Microservice.Service.Categories.Events
{
    using PhungDKH.EvenBus.Events;

    public class CategoryCreatedEvent : IntegrationEvent
    {
        public string Name { get; set; }

        public CategoryCreatedEvent(string name)
        {
            this.Name = name;
            this.RoutingKey = "crud_category_created";
        }
    }
}
