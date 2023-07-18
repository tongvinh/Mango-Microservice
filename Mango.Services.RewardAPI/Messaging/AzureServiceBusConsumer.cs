using System.Text;
using Azure.Messaging.ServiceBus;
using Mango.Services.RewardAPI.Message;
using Mango.Services.RewardAPI.Services;
using Newtonsoft.Json;

namespace Mango.Services.RewardAPI.Messaging;

public class AzureServiceBusConsumer:IAzureServiceBusConsumer
{
    private readonly string? serviceBusConnectioString;
    private readonly string orderCreatedTopic;
    private readonly string orderCreatedRewardSubscription;    
    private readonly IConfiguration _configuration;
    private readonly RewardService _rewardService;

    private ServiceBusProcessor _rewardProcessor;

    
    public AzureServiceBusConsumer(IConfiguration configuration,RewardService rewardService)
    {
        _configuration = configuration;
        _rewardService = rewardService;
        serviceBusConnectioString = _configuration.GetValue<string>("ServiceBusConnectionString");
        orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
        orderCreatedRewardSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

        var client = new ServiceBusClient(serviceBusConnectioString);
        _rewardProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedRewardSubscription);
    }

    public async Task Start()
    {
        _rewardProcessor.ProcessMessageAsync += OnOrderRewardsRequestReceived;
        _rewardProcessor.ProcessErrorAsync += ErrorHandler;
        await _rewardProcessor.StartProcessingAsync();
    }

    public async Task Stop()
    {
        await _rewardProcessor.StopProcessingAsync();
        await _rewardProcessor.DisposeAsync();
    }

    private Task ErrorHandler(ProcessErrorEventArgs arg)
    {
        Console.WriteLine(arg.Exception.ToString());
        return Task.CompletedTask;
    }

    private async Task OnOrderRewardsRequestReceived(ProcessMessageEventArgs arg)
    {
        // This is where you will receive message
        var message = arg.Message;
        var body = Encoding.UTF8.GetString(message.Body);

        RewardsMessage objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
        try
        {
            //TODO - Try to log email
            await _rewardService.UpdateRewards(objMessage);
            await arg.CompleteMessageAsync(arg.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}