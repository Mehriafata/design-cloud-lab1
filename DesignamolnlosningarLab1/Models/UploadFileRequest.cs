using System.ComponentModel.DataAnnotations;

namespace DesignamolnlosningarLab1.Models
{
    public class UploadFileRequest
    {
        [Required]
        public IFormFile File { get; set; } = default!;
    }
}
