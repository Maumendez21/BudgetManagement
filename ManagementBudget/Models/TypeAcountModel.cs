using System.ComponentModel.DataAnnotations;

namespace ManagementBudget.Models
{
    public class TypeAcountModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage ="La longitud de {0} debe ser de {2} y  {1}")]
        [Display(Name = "Nombre del tipo de cuenta")]
        public string Name { get; set; }
        public int  UserId { get; set; }
        public int Order { get; set; }
    }
}
