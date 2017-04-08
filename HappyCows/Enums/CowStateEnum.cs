using System.ComponentModel.DataAnnotations;

namespace HappyCows.Enums
{
    public enum CowStateEnum
    {
        [Display(Name = "Otvoreno")]
        OPEN,
        [Display(Name = "Osjemenjeno")]
        INSEMINATED,
        [Display(Name = "Steono")]
        PREGNANT

    }
}