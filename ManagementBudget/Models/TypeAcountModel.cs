using ManagementBudget.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManagementBudget.Models
{
    public class TypeAcountModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre del tipo de cuenta")]
        //[FirstUpper] Validacion personalizada por atributo
        [Remote(action: "VerifyExistTypeAcount", controller: "TypeAcount")]
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Order { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name != null && Name.Length > 0)
            {
                var firstLeter = Name[0].ToString();
                if (firstLeter != firstLeter.ToUpper())
                {
                    yield return new ValidationResult("La primera letra tiene que ser mayusucla", new[] { nameof(Name) });
                }
            }
        }


    }
}
