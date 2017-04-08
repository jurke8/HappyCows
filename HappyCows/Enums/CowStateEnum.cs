using System.ComponentModel;

namespace HappyCows.Enums
{
    public enum CowStateEnum
    {
        [Description("Insemination")]
        INSEMINATION = 1,
        [Description("Examination")] 
        EXAMINATION = 2,
        [Description("Calving")]  
        CALVING = 3,
    }
}