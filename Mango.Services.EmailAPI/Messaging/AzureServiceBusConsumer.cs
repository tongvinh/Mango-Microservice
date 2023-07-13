using System.Text;
using Azure.Messaging.ServiceBus;

namespace Mango.Services.EmailAPI.Messaging;

public class AzureServiceBusConsumer:IAzureServiceBusConsumer
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

    public async Task Start()
    {
        _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
        _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
    }
    
    public async Task Stop()
    {
        await _emailCartProcessor.StopProcessingAsync();
        await _emailCartProcessor.DisposeAsync();
    }

    private Task ErrorHandler(ProcessErrorEventArgs arg)
    {
        Console.WriteLine(arg.Exception.ToString());
        return Task.CompletedTask;
    }

    private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs arg)
    {
        // This is where you will receive message
        var message = arg.Message;
        var body = Encoding.UTF8.GetString(message.Body);
        try
        {
            //TODO - Try to log email
            await arg.CompleteMessageAsync(arg.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}