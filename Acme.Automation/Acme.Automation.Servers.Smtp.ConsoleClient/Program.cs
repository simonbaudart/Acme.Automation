using System;

namespace Acme.Automation.Servers.Smtp.ConsoleClient
{
    using System.Net.Mail;

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("From ?");
                var from = Console.ReadLine();

                Console.WriteLine("To ?");
                var to = Console.ReadLine();

                Console.WriteLine("Subject ?");
                var subject = Console.ReadLine();

                Console.WriteLine("Body ?");
                var body = Console.ReadLine();

                if (from != null && to != null)
                {
                    var mail = new MailMessage(from, to, subject, body);
                    try
                    {
                        new SmtpClient("localhost").Send(mail);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}