using System.ComponentModel;

namespace HappyCows.Enums
{
    public enum CowStateEnum
    {
        [Description("Otvoreno")]
        OPEN = 1,
        [Description("Osjemenjeno")]
        INSEMINATED = 2,
        [Description("Steono")]
        PREGNANT = 3

    }
}