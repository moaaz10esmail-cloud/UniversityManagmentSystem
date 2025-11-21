namespace UniversityManagementSystem.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendPasswordResetEmailAsync(string to, string userName, string resetLink);
    Task SendEmailConfirmationEmailAsync(string to, string userName, string confirmationLink);
    Task SendWelcomeEmailAsync(string to, string userName);
}
