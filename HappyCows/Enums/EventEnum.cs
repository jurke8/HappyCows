using System.ComponentModel.DataAnnotations;

namespace HappyCows.Enums
{
    public enum EventEnum
    {
        [Display(Name = "Osjemenjivanje")]
        INSEMINATION,
        [Display(Name = "Pregled")]
        EXAMINATION,
        [Display(Name = "Teljenje")]
        CALVING
    }
}