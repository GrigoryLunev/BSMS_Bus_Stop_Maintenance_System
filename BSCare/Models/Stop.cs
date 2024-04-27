using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BSCare.Models;

public partial class Stop
{
    public int StopId { get; set; }

    [DisplayName("מספר תחנה")]
    public int StopCode { get; set; }

    [DisplayName("שם תחנה")]
    public string? StopName { get; set; }

    [DisplayName("תיאור")]
    public string? StopDesc { get; set; }

    [DisplayName("קו רוחב")]
    public double StopLat { get; set; }

    [DisplayName("קו אורך")]
    public double StopLon { get; set; }

    [DisplayName("סוג מיקום")]
    public int? LocationType { get; set; }

    [DisplayName("תחנת אב")]
    public int? ParentStation { get; set; }

    [DisplayName("אזור")]
    public int? ZoneId { get; set; }

    public virtual ICollection<Report> Reports { get; } = new List<Report>();
}
