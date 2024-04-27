using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BSCare.Models;

public partial class Repair
{
    public int ExpensesId { get; set; }

    
    public int ReportId { get; set; }

    [DisplayName("התיקון שבוצע")]
    public string RepairAction { get; set; } = null!;

    [DisplayName("עלות התיקון")]
    public int ActionPrice { get; set; }

    [DisplayName("מספר תחנה")]
    public int StopCode { get; set; }

    [DisplayName("מספר דיווח")]
    public virtual Report Report { get; set; } = null!;
}
