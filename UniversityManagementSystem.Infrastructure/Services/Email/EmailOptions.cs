namespace UniversityManagementSystem.Infrastructure.Services.Email;

public class EmailOptions
{
    public string FromAddress { get; set; } = "noreply@university.com";
    public string FromName { get; set; } = "University Management System";
    public string SmtpServer { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public bool UseSSL { get; set; } = true;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableEmailSending { get; set; } = true;
}
