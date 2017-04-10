using HappyCows.Enums;
using HappyCows.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HappyCows.Utils
{
    public class ReportUtils
    {
        public MemoryStream GenerateReport(int? typeFilter, Guid? cowId)
        {
            try
            {
                HappyCowsContext db = new HappyCowsContext();
                var events = db.Events.ToList();

                // Filtering
                if (typeFilter != null)
                {
                    events = events.Where(e => (int)e.EventType == typeFilter).ToList();
                }
                if (cowId != null)
                {
                    events = events.Where(e => e.CowId == cowId).ToList();
                }

                var package = new ExcelPackage();
                var ws = CreateSheet(package, "Detalji događaja", 1);

                //Header
                ws.Cells[1, 1].Value = "Ime";
                ws.Cells[1, 2].Value = "Tip";
                ws.Cells[1, 3].Value = "Krava";
                ws.Cells[1, 4].Value = "Datum";

                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 2].Style.Font.Bold = true;
                ws.Cells[1, 3].Style.Font.Bold = true;
                ws.Cells[1, 4].Style.Font.Bold = true;

                //Body
                var n = 2;

                foreach (var e in events)
                {
                    ws.Cells[n, 1].Value = e.Name;
                    ws.Cells[n, 2].Value = e.EventType.GetDisplayName();
                    ws.Cells[n, 3].Value = e.Cow.Name;
                    ws.Cells[n, 4].Value = e.EventDate;
                    ws.Cells[n, 4].Style.Numberformat.Format = "dd.MM.yyyy";
                    n++;
                }

                //Cells style
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                ws.Cells[ws.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[ws.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;
                return fileStream;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int sheetId)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[sheetId];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
            return ws;
        }
    }
}