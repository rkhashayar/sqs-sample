using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Contracts.Messages;

namespace SenderWebApi.Services;

public interface ISqsPublisher
{
    Task PublishAsync<T>(string queueName, T message) where T : IMessage;
}

public class SqsPublisher : ISqsPublisher
{
    private readonly IAmazonSQS _amazonSQS;

    public SqsPublisher(IAmazonSQS amazonSQS)
    {
        _amazonSQS = amazonSQS;
    }

    public async Task PublishAsync<T>(string queueName, T message) where T : IMessage
    {
        var queueUrl = await _amazonSQS.GetQueueUrlAsync(queueName);

        var request = new SendMessageRequest{
            QueueUrl = queueUrl.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    nameof(message.MessageTypeName),
                    new MessageAttributeValue
                    {
                        StringValue = message.MessageTypeName,
                        DataType = "String"
                    }
                },
                {
                    "timestamp",
                    new MessageAttributeValue
                    {
                        StringValue = DateTime.UtcNow.ToString(),
                        DataType = "String"
                    }
                },
                {
                    "version",
                    new MessageAttributeValue
                    {
                        StringValue = "v1",
                        DataType = "String"
                    }
                }
            }
        };

        await _amazonSQS.SendMessageAsync(request);
    }
}
