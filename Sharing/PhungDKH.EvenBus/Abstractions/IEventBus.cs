using PhungDKH.EvenBus.Events;

namespace PhungDKH.EvenBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
    }
}
