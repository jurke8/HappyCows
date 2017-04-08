using System.ComponentModel;

namespace HappyCows.Enums
{
    public enum EventEnum
    {
        [Description("Insemninated")] 
        INSEMINATED = 1,
        [Description("Pregnant")]  
        PREGNANT = 2,
        [Description("Open")]
        OPEN = 3
    }
}