using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;

public class SqsServices : BackgroundService
{
    private readonly IAmazonSQS _sqs;

    public SqsServices(IAmazonSQS sqs)
    {
        _sqs = sqs;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var url = await _sqs.GetQueueUrlAsync("comp1_queue");
        var request = new ReceiveMessageRequest
        {
            QueueUrl = url.QueueUrl,
            MessageAttributeNames = new List<string> { "All" },
            AttributeNames = new List<string> { "All" },
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var sqsResponse = await _sqs.ReceiveMessageAsync(request, stoppingToken);
            if (!sqsResponse.HttpStatusCode.Equals(HttpStatusCode.OK)){
                Console.WriteLine(sqsResponse.HttpStatusCode);
                continue;
            }

            foreach (var msg in sqsResponse.Messages)
                await _sqs.DeleteMessageAsync(url.QueueUrl, msg.ReceiptHandle);

            sqsResponse.Messages.Select(x => x.Body).All(x => { Console.WriteLine(x); return true; });
        }
    }
}