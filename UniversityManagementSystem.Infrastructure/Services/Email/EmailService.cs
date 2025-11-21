using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UniversityManagementSystem.Application.Services;

namespace UniversityManagementSystem.Infrastructure.Services.Email;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;
    private readonly EmailOptions _emailOptions;

    public MockEmailService(IOptions<EmailOptions> emailOptions, ILogger<MockEmailService> logger)
    {
        _logger = logger;
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        if (!_emailOptions.EnableEmailSending)
        {
            _logger.LogInformation("📧 [MOCK EMAIL] Email sending is disabled. Would have sent to: {To}", to);
            return;
        }

        // Simulate email sending delay
        await Task.Delay(100);

        _logger.LogInformation("""
        📧 [MOCK EMAIL] Email Sent Successfully:
        From: {FromName} <{FromAddress}>
        To: {To}
        Subject: {Subject}
        Body: {Body}
        """, _emailOptions.FromName, _emailOptions.FromAddress, to, subject, body);
    }

    public async Task SendEmailConfirmationEmailAsync(string email, string firstName, string confirmationLink)
    {
        var subject = "Confirm Your Email - University Management System";
        var body = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                .header {{ background: #2c3e50; color: white; padding: 20px; text-align: center; }}
                .content {{ padding: 20px; background: #f9f9f9; }}
                .button {{ display: inline-block; padding: 12px 24px; background: #3498db; color: white; text-decoration: none; border-radius: 5px; }}
                .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
            </style>
        </head>
        <body>
            <div class="container">
                <div class="header">
                    <h1>University Management System</h1>
                </div>
                <div class="content">
                    <h2>Hello {firstName}!</h2>
                    <p>Thank you for registering with our University Management System. Please confirm your email address by clicking the button below:</p>
                    <p style="text-align: center;">
                        <a href="{confirmationLink}" class="button">Confirm Email Address</a>
                    </p>
                    <p>If the button doesn't work, you can copy and paste the following link in your browser:</p>
                    <p><code>{confirmationLink}</code></p>
                    <p>This link will expire in 24 hours.</p>
                </div>
                <div class="footer">
                    <p>&copy; {DateTime.Now.Year} University Management System. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
        """;

        await SendEmailAsync(email, subject, body);
        
        _logger.LogInformation("✅ Email confirmation sent to {Email} for user {FirstName}", email, firstName);
    }

    public async Task SendPasswordResetEmailAsync(string email, string firstName, string resetLink)
    {
        var subject = "Reset Your Password - University Management System";
        var body = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                .header {{ background: #e74c3c; color: white; padding: 20px; text-align: center; }}
                .content {{ padding: 20px; background: #f9f9f9; }}
                .button {{ display: inline-block; padding: 12px 24px; background: #e74c3c; color: white; text-decoration: none; border-radius: 5px; }}
                .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
                .warning {{ background: #fff3cd; border: 1px solid #ffeaa7; padding: 10px; border-radius: 5px; margin: 10px 0; }}
            </style>
        </head>
        <body>
            <div class="container">
                <div class="header">
                    <h1>Password Reset Request</h1>
                </div>
                <div class="content">
                    <h2>Hello {firstName}!</h2>
                    <p>We received a request to reset your password for the University Management System.</p>
                    <p>Click the button below to reset your password:</p>
                    <p style="text-align: center;">
                        <a href="{resetLink}" class="button">Reset Password</a>
                    </p>
                    <div class="warning">
                        <strong>Important:</strong> This link will expire in 1 hour. If you didn't request a password reset, please ignore this email.
                    </div>
                    <p>If the button doesn't work, you can copy and paste the following link in your browser:</p>
                    <p><code>{resetLink}</code></p>
                </div>
                <div class="footer">
                    <p>&copy; {DateTime.Now.Year} University Management System. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
        """;

        await SendEmailAsync(email, subject, body);
        
        _logger.LogInformation("✅ Password reset email sent to {Email} for user {FirstName}", email, firstName);
    }

    public async Task SendWelcomeEmailAsync(string email, string firstName)
    {
        var subject = "Welcome to University Management System!";
        var body = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                .header {{ background: #27ae60; color: white; padding: 20px; text-align: center; }}
                .content {{ padding: 20px; background: #f9f9f9; }}
                .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
            </style>
        </head>
        <body>
            <div class="container">
                <div class="header">
                    <h1>Welcome Aboard!</h1>
                </div>
                <div class="content">
                    <h2>Hello {firstName}!</h2>
                    <p>Welcome to the University Management System! Your account has been successfully created and is ready to use.</p>
                    <p>You can now log in to your account and start exploring all the features available to you.</p>
                    <p>If you have any questions or need assistance, please don't hesitate to contact our support team.</p>
                </div>
                <div class="footer">
                    <p>&copy; {DateTime.Now.Year} University Management System. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
        """;

        await SendEmailAsync(email, subject, body);
        
        _logger.LogInformation("✅ Welcome email sent to {Email} for user {FirstName}", email, firstName);
    }
}

