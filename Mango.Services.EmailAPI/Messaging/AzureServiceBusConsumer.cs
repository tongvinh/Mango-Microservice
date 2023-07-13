using Azure.Messaging.ServiceBus;

namespace Mango.Services.EmailAPI.Messaging;

public class AzureServiceBusConsumer
{
    private readonly string? serviceBusConnectioString;
    private readonly string emailCartQueue;
    private readonly IConfiguration _configuration;

    private ServiceBusProcessor _emailCartProcessor;
    
    public AzureServiceBusConsumer(IConfiguration configuration)
    {
        _configuration = configuration;
        serviceBusConnectioString = _configuration.GetValue<string>("ServiceBusConnectionString");
        emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

        var client = new ServiceBusClient(serviceBusConnectioString);
        _emailCartProcessor = client.CreateProcessor(emailCartQueue);
    }
}