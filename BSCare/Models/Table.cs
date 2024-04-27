using System;
using System.Collections.Generic;

namespace BSCare.Models;

public partial class Table
{
    public int ReportId { get; set; }

    public int ReportSource { get; set; }

    public DateTime OpenDate { get; set; }

    public DateTime CloseDate { get; set; }

    public int Status { get; set; }

    public int StopId { get; set; }

    public int HazardType { get; set; }

    public string Description { get; set; } = null!;

    public string? PicPath { get; set; }
}
