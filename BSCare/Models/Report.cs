using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BSCare.Models;

public partial class Report
{
    [DisplayName("מספר דיווח")]
    public int ReportId { get; set; }

    [DisplayName("יוזם הדיווח")]
    public int ReportSource { get; set; }
    
    [DisplayName("תאריך פתיחה")]

    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime OpenDate { get; set; }

    [DisplayName("תאריך סגירה")]

    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? CloseDate { get; set; }

    [DisplayName("סטטוס")]
    public int Status { get; set; }

    [DisplayName("מספר תחנה")]
    public int StopId { get; set; }

    [DisplayName("סוג תקלה")]
    public int HazardType { get; set; }

    [DisplayName("תיאור תקלה")]
    public string? Description { get; set; }

    [DisplayName("נתיב תמונה")]
    public string? PicPath { get; set; }

    public virtual ICollection<Repair> Repairs { get; } = new List<Repair>();

    public virtual Stop? Stop { get; set; } = null!;
}
