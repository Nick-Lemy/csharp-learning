using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace NotificationService.Services;

public class EmailSender : IEmailSender
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _user;
    private readonly string _password;
    private readonly string _fromAddress;

    public EmailSender(string host, int port, string user, string password, string fromAddress)
    {
        _host = host;
        _port = port;
        _user = user;
        _password = password;
        _fromAddress = fromAddress;
    }

    public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_fromAddress));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();

        try 
        {
            await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_user, _password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
        finally
        {
        await client.DisconnectAsync(true, CancellationToken.None);
        }
    }
}