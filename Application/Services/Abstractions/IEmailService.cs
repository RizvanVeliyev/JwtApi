using Application.Dtos;

namespace Application.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailSendDto dto);
    }
}
