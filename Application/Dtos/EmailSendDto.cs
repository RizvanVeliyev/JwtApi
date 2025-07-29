using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class EmailSendDto
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
