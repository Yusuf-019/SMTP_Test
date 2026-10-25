using System.Net;
using System.Net.Mail;
using System.Text;

namespace SMTP_Test
{
    internal class Program
    {
        static string fromMail = "yusufcaf8775@gmail.com";
        static string password = "indc basr mvbm pqyx";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Name: ");
            string? name = Console.ReadLine();

            Console.Write("Email: ");
            string? email = Console.ReadLine();

            Console.Write("Password: ");
            string? userPassword = Console.ReadLine();

            Random rand = new Random();
            string code = rand.Next(100000, 1000000).ToString();

            var mail = new MailMessage();
            mail.From = new MailAddress(fromMail);
            mail.To.Add(new MailAddress(email!));
            mail.Subject = "Code";
            mail.Body = $"6 reqemli sifre --> {code}";

            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(fromMail, password); 

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Code gonderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xeta: " + ex.Message);
                return;
            }

            DateTime startTime = DateTime.Now;
            int attempts = 0;

            while (attempts < 3)
            {
                if ((DateTime.Now - startTime).TotalMinutes > 3)
                {
                    Console.WriteLine("3 deqiqelik vaxtiniz bitdi!");
                    return;
                }

                attempts++;
                Console.Write($"Kodu daxil edin (Cehd {attempts}/3): ");
                string? inputCode = Console.ReadLine();

                if (inputCode == code)
                {
                    Console.WriteLine($"Tebriklər {name}, qeydiyyat tamamlandi!");
                    return;
                }
                else
                {
                    Console.WriteLine("Yanlis kod!");
                }
            }

            Console.WriteLine("3 cehd haqqiniz bitdi!");
        }
    }
}