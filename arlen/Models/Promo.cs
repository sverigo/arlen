using System.ComponentModel.DataAnnotations;

namespace arlen.Models
{
    public class Promo
    {
        public int Id { get; set; }
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Длинна поля Text должна быть в диапазоне от 5 до 150 символов")]
        public string Text { get; set; } = "";
        public string Image { get; set; } = "";

    }
}