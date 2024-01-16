using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApplication1.Context;
using WebApplication1.Controllers;
using System.Text.Json;
using Azure.Core;

namespace WebApplication1.Source.Svc
{
    public class BloodSearchService : IBloodSearchService
    {
        private readonly BloodContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService emailService;
        private readonly DonationService donationService;
        public BloodSearchService(BloodContext context)
        {
            _context = context;
            emailService = new EmailService("t85846658@gmail.com", "iokz tpco nhlc ufzq");
            donationService = new DonationService(context);
        }
        public void NightlySearchForBloodRequests(BloodRequest request)
        {
            try
            {
                // Consume blood requests from the queue
                var bloodRequestsFromQueue = ConsumeBloodRequestsFromQueueAsync();
                donationService.RequestBlood(request);
                var toEmail = "t85846658@gmail.com";
                var subject = "Blood Donation Notification";
                var body = "Dear recipient, we tried your request again.";

                // Send email asynchronously
                Task.Run(async () => await emailService.SendEmailAsync(toEmail, subject, body))
                    .GetAwaiter()
                    .GetResult();

            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error in NightlySearchForBloodRequests: {ex.Message}");
            }

        }

 
        public async Task<List<BloodRequest>> ConsumeBloodRequestsFromQueueAsync()
        {
            var serviceBusConnectionString = "Endpoint=sb://final19070006002.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Cttg1DfSeL1rjafuZ7wATWQhhrH8xY0cS+ASbIiyFQM=";
            var queueName = "bloodrequestqueue";

            if (string.IsNullOrEmpty(serviceBusConnectionString) || string.IsNullOrEmpty(queueName))
            {
                // Log or handle the null connection string or queue name
                Console.WriteLine("Service Bus connection string or queue name is null or empty.");
                return new List<BloodRequest>();
            }

            try
            {
                // Create a Service Bus client
                await using (ServiceBusClient client = new ServiceBusClient(serviceBusConnectionString))
                {
                    // Create a receiver for the queue
                    ServiceBusReceiver receiver = client.CreateReceiver(queueName);

                    var bloodRequestsFromQueue = new List<BloodRequest>();

                    // Receive messages in a loop
                    await foreach (ServiceBusReceivedMessage message in receiver.ReceiveMessagesAsync())
                    {
                        try
                        {
                            // Deserialize the message content to BloodRequest
                            var requestJson = message.Body.ToString();
                            var bloodRequest = JsonSerializer.Deserialize<BloodRequest>(requestJson);

                            // Add the blood request to the list
                            bloodRequestsFromQueue.Add(bloodRequest);

                            // Complete the message to remove it from the queue
                            await receiver.CompleteMessageAsync(message);
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the exception
                            Console.WriteLine($"Error processing blood request from queue: {ex.Message}");

                            // Abandon the message to make it available for processing again
                            await receiver.AbandonMessageAsync(message);
                        }
                    }

                    return bloodRequestsFromQueue;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error receiving blood requests from queue: {ex.Message}");
                return new List<BloodRequest>();
            }
        }

    }
}
