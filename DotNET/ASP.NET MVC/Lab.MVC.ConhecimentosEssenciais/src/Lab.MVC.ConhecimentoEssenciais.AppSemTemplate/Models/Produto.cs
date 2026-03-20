using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.MVC.AppSemTemplate.Models
{
    [Table("AppSemTemplate_Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Name { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Valor { get; set; }

        [NotMapped]
        public IFormFile? Upload { get; set; }
    }
}
