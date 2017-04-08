using System.ComponentModel;

namespace HappyCows.Enums
{
    public enum EventEnum
    {
        [Description("Osjemenjivanje")]
        INSEMINATION = 1,
        [Description("Pregled")]
        EXAMINATION = 2,
        [Description("Teljenje")]
        CALVING = 3,
    }
}