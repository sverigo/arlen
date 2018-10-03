using System.ComponentModel.DataAnnotations;

namespace arlen.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string AccountEmail { get; set; } = "";

        public string Phones { get; set; } = "";
        public string Youtube { get; set; } = "";
        public string Facebook { get; set; } = "";
        public string Linkedin { get; set; } = "";
        public string Twitter { get; set; } = "";
        public string Viber { get; set; } = "";
        public string Whatsapp { get; set; } = "";
        public string Emails { get; set; } = "";
        public string Address { get; set; } = "";
        public string Map { get; set; } = "";

        [StringLength(500, ErrorMessage = "Длина текста должна быть не более 500 символов")]
        public string AboutText { get; set; } = "";
        public string Logo { get; set; } = "";
        public string Files { get; set; } = "";
        public string AboutVideo { get; set; } = "";
        public string AboutImage { get; set; } = "";
        public bool AboutVideoMode { get; set; }

    }
}