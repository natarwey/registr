using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace registr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _email;
        private static string _password;
        private static int _code;
        public MainWindow()
        {
            InitializeComponent();
        }

        private static class MailSender
        {
            public static async Task SendMail(string recipient)
            {
                Random rnd = new Random();
                _code = rnd.Next(1000, 9999);

                MailAddress fromAddress = new MailAddress("brm280706@gmail.com", "Boss");
                MailAddress toAddress = new MailAddress(recipient);

                using (MailMessage message = new MailMessage(fromAddress, toAddress))
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    message.Subject = "Добро пожаловать!";
                    message.Body = $"<h1>Код подтверждения:<br>{_code}</h1><br><h1>Данные для входа в аккаунт</h1><p>Почта: {_email}<br>Пароль: {_password}</p>";
                    message.IsBodyHtml = true;

                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    smtpClient.Credentials = new NetworkCredential(fromAddress.Address, "woyo zwzk kyoc ldtb");

                    await smtpClient.SendMailAsync(message);
                }
            }
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmailTB.Text.Trim() == String.Empty || PasswordTB.Password.Trim() == String.Empty)
            {
                if (EmailTB.Text.Trim() == String.Empty)
                    EmailTB.Background = Brushes.LightPink;
                if (PasswordTB.Password.Trim() == String.Empty)
                    PasswordTB.Background = Brushes.LightPink;
                MessageBox.Show("Все поля должны быть заполнены!");
                PasswordTB.Background = Brushes.White;
                EmailTB.Background = Brushes.White;
                return;
            }

            if (!EmailTB.Text.Contains("@"))
            {
                EmailTB.Background = Brushes.LightPink;
                MessageBox.Show("Неверный формат почты!");
                EmailTB.Clear();
                EmailTB.Background = Brushes.White;
                return;
            }

            EmailTB.Background = Brushes.LightGreen;
            PasswordTB.Background = Brushes.LightGreen;

            _email = EmailTB.Text;
            _password = PasswordTB.Password;
            SendMessage();

            MessageBox.Show($"Данные для входа отправлены на почту {EmailTB.Text}");

            EmailTB.Clear();
            PasswordTB.Clear();
            PasswordTB.Background = Brushes.White;
            EmailTB.Background = Brushes.White;

            new Window1(_code).ShowDialog();
        }

        private async Task SendMessage()
        {
            await MailSender.SendMail(_email);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
