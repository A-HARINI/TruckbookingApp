using System;
using System.Collections.Generic;

namespace Truck1.Model;

public partial class Document
{
    public long Id { get; set; }

    public string? ImagePath { get; set; }

    public string? ExcelPath { get; set; }

    public string? PdfPath { get; set; }
}
