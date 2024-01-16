using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Controllers;
using WebApplication1.Model;
using WebApplication1.Source.Db;

namespace WebApplication1.Source.Svc
{
    public class DonationService : IDonationService
    {
        private readonly BloodContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService emailService;

        public DonationService(BloodContext context)
        {
            _context = context;
            emailService = new EmailService("t85846658@gmail.com", "iokz tpco nhlc ufzq");
        }

        public List<BloodDonation> getBloodDonations()
        {
            DonorAccess access = new DonorAccess(_context);
            return access.getAllBloodDonations().ToList();
        }
        public string AddBlood(string donorName, int unit)
        {
            var donor = _context.Donors.FirstOrDefault(d => d.FullName == donorName);

            if (donor != null)
            {
                var donation = _context.BloodDonations
                    .FirstOrDefault(d => d.DonorName == donorName && d.BloodType == donor.BloodType);

                if (donation != null)
                {
                    donation.Units += unit;
                }
                else
                {
                    _context.BloodDonations.Add(new BloodDonation
                    {
                        DonorName = donorName,
                        BloodType = donor.BloodType,
                        Units = unit
                    });
                }

                _context.SaveChanges();
                return "Blood Added Successfully";
            }

            return "Donor not found";
        }
        public string RequestBlood(BloodRequest request)
        {
            var bloodType = _context.Donors
                .Where(d => d.FullName == request.DonorName)
                .Select(d => d.BloodType)
                .FirstOrDefault();

            if (bloodType == null)
            {
                return "Donor not found";
            }

            var totalUnitsAvailable = _context.BloodDonations
                .Where(d => d.BloodType == bloodType)
                .Sum(d => d.Units);

            if (totalUnitsAvailable >= request.Units)
            {
                var donors = _context.BloodDonations
                    .Where(d => d.BloodType == bloodType)
                    .ToList();

                var bloodNeed = request.Units;
                var donorNameList = new List<string>();

                foreach (var donor in donors)
                {
                    if (donor.Units > bloodNeed)
                    {
                        donor.Units -= bloodNeed;
                        bloodNeed = 0;
                        donorNameList.Add(donor.DonorName);
                        _context.SaveChanges();
                        break;
                    }
                    else if (donor.Units == bloodNeed)
                    {
                        bloodNeed = 0;
                        donorNameList.Add(donor.DonorName);
                        _context.BloodDonations.Remove(donor);
                        _context.SaveChanges();
                        break;
                    }
                    else
                    {
                        bloodNeed -= donor.Units;
                        donorNameList.Add(donor.DonorName);
                        _context.BloodDonations.Remove(donor);
                    }
                }
                var toEmail = "t85846658@gmail.com";
                var subject = "Blood Donation Notification";
                var body = "Dear recipient, blood has been found. Thank you for your donation.";

                // Send email asynchronously
                Task.Run(async () => await emailService.SendEmailAsync(toEmail, subject, body))
                    .GetAwaiter()
                    .GetResult();

                return string.Join(", ", donorNameList);
            }
            else
            {
                
                try
                {
                    Task.Run(async () => await SendBloodRequestToQueueAsync(request))
        .GetAwaiter()
        .GetResult();
                    // Continue with the rest of your logic
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Error in RequestBlood: {ex.Message}");
                }


                return "Not enough blood, request sent to queue";
            }
        }

        private async Task SendBloodRequestToQueueAsync(BloodRequest request)
        {
            var serviceBusConnectionString = "Endpoint=sb://final19070006002.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Cttg1DfSeL1rjafuZ7wATWQhhrH8xY0cS+ASbIiyFQM=";
            var queueName = "bloodrequestqueue";

           

            if (string.IsNullOrEmpty(serviceBusConnectionString))
            {
                // Log or handle the null connection string
                Console.WriteLine("Service Bus connection string is null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(queueName))
            {
                // Log or handle the null queue name
                Console.WriteLine("Queue name is null or empty.");
                return;
            }
            if (request == null)
            {
                // Log or handle the null request
                Console.WriteLine("Blood request object is null.");
                return;
            }
            try
            {
                // Create a Service Bus client
                await using (ServiceBusClient client = new ServiceBusClient(serviceBusConnectionString))
                {
                    // Create a sender for the queue
                    ServiceBusSender sender = client.CreateSender(queueName);

                    // Serialize the request object to JSON
                    var requestJson = JsonSerializer.Serialize(request);

                    // Create a message containing the JSON
                    ServiceBusMessage message = new ServiceBusMessage(requestJson);
                    //ServiceBusMessage message = new ServiceBusMessage("deneme");

                    // Send the message to the queue
                    await sender.SendMessageAsync(message);
                }

                // Log success
                Console.WriteLine("Blood request message sent successfully.");
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"Error sending blood request message: {ex.Message}");
            }
            if (request == null)
            {
                // Log or handle the null request
                Console.WriteLine("Blood request object is null.");
                return;
            }
        }

    }
}
