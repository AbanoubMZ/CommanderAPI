using System.ComponentModel.DataAnnotations;

namespace Commander.DTOS
{
    public class CommandUpdateDTO 
    {
        [Required]
        [MaxLength(350)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
        
    }
}