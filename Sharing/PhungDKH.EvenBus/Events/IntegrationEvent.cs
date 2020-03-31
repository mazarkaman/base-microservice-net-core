namespace PhungDKH.EvenBus.Events
{
    using Newtonsoft.Json;
    using System;

    public class IntegrationEvent
    {
        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
