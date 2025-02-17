using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DataModal.ModelsMaster
{
    public class ExportModal : IExportHelper
    {
        IReportHelper report;
        public ExportModal()
        {
            report = new ReportModal();
        }

        public ISheet GetWorkbookSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                var lightBG = SetCellColor("LightColor", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;

                int[] intColomnIndex = { 1 };

                if (Doctype == "Daily Sales")
                {
                    var r2 = sheet.CreateRow(0);
                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {

                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount <= 13 ? BGGreen : BGYellow);
                            HeadCount++;
                        }
                    }
                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_"))
                            {

                                if (dc.ColumnName.Contains("Image"))
                                {
                                    ICell cell = DyRows.CreateCell(CellCount);
                                    cell.SetCellValue(ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString());
                                    XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                    link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                    cell.Hyperlink = (link);
                                    ICellStyle hlink_style = workbook.CreateCellStyle();
                                    IFont hlink_font = workbook.CreateFont();
                                    hlink_font.Underline = FontUnderlineType.Single;
                                    hlink_font.Color = HSSFColor.Blue.Index;
                                    hlink_style.SetFont(hlink_font);
                                    cell.CellStyle = (hlink_style);
                                    CellCount++;
                                }
                                else
                                {
                                    ICell cell = DyRows.CreateCell(CellCount);
                                    cell.SetCellValue(dtRow[dc].ToString());
                                    if (intColomnIndex.Contains(CellCount + 1))
                                    {
                                        cell.SetCellType(CellType.Numeric);
                                        int qty = 0;
                                        int.TryParse(dtRow[dc].ToString(), out qty);
                                        cell.SetCellValue(qty);
                                    }
                                    cell.CellStyle = RowBG;
                                    CellCount++;
                                }
                            }

                        }


                    }
                }
                else if (Doctype == "Trgt Vs Achv")
                {
                    var r1 = sheet.CreateRow(0);
                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r1.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r1.Cells[HeadCount].CellStyle = BGYellow;
                                }

                            }
                            else
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                                r1.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    var r2 = sheet.CreateRow(1);
                    HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r2.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r2.Cells[HeadCount].CellStyle = BGYellow;
                                }
                            }
                            else
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                                r2.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    // Bind header End
                    rowCount = 1; CellCount = 0;
                    RowBG = BGdeafult;
                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (dc.ColumnName.Contains("#") || dc.ColumnName.ToUpper() == "MAN DAYS")
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float qty = 0;
                                    float.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }

                        }

                    }
                }
                else if (Doctype == "TL Tracker")
                {
                    var r1 = sheet.CreateRow(0);
                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r1.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r1.Cells[HeadCount].CellStyle = BGYellow;
                                }

                            }
                            else
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                                r1.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    var r2 = sheet.CreateRow(1);
                    HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r2.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r2.Cells[HeadCount].CellStyle = BGYellow;
                                }
                            }
                            else
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                                r2.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    // Bind header End
                    rowCount = 1; CellCount = 0;
                    RowBG = BGdeafult;
                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (dc.ColumnName.Contains("#") || dc.ColumnName.ToUpper() == "MAN DAYS")
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float qty = 0;
                                    float.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }

                        }

                    }
                }
                else if (Doctype == "Daily Attendance")
                {
                    int coloumnCOunt = result.Columns.Count;
                    if (coloumnCOunt > 20)
                    {
                        List<int> last11Numbers = Enumerable.Range(0, 11).Select(i => coloumnCOunt - i).ToList();
                        intColomnIndex = intColomnIndex.Concat(last11Numbers).ToArray();
                    }
                    var r2 = sheet.CreateRow(0);

                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount > 23 ? BGYellow : BGGreen);
                            HeadCount++;
                        }
                    }
                    // Bind header End
                    rowCount = 0; CellCount = 0;

                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                if (dtRow[dc].ToString().Contains("*"))
                                {
                                    cell.SetCellValue(dtRow[dc].ToString().Replace("*", ""));
                                    cell.CellStyle = lightBG;
                                }
                                else
                                {
                                    cell.SetCellValue(dtRow[dc].ToString());
                                    cell.CellStyle = RowBG;
                                }
                                if (intColomnIndex.Contains(CellCount + 1))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float Amount = 0;
                                    float.TryParse(dtRow[dc].ToString(), out Amount);
                                    cell.SetCellValue(Amount);
                                }
                                CellCount++;
                            }

                        }
                    }
                }
                else if (Doctype.Contains("ISD Sales Summary"))
                {
                    intColomnIndex = new int[] { 2, 3, 4, 5, 6, 7, 8, 9 };
                    var r2 = sheet.CreateRow(0);

                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount > 23 ? BGYellow : BGGreen);
                            HeadCount++;
                        }
                    }
                    // Bind header End
                    rowCount = 0; CellCount = 0;

                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                cell.CellStyle = RowBG;
                                if (intColomnIndex.Contains(CellCount + 1))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float Amount = 0;
                                    float.TryParse(dtRow[dc].ToString(), out Amount);
                                    cell.SetCellValue(Amount);
                                }
                                CellCount++;
                            }

                        }
                    }
                }
                else
                {
                    var r2 = sheet.CreateRow(0);

                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount > 23 ? BGYellow : BGGreen);
                            HeadCount++;
                        }
                    }
                    // Bind header End
                    rowCount = 0; CellCount = 0;

                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                if (dtRow[dc].ToString().Contains("*"))
                                {
                                    cell.SetCellValue(dtRow[dc].ToString().Replace("*", ""));
                                    cell.CellStyle = lightBG;
                                }
                                else
                                {
                                    cell.SetCellValue(dtRow[dc].ToString());
                                    cell.CellStyle = RowBG;
                                }
                                if (intColomnIndex.Contains(CellCount + 1) || CellCount >= 59)
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float Amount = 0;
                                    float.TryParse(dtRow[dc].ToString(), out Amount);
                                    cell.SetCellValue(Amount);
                                }
                                CellCount++;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetWorkbookSheet. The query was executed :", ex.ToString(), "GetWorkbookSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public ISheet GetWorkbookSheet_ISDSummary(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };
                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {

                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = BGGreen;
                        HeadCount++;
                    }
                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {

                            if (dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (intColomnIndex.Contains(CellCount + 1))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    int qty = 0;
                                    int.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetWorkbookSheet_ISDSummary. The query was executed :", ex.ToString(), "GetWorkbookSheet_ISDSummary()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public ISheet GetWorkbookSheet_Common(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {

                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = BGGreen;
                        HeadCount++;
                    }
                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {

                            if (dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetWorkbookSheet_ISDSummary. The query was executed :", ex.ToString(), "GetWorkbookSheet_ISDSummary()", "Export", "Export", 0, "");
            }

            return sheet;
        }
        public IWorkbook GetDataTable_Workbook_Common(DataTable result, string Doctype)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                // Bind Header start
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };
                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 23 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End

                // Keep track of the maximum length of data in each column
                int[] maxColumnWidths = new int[result.Columns.Count];

                rowCount = 0;
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            string cellValue = dtRow[dc].ToString();
                            cell.SetCellValue(cellValue);
                            cell.CellStyle = RowBG;
                            if (intColomnIndex.Contains(CellCount + 1) || CellCount >= 56)
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(cellValue, out Amount);
                                cell.SetCellValue(Amount);
                            }

                            // Update the maximum column width
                            int cellValueLength = cellValue.Length;
                            if (cellValueLength > maxColumnWidths[CellCount])
                            {
                                maxColumnWidths[CellCount] = cellValueLength;
                            }
                            CellCount++;
                        }
                    }
                }

                // Auto-size columns based on the maximum width calculated
                for (int i = 0; i < maxColumnWidths.Length; i++)
                {
                    // Adding a larger buffer to ensure the content fits
                    int columnWidth = (maxColumnWidths[i] + 2) * 256;
                    sheet.SetColumnWidth(i, columnWidth);
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDataTable_Workbook_Common. The query was executed :", ex.ToString(), "GetDataTable_Workbook()", "Export", "Export", 0, "");
            }

            return workbook;
        }

        public IWorkbook GetDataTable_Workbook(DataTable result, string Doctype)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };
                if (Doctype == "Daily Sales")
                {
                    var r2 = sheet.CreateRow(0);
                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {

                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount <= 13 ? BGGreen : BGYellow);
                            HeadCount++;
                        }
                    }
                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_"))
                            {

                                if (dc.ColumnName.Contains("Image"))
                                {
                                    ICell cell = DyRows.CreateCell(CellCount);
                                    cell.SetCellValue("Image Link");
                                    XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                    link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                    cell.Hyperlink = (link);
                                    ICellStyle hlink_style = workbook.CreateCellStyle();
                                    IFont hlink_font = workbook.CreateFont();
                                    hlink_font.Underline = FontUnderlineType.Single;
                                    hlink_font.Color = HSSFColor.Blue.Index;
                                    hlink_style.SetFont(hlink_font);
                                    cell.CellStyle = (hlink_style);
                                    CellCount++;
                                }
                                else
                                {
                                    ICell cell = DyRows.CreateCell(CellCount);
                                    cell.SetCellValue(dtRow[dc].ToString());
                                    if (intColomnIndex.Contains(CellCount + 1))
                                    {
                                        cell.SetCellType(CellType.Numeric);
                                        int qty = 0;
                                        int.TryParse(dtRow[dc].ToString(), out qty);
                                        cell.SetCellValue(qty);
                                    }
                                    cell.CellStyle = RowBG;
                                    CellCount++;
                                }
                            }

                        }
                    }
                }
                else if (Doctype == "Trgt Vs Achv")
                {
                    var r1 = sheet.CreateRow(0);
                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r1.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r1.Cells[HeadCount].CellStyle = BGYellow;
                                }

                            }
                            else
                            {
                                r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                                r1.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    var r2 = sheet.CreateRow(1);
                    HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_"))
                        {
                            if (item.ColumnName.Contains("#"))
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                                if (item.ColumnName.Split('#')[0] == "Productivity")
                                {
                                    r2.Cells[HeadCount].CellStyle = BGGreen;
                                }
                                else
                                {
                                    r2.Cells[HeadCount].CellStyle = BGYellow;
                                }
                            }
                            else
                            {
                                r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                                r2.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            HeadCount++;
                        }
                    }

                    // Bind header End
                    rowCount = 1; CellCount = 0;
                    RowBG = BGdeafult;
                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (dc.ColumnName.Contains("#") || dc.ColumnName.ToUpper() == "MAN DAYS")
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float qty = 0;
                                    float.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }

                        }

                    }
                }
                else
                {
                    var r2 = sheet.CreateRow(0);

                    int HeadCount = 0;
                    foreach (DataColumn item in result.Columns)
                    {
                        if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = (HeadCount > 23 ? BGYellow : BGGreen);
                            HeadCount++;
                        }
                    }
                    // Bind header End
                    rowCount = 0; CellCount = 0;

                    foreach (DataRow dtRow in result.Rows)
                    {
                        rowCount++;
                        RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                        IRow DyRows = sheet.CreateRow(rowCount);
                        CellCount = 0;
                        foreach (DataColumn dc in result.Columns)
                        {
                            if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                cell.CellStyle = RowBG;
                                if (intColomnIndex.Contains(CellCount + 1) || CellCount >= 56)
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    float Amount = 0;
                                    float.TryParse(dtRow[dc].ToString(), out Amount);
                                    cell.SetCellValue(Amount);
                                }
                                CellCount++;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDataTable_Workbook. The query was executed :", ex.ToString(), "GetDataTable_Workbook()", "Export", "Export", 0, "");
            }

            return workbook;
        }


        public IWorkbook GetSaleEntry_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetSaleEntryReport_Export(modal);

                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                string intColomn = "Qty,Price,S.No";

                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {

                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount <= 13 ? BGGreen : BGYellow);
                        HeadCount++;
                    }
                }

                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {

                            if (dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);

                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (intColomn.Contains(dc.ColumnName))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    int qty = 0;
                                    int.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;

                            }
                            CellCount++;
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetSaleEntry_Workbook. The query was executed :", ex.ToString(), "GetSaleEntryReportList()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook GetTargetVsAchievement_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetTargetVsAchievement_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                var r1 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                            if (item.ColumnName.Split('#')[0] == "Productivity")
                            {
                                r1.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            else
                            {
                                r1.Cells[HeadCount].CellStyle = BGYellow;
                            }

                        }
                        else
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                            r1.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                var r2 = sheet.CreateRow(1);
                HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                            if (item.ColumnName.Split('#')[0] == "Productivity")
                            {
                                r2.Cells[HeadCount].CellStyle = BGGreen;
                            }
                            else
                            {
                                r2.Cells[HeadCount].CellStyle = BGYellow;
                            }
                        }
                        else
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                // Bind header End
                int rowCount = 1, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            if (dc.ColumnName.Contains("#") || dc.ColumnName.ToUpper() == "MAN DAYS")
                            {
                                cell.SetCellType(CellType.Numeric);
                                float qty = 0;
                                float.TryParse(dtRow[dc].ToString(), out qty);
                                cell.SetCellValue(qty);
                            }
                            cell.CellStyle = RowBG;
                            CellCount++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTargetVsAchievement_Workbook. The query was executed :", ex.ToString(), "GetTargetVsAchievement_Report()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetMTDReport_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetMTD_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                var r1 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                            r1.Cells[HeadCount].CellStyle = BGYellow;

                        }
                        else
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                            r1.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                var r2 = sheet.CreateRow(1);
                HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                            r2.Cells[HeadCount].CellStyle = BGYellow;
                        }
                        else
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                // Bind header End
                int rowCount = 1, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            if (dc.ColumnName.Contains("#"))
                            {
                                cell.SetCellType(CellType.Numeric);
                                int qty = 0;
                                int.TryParse(dtRow[dc].ToString(), out qty);
                                cell.SetCellValue(qty);

                            }
                            cell.CellStyle = RowBG;
                            CellCount++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetMTD_Report. The query was executed :", ex.ToString(), "GetMTD_Report()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetPJPExpense_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = Common_SPU.GetPJPEntry_Expense(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                string intColomn = "Amount,S.No";
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 12 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (intColomn.Contains(dc.ColumnName))
                            {
                                cell.SetCellType(CellType.Numeric);
                                int Amount = 0;
                                int.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            else if (dc.ColumnName.ToLower() == "image" || dc.ColumnName.ToLower() == "exp. image")
                            {
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetPJPExpense_Workbook. The query was executed :", ex.ToString(), "GetPJPExpense_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetAttendanceLog_Day_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                List<Attendance_Log.Daily> result = report.GetAttendance_Log_Daily(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                // Bind Header star
                int HeadCount = 0;
                var r1 = sheet.CreateRow(0);
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("S No.");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Gender");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Phone");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Department");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Designation");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("DOJ");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Role");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Region");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("State");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("City");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Area");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Type");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Branch Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;


                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Title");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Working");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("In Time");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("In Distance");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("In Lattitude");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("In Longitude");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Out Time");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Out Distance");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Out Lattitude");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Out Longitude");
                r1.Cells[HeadCount].CellStyle = BGYellow;
                HeadCount++;

                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (Attendance_Log.Daily row in result)
                {
                    rowCount++;
                    CellCount = 0;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    ICell cell0 = DyRows.CreateCell(CellCount);
                    cell0.SetCellValue(rowCount);
                    cell0.CellStyle = RowBG;
                    CellCount++;
                    ICell cell1 = DyRows.CreateCell(CellCount);
                    cell1.SetCellValue(row.EMPCode);
                    cell1.CellStyle = RowBG;
                    CellCount++;
                    ICell cell2 = DyRows.CreateCell(CellCount);
                    cell2.SetCellValue(row.EMPName);
                    cell2.CellStyle = RowBG;
                    CellCount++;
                    ICell cell3 = DyRows.CreateCell(CellCount);
                    cell3.SetCellValue(row.Gender);
                    cell3.CellStyle = RowBG;
                    CellCount++;
                    ICell cellG = DyRows.CreateCell(CellCount);
                    cellG.SetCellValue(row.Phone);
                    cellG.CellStyle = RowBG;
                    CellCount++;
                    ICell cell5 = DyRows.CreateCell(CellCount);
                    cell5.SetCellValue(row.DeptName);
                    cell5.CellStyle = RowBG;
                    CellCount++;
                    ICell cell6 = DyRows.CreateCell(CellCount);
                    cell6.SetCellValue(row.DesignName);
                    cell6.CellStyle = RowBG;
                    CellCount++;
                    ICell cell4 = DyRows.CreateCell(CellCount);
                    cell4.SetCellValue(row.DOJ);
                    cell4.CellStyle = RowBG;
                    CellCount++;
                    ICell cellR = DyRows.CreateCell(CellCount);
                    cellR.SetCellValue(row.Role);
                    cellR.CellStyle = RowBG;
                    CellCount++;
                    ICell cell7 = DyRows.CreateCell(CellCount);
                    cell7.SetCellValue(row.Region);
                    cell7.CellStyle = RowBG;
                    CellCount++;
                    ICell cell8 = DyRows.CreateCell(CellCount);
                    cell8.SetCellValue(row.State);
                    cell8.CellStyle = RowBG;
                    CellCount++;
                    ICell cell9 = DyRows.CreateCell(CellCount);
                    cell9.SetCellValue(row.City);
                    cell9.CellStyle = RowBG;
                    CellCount++;
                    ICell cell10 = DyRows.CreateCell(CellCount);
                    cell10.SetCellValue(row.Area);
                    cell10.CellStyle = RowBG;
                    CellCount++;
                    ICell cellDN = DyRows.CreateCell(CellCount);
                    cellDN.SetCellValue(row.DealerName);
                    cellDN.CellStyle = RowBG;
                    CellCount++;
                    ICell cellDC = DyRows.CreateCell(CellCount);
                    cellDC.SetCellValue(row.DealerCode);
                    cellDC.CellStyle = RowBG;
                    CellCount++;
                    ICell cellDT = DyRows.CreateCell(CellCount);
                    cellDT.SetCellValue(row.DealerType);
                    cellDT.CellStyle = RowBG;
                    CellCount++;
                    ICell cellBN = DyRows.CreateCell(CellCount);
                    cellBN.SetCellValue(row.BranchName);
                    cellBN.CellStyle = RowBG;
                    CellCount++;


                    ICell cellTI = DyRows.CreateCell(CellCount);
                    cellTI.SetCellValue(row.Title);
                    cellTI.CellStyle = RowBG;
                    CellCount++;
                    ICell cellWR = DyRows.CreateCell(CellCount);
                    cellWR.SetCellValue(row.Working.ToString());
                    cellWR.CellStyle = RowBG;
                    CellCount++;

                    ICell cellINT = DyRows.CreateCell(CellCount);
                    cellINT.SetCellValue(row.In_Time.ToString());
                    cellINT.CellStyle = RowBG;
                    CellCount++;

                    ICell cellINP = DyRows.CreateCell(CellCount);
                    cellINP.SetCellType(CellType.Numeric);
                    float In_PunchDistance = 0;
                    float.TryParse(row.In_PunchDistance, out In_PunchDistance);
                    cellINP.SetCellValue(In_PunchDistance);
                    cellINP.CellStyle = RowBG;
                    CellCount++;

                    ICell cellINLat = DyRows.CreateCell(CellCount);
                    cellINLat.SetCellValue(row.In_Lat.ToString());
                    cellINLat.CellStyle = RowBG;
                    CellCount++;

                    ICell cellINLong = DyRows.CreateCell(CellCount);
                    cellINLong.SetCellValue(row.In_Long.ToString());
                    cellINLong.CellStyle = RowBG;
                    CellCount++;

                    ICell cellOTT = DyRows.CreateCell(CellCount);
                    cellOTT.SetCellValue(row.Out_Time.ToString());
                    cellOTT.CellStyle = RowBG;
                    CellCount++;
                    ICell cell25 = DyRows.CreateCell(CellCount);
                    cell25.SetCellType(CellType.Numeric);
                    float Out_PunchDistance = 0;
                    float.TryParse(row.Out_PunchDistance, out Out_PunchDistance);
                    cell25.SetCellValue(Out_PunchDistance);
                    cell25.CellStyle = RowBG;
                    CellCount++;

                    ICell cellOTLat = DyRows.CreateCell(CellCount);
                    cellOTLat.SetCellValue(row.Out_Lat.ToString());
                    cellOTLat.CellStyle = RowBG;
                    CellCount++;

                    ICell cellOTLong = DyRows.CreateCell(CellCount);
                    cellOTLong.SetCellValue(row.Out_Long.ToString());
                    cellOTLong.CellStyle = RowBG;
                    CellCount++;

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetAttendanceLog_Day_Workbook. The query was executed :", ex.ToString(), "GetAttendanceLog_Day_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook GetAttendanceLog_Monthly_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DateTime dt;
                DateTime.TryParse(modal.Month, out dt);

                DateTime baseDate = dt;
                DateTime StartDate = baseDate.AddDays(1 - baseDate.Day);
                DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);

                List<Attendance_Log.Monthly> result = report.GetAttendance_Log_MonthlyList(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int HeadCount = 0;
                var r1 = sheet.CreateRow(0);
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("S No.");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Gender");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Phone");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Department");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Designation");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("DOJ");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Role");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Region");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("State");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("City");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Area");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Type");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Branch Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Status");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                {
                    r1.CreateCell(HeadCount, CellType.String).SetCellValue(da.ToString("dd-MMM-yyyy"));
                    r1.Cells[HeadCount].CellStyle = BGYellow;
                    HeadCount++;
                }


                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (var row in result)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    CellCount = 0;
                    IRow DyRows = sheet.CreateRow(rowCount);
                    ICell cell0 = DyRows.CreateCell(CellCount);
                    cell0.SetCellValue(rowCount);
                    cell0.CellStyle = RowBG;
                    CellCount++;
                    ICell cell2 = DyRows.CreateCell(CellCount);
                    cell2.SetCellValue(row.EMPCode);
                    cell2.CellStyle = RowBG;
                    CellCount++;
                    ICell cell1 = DyRows.CreateCell(CellCount);
                    cell1.SetCellValue(row.EMPName);
                    cell1.CellStyle = RowBG;
                    CellCount++;

                    ICell cell3 = DyRows.CreateCell(CellCount);
                    cell3.SetCellValue(row.Gender);
                    cell3.CellStyle = RowBG;

                    CellCount++;
                    ICell cell4 = DyRows.CreateCell(CellCount);
                    cell4.SetCellValue(row.Phone);
                    cell4.CellStyle = RowBG;

                    CellCount++;
                    ICell cell5 = DyRows.CreateCell(CellCount);
                    cell5.SetCellValue(row.DeptName);
                    cell5.CellStyle = RowBG;
                    CellCount++;
                    ICell cell6 = DyRows.CreateCell(CellCount);
                    cell6.SetCellValue(row.DesignName);
                    cell6.CellStyle = RowBG;
                    CellCount++;
                    ICell cellDO = DyRows.CreateCell(CellCount);
                    cellDO.SetCellValue(row.DOJ);
                    cellDO.CellStyle = RowBG;
                    CellCount++;
                    ICell cellRO = DyRows.CreateCell(CellCount);
                    cellRO.SetCellValue(row.Role);
                    cellRO.CellStyle = RowBG;
                    CellCount++;
                    ICell cell7 = DyRows.CreateCell(CellCount);
                    cell7.SetCellValue(row.Region);
                    cell7.CellStyle = RowBG;
                    CellCount++;
                    ICell cell8 = DyRows.CreateCell(CellCount);
                    cell8.SetCellValue(row.State);
                    cell8.CellStyle = RowBG;
                    CellCount++;
                    ICell cell9 = DyRows.CreateCell(CellCount);
                    cell9.SetCellValue(row.City);
                    cell9.CellStyle = RowBG;
                    CellCount++;
                    ICell cell10 = DyRows.CreateCell(CellCount);
                    cell10.SetCellValue(row.Area);
                    cell10.CellStyle = RowBG;
                    CellCount++;
                    ICell cell11 = DyRows.CreateCell(CellCount);
                    cell11.SetCellValue(row.DealerName);
                    cell11.CellStyle = RowBG;
                    CellCount++;
                    ICell cell12 = DyRows.CreateCell(CellCount);
                    cell12.SetCellValue(row.DealerCode);
                    cell12.CellStyle = RowBG;
                    CellCount++;
                    ICell cell13 = DyRows.CreateCell(CellCount);
                    cell13.SetCellValue(row.DealerType);
                    cell13.CellStyle = RowBG;
                    CellCount++;
                    ICell cell14 = DyRows.CreateCell(CellCount);
                    cell14.SetCellValue(row.BranchName);
                    cell14.CellStyle = RowBG;
                    CellCount++;

                    ICell cellST = DyRows.CreateCell(CellCount);
                    cellST.SetCellValue(row.Status);
                    cellST.CellStyle = RowBG;
                    CellCount++;
                    foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                    {
                        ICell cell = DyRows.CreateCell(CellCount);
                        cell.SetCellValue("");
                        if (row.Days.ContainsKey(da.ToString("dd-MMM-yyyy")))
                        {
                            cell.SetCellValue(row.Days[da.ToString("dd-MMM-yyyy")]);
                        }
                        cell.CellStyle = RowBG;
                        CellCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetAttendanceLog_Monthly_Workbook. The query was executed :", ex.ToString(), "GetAttendanceLog_Monthly_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook GetAttendanceLog_Monthly_InOut_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DateTime dt;
                DateTime.TryParse(modal.Month, out dt);

                DateTime baseDate = dt;
                DateTime StartDate = baseDate.AddDays(1 - baseDate.Day);
                DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
                List<Attendance_Log.Monthly_INOUT> result = report.GetAttendance_Log_Monthly_InOutList(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                // Bind Header star
                int HeadCount = 0;
                var r0 = sheet.CreateRow(0);
                for (int i = 0; i <= 17; i++)
                {
                    r0.CreateCell(HeadCount, CellType.String).SetCellValue("");
                    r0.Cells[HeadCount].CellStyle = BGGreen;
                    HeadCount++;
                }
                foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        r0.CreateCell(HeadCount, CellType.String).SetCellValue(da.ToString("dd-MMM-yyyy"));
                        r0.Cells[HeadCount].CellStyle = BGGreen;
                        HeadCount++;
                    }
                }

                HeadCount = 0;
                var r1 = sheet.CreateRow(1);
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("S No.");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Gender");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Phone");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Department");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Designation");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("DOJ");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Role");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Region");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("State");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("City");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Area");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Dealer Type");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Branch Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Status");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                {
                    r1.CreateCell(HeadCount, CellType.String).SetCellValue("Mark");
                    r1.Cells[HeadCount].CellStyle = BGYellow;
                    HeadCount++;
                    r1.CreateCell(HeadCount, CellType.String).SetCellValue("In");
                    r1.Cells[HeadCount].CellStyle = BGYellow;
                    HeadCount++;
                    r1.CreateCell(HeadCount, CellType.String).SetCellValue("Out");
                    r1.Cells[HeadCount].CellStyle = BGYellow;
                    HeadCount++;
                    r1.CreateCell(HeadCount, CellType.String).SetCellValue("Working");
                    r1.Cells[HeadCount].CellStyle = BGYellow;
                    HeadCount++;
                }


                int rowCount = 1, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (Attendance_Log.Monthly_INOUT row in result)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    CellCount = 0;
                    IRow DyRows = sheet.CreateRow(rowCount);
                    ICell cell0 = DyRows.CreateCell(CellCount);
                    cell0.SetCellValue(rowCount);
                    cell0.CellStyle = RowBG;
                    CellCount++;
                    ICell cell2 = DyRows.CreateCell(CellCount);
                    cell2.SetCellValue(row.EMPCode);
                    cell2.CellStyle = RowBG;
                    CellCount++;
                    ICell cell1 = DyRows.CreateCell(CellCount);
                    cell1.SetCellValue(row.EMPName);
                    cell1.CellStyle = RowBG;
                    CellCount++;

                    ICell cell3 = DyRows.CreateCell(CellCount);
                    cell3.SetCellValue(row.Gender);
                    cell3.CellStyle = RowBG;

                    CellCount++;
                    ICell cell4 = DyRows.CreateCell(CellCount);
                    cell4.SetCellValue(row.Phone);
                    cell4.CellStyle = RowBG;

                    CellCount++;
                    ICell cell5 = DyRows.CreateCell(CellCount);
                    cell5.SetCellValue(row.DeptName);
                    cell5.CellStyle = RowBG;
                    CellCount++;
                    ICell cell6 = DyRows.CreateCell(CellCount);
                    cell6.SetCellValue(row.DesignName);
                    cell6.CellStyle = RowBG;
                    CellCount++;
                    ICell cellDO = DyRows.CreateCell(CellCount);
                    cellDO.SetCellValue(row.DOJ);
                    cellDO.CellStyle = RowBG;
                    CellCount++;
                    ICell cellRO = DyRows.CreateCell(CellCount);
                    cellRO.SetCellValue(row.Role);
                    cellRO.CellStyle = RowBG;
                    CellCount++;
                    ICell cell7 = DyRows.CreateCell(CellCount);
                    cell7.SetCellValue(row.Region);
                    cell7.CellStyle = RowBG;
                    CellCount++;
                    ICell cell8 = DyRows.CreateCell(CellCount);
                    cell8.SetCellValue(row.State);
                    cell8.CellStyle = RowBG;
                    CellCount++;
                    ICell cell9 = DyRows.CreateCell(CellCount);
                    cell9.SetCellValue(row.City);
                    cell9.CellStyle = RowBG;
                    CellCount++;
                    ICell cell10 = DyRows.CreateCell(CellCount);
                    cell10.SetCellValue(row.Area);
                    cell10.CellStyle = RowBG;
                    CellCount++;
                    ICell cell11 = DyRows.CreateCell(CellCount);
                    cell11.SetCellValue(row.DealerName);
                    cell11.CellStyle = RowBG;
                    CellCount++;
                    ICell cell12 = DyRows.CreateCell(CellCount);
                    cell12.SetCellValue(row.DealerCode);
                    cell12.CellStyle = RowBG;
                    CellCount++;
                    ICell cell13 = DyRows.CreateCell(CellCount);
                    cell13.SetCellValue(row.DealerType);
                    cell13.CellStyle = RowBG;
                    CellCount++;
                    ICell cell14 = DyRows.CreateCell(CellCount);
                    cell14.SetCellValue(row.BranchName);
                    cell14.CellStyle = RowBG;
                    CellCount++;

                    ICell cellST = DyRows.CreateCell(CellCount);
                    cellST.SetCellValue(row.Status);
                    cellST.CellStyle = RowBG;
                    CellCount++;
                    foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                    {
                        if (row.Days.ContainsKey(da.ToString("dd-MMM-yyyy")))
                        {
                            if (!string.IsNullOrEmpty(row.Days[da.ToString("dd-MMM-yyyy")]))
                            {
                                if (row.Days[da.ToString("dd-MMM-yyyy")].Contains('#'))
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        ICell cell = DyRows.CreateCell(CellCount);
                                        cell.SetCellValue(row.Days[da.ToString("dd-MMM-yyyy")].Split('#')[i]);
                                        cell.CellStyle = RowBG;
                                        CellCount++;
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        ICell cell = DyRows.CreateCell(CellCount);
                                        cell.SetCellValue("");
                                        cell.CellStyle = RowBG;
                                        CellCount++;
                                    }

                                }
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    ICell cell = DyRows.CreateCell(CellCount);
                                    cell.SetCellValue("");
                                    cell.CellStyle = RowBG;
                                    CellCount++;
                                }

                            }


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetAttendanceLog_Monthly_InOut_Workbook. The query was executed :", ex.ToString(), "GetAttendanceLog_Monthly_InOut_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetAttendance_Final_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetAttendance_Final(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] intColomnIndex = { 1 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 11 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                ICellStyle lightBG = workbook.CreateCellStyle();
                lightBG.FillForegroundColor = HSSFColor.LightYellow.Index;
                lightBG.FillPattern = FillPattern.SolidForeground;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (dtRow[dc].ToString().Contains("*"))
                            {
                                // Apply the light background style
                                cell.CellStyle = lightBG;
                            }
                            if (intColomnIndex.Contains(CellCount + 1) || CellCount >= 59)
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTracker_Workbook. The query was executed :", ex.ToString(), "TLTracker_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetAttendance_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetAttendance(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                var lightBG = SetCellColor("LightColor", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] intColomnIndex = { 1 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 26 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            if (dtRow[dc].ToString().Contains("*"))
                            {
                                cell.SetCellValue(dtRow[dc].ToString().Replace("*", ""));
                                cell.CellStyle = lightBG;
                            }
                            else
                            {
                                cell.SetCellValue(dtRow[dc].ToString());
                                cell.CellStyle = RowBG;
                            }

                            if (intColomnIndex.Contains(CellCount + 1) || CellCount >= 59)
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTracker_Workbook. The query was executed :", ex.ToString(), "TLTracker_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public ICellStyle SetCellColor(string Value, IWorkbook workbook)
        {
            var cellBorder = workbook.CreateCellStyle();
            cellBorder.BorderBottom = BorderStyle.Thin;
            cellBorder.BorderLeft = BorderStyle.Thin;
            cellBorder.BorderRight = BorderStyle.Thin;
            cellBorder.BorderTop = BorderStyle.Thin;
            cellBorder.Alignment = HorizontalAlignment.Center;
            cellBorder.VerticalAlignment = VerticalAlignment.Center;
            var Cell = workbook.CreateCellStyle();
            Cell.CloneStyleFrom(cellBorder);

            switch (Value)
            {
                case "BGGreen":
                    Cell.FillPattern = FillPattern.SolidForeground;
                    ((XSSFCellStyle)Cell).SetFillForegroundColor(new XSSFColor(new byte[] { 198, 239, 206 }));
                    break;
                case "BGYellow":
                    Cell.FillPattern = FillPattern.SolidForeground;
                    ((XSSFCellStyle)Cell).SetFillForegroundColor(new XSSFColor(new byte[] { 255, 235, 156 }));
                    break;
                case "BGGray":
                    Cell.FillPattern = FillPattern.SolidForeground;
                    ((XSSFCellStyle)Cell).SetFillForegroundColor(new XSSFColor(new byte[] { 240, 240, 240 }));
                    break;
                case "LightColor":
                    ICellStyle newStyle = workbook.CreateCellStyle();
                    newStyle.CloneStyleFrom(cellBorder);
                    newStyle.FillForegroundColor = HSSFColor.Lime.Index;
                    newStyle.FillPattern = FillPattern.SolidForeground;
                    return newStyle;
                //case "P":
                //    IFont pfont = workbook.CreateFont();
                //    pfont.Color = HSSFColor.Green.Index;
                //    Cell.SetFont(pfont);
                //    break;
                //case "A":
                //    IFont Afont = workbook.CreateFont();
                //    Afont.Color = HSSFColor.Red.Index;
                //    Cell.SetFont(Afont);
                //    break;
                default:
                    break;
            }

            return Cell;
        }


        public ISheet GetTLTrackerSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] StringColomnIndex = { 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 16, 17 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 5 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (!StringColomnIndex.Contains(CellCount + 1))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTrackerSheet. The query was executed :", ex.ToString(), "GetTLTrackerSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public IWorkbook GetTLTracker_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetTLTracker_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] StringColomnIndex = { 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 16, 17 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 5 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (!StringColomnIndex.Contains(CellCount + 1))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTracker_Workbook. The query was executed :", ex.ToString(), "TLTracker_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook CompetitionSummary_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = Common_SPU.GetCompetitionSummary_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] intColomnIndex = { 1 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 13 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            //if (CellCount > 13 || CellCount == 1)
                            //{
                            //    cell.SetCellType(CellType.Numeric);
                            //    float Amount = 0;
                            //    float.TryParse(dtRow[dc].ToString(), out Amount);
                            //    cell.SetCellValue(Amount);
                            //}
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTracker_Workbook. The query was executed :", ex.ToString(), "TLTracker_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook SaleEntryWithCustomerReport_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetSaleEntry_WithCustomer(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] intColomnIndex = { 1, 19, 20 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 13 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (intColomnIndex.Contains(CellCount + 1))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during SaleEntryWithCustomerReport_Workbook. The query was executed :", ex.ToString(), "SaleEntryWithCustomerReport_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook GetTravel_Expenses_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetTravel_Expenses_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                int[] intColomnIndex = { 1, 12, 13, 14, 15, 16, 17 };
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 9 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            cell.CellStyle = RowBG;
                            if (intColomnIndex.Contains(CellCount + 1))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float Amount = 0;
                                float.TryParse(dtRow[dc].ToString(), out Amount);
                                cell.SetCellValue(Amount);
                            }
                            CellCount++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTravel_Expenses_Workbook. The query was executed :", ex.ToString(), "SaleEntryWithCustomerReport_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetTravel_Visit_Report_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetTravel_Visit_Report(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                var r2 = sheet.CreateRow(0);
                string intColomn = "RowNum,Product Rating,Customer Rating,Expense Amt";
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount > 9 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            if (dc.ColumnName.ToUpper().Contains("IMAGE") && !string.IsNullOrEmpty(dtRow[dc.ColumnName].ToString()))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (intColomn.Contains(dc.ColumnName))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    int qty = 0;
                                    int.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTravel_Visit_Report. The query was executed :", ex.ToString(), "GetTravel_Visit_Report()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook CounterDisplayReport_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                List<CounterDisplay.Report> result = report.GetCounterDisplayReport(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                // Bind Header star
                int HeadCount = 0;
                var r1 = sheet.CreateRow(0);
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("S No.");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Date");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Name");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Code");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Brand");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Qty");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Image");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;
                r1.CreateCell(HeadCount, CellType.String).SetCellValue("Remarks");
                r1.Cells[HeadCount].CellStyle = BGGreen;
                HeadCount++;

                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (CounterDisplay.Report row in result)
                {
                    rowCount++;
                    CellCount = 0;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    ICell cell0 = DyRows.CreateCell(CellCount);
                    cell0.SetCellValue(rowCount);
                    cell0.CellStyle = RowBG;
                    CellCount++;
                    ICell cell1 = DyRows.CreateCell(CellCount);
                    cell1.SetCellValue(row.Date);
                    cell1.CellStyle = RowBG;
                    CellCount++;
                    ICell cell2 = DyRows.CreateCell(CellCount);
                    cell2.SetCellValue(row.EMPName);
                    cell2.CellStyle = RowBG;
                    CellCount++;
                    ICell cell3 = DyRows.CreateCell(CellCount);
                    cell3.SetCellValue(row.EMPCode);
                    cell3.CellStyle = RowBG;
                    CellCount++;
                    ICell cellG = DyRows.CreateCell(CellCount);
                    cellG.SetCellValue(row.BrandName);
                    cellG.CellStyle = RowBG;
                    CellCount++;

                    ICell cell5 = DyRows.CreateCell(CellCount);
                    cell5.SetCellType(CellType.Numeric);
                    cell5.SetCellValue(Convert.ToDouble(row.Qty));
                    cell5.CellStyle = RowBG;
                    CellCount++;

                    ICell cell6 = DyRows.CreateCell(CellCount);
                    cell6.SetCellValue("Image Link");
                    XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                    link.Address = ClsCommon.GetWebsiteURL() + "/" + row.AttachmentURL;
                    cell6.Hyperlink = (link);
                    ICellStyle hlink_style = workbook.CreateCellStyle();
                    IFont hlink_font = workbook.CreateFont();
                    hlink_font.Underline = FontUnderlineType.Single;
                    hlink_font.Color = HSSFColor.Blue.Index;
                    hlink_style.SetFont(hlink_font);
                    cell6.CellStyle = (hlink_style);

                    CellCount++;
                    ICell cell4 = DyRows.CreateCell(CellCount);
                    cell4.SetCellValue(row.Remarks);
                    cell4.CellStyle = RowBG;
                    CellCount++;

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during CounterDisplayReport_Workbook. The query was executed :", ex.ToString(), "GetAttendanceLog_Day_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public IWorkbook GetIncentiveCalculator_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetEMP_Incentive(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                var r1 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[1]);
                            r1.Cells[HeadCount].CellStyle = BGYellow;
                        }
                        else
                        {
                            r1.CreateCell(HeadCount, CellType.String).SetCellValue("");
                            r1.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                var r2 = sheet.CreateRow(1);
                HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_") && !item.ColumnName.Contains("Image"))
                    {
                        if (item.ColumnName.Contains("#"))
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Split('#')[0]);
                            r2.Cells[HeadCount].CellStyle = BGYellow;
                        }
                        else
                        {
                            r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                            r2.Cells[HeadCount].CellStyle = BGGreen;
                        }
                        HeadCount++;
                    }
                }

                // Bind header End
                int rowCount = 1, CellCount = 0;
                var RowBG = BGdeafult;
                string intColomn = "RowNum,TotalSalesQty,TotalIncentive";
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_") && !dc.ColumnName.Contains("Image"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            if (dc.ColumnName.Contains("#") || intColomn.Contains(dc.ColumnName))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float qty = 0;
                                float.TryParse(dtRow[dc].ToString(), out qty);
                                cell.SetCellValue(qty);
                            }
                            cell.CellStyle = RowBG;
                            CellCount++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetIncentiveCalculator_Workbook. The query was executed :", ex.ToString(), "GetIncentiveCalculator_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }

        public IWorkbook GetDealerPerformance_Workbook(Tab.Approval modal)
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                DataSet result = report.GetDealerPerformance_Reports(modal);
                var sheet = workbook.CreateSheet("Sheet 1");
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);

                var r1 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Tables[0].Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r1.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r1.Cells[HeadCount].CellStyle = (HeadCount > 8 ? BGYellow : BGGreen);
                        HeadCount++;
                    }
                }



                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                string intColomn = "RowNum,MTDQTY,MTDAMT,lTDQTY,lTDAMT";
                foreach (DataRow dtRow in result.Tables[0].Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Tables[0].Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            if (intColomn.Contains(dc.ColumnName))
                            {
                                cell.SetCellType(CellType.Numeric);
                                float qty = 0;
                                float.TryParse(dtRow[dc].ToString(), out qty);
                                cell.SetCellValue(qty);
                            }
                            cell.CellStyle = RowBG;
                            CellCount++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDealerPerformance_Workbook. The query was executed :", ex.ToString(), "GetDealerPerformance_Workbook()", "Export", "Export", modal.LoginID, "");
            }

            return workbook;
        }


        public ISheet GetDealerSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };

                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {

                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount <= 1 ? BGGreen : BGYellow);
                        HeadCount++;
                    }
                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            ICell cell = DyRows.CreateCell(CellCount);
                            cell.SetCellValue(dtRow[dc].ToString());
                            if (intColomnIndex.Contains(CellCount + 1))
                            {
                                cell.SetCellType(CellType.Numeric);
                                int qty = 0;
                                int.TryParse(dtRow[dc].ToString(), out qty);
                                cell.SetCellValue(qty);
                            }
                            cell.CellStyle = RowBG;
                            CellCount++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDealerSheet. The query was executed :", ex.ToString(), "GetDealerSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public ISheet GetEmployeeSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };

                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {
                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = BGGreen;
                        HeadCount++;
                    }
                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            if (dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (intColomnIndex.Contains(CellCount + 1))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    int qty = 0;
                                    int.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetEmployeeSheet. The query was executed :", ex.ToString(), "GetEmployeeSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public ISheet GetCommonSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };

                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {
                    if (!item.ColumnName.Contains("_"))
                    {

                        r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                        r2.Cells[HeadCount].CellStyle = (HeadCount <= 1 ? BGGreen : BGYellow);
                        HeadCount++;
                    }
                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        if (!dc.ColumnName.Contains("_"))
                        {
                            if (dc.ColumnName.Contains("Image"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Image Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else if (dc.ColumnName.Contains("Resume"))
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue("Resume Link");
                                XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Url);
                                link.Address = ClsCommon.GetWebsiteURL() + "/" + dtRow[dc].ToString();
                                cell.Hyperlink = (link);
                                ICellStyle hlink_style = workbook.CreateCellStyle();
                                IFont hlink_font = workbook.CreateFont();
                                hlink_font.Underline = FontUnderlineType.Single;
                                hlink_font.Color = HSSFColor.Blue.Index;
                                hlink_style.SetFont(hlink_font);
                                cell.CellStyle = (hlink_style);
                                CellCount++;
                            }
                            else
                            {
                                ICell cell = DyRows.CreateCell(CellCount);
                                cell.SetCellValue(dtRow[dc].ToString());
                                if (intColomnIndex.Contains(CellCount + 1))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    int qty = 0;
                                    int.TryParse(dtRow[dc].ToString(), out qty);
                                    cell.SetCellValue(qty);
                                }
                                cell.CellStyle = RowBG;
                                CellCount++;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetCommonSheet. The query was executed :", ex.ToString(), "GetCommonSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public DataSet GetBrandDisplay_Export(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetBrandDisplay_Export", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetBrandDisplay_Export", "spu_GetBrandDisplay_Export", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public DataSet GetOnboarding_Applications_Export(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Approved", Modal.Approved);
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetOnboarding_Applications_Export", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetOnboarding_Applications_Export", "spu_GetOnboarding_Applications_Export", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public DataSet GetEMP_TalentPool_Export(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetEMP_TalentPool_Export", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMP_TalentPool_Export", "GetEMP_TalentPool_Export", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public ISheet GetOnboardingSheet(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                // Bind Header star
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                int[] intColomnIndex = { 1 };

                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                foreach (DataColumn item in result.Columns)
                {


                    r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName);
                    r2.Cells[HeadCount].CellStyle = (HeadCount <= 1 ? BGGreen : BGYellow);
                    HeadCount++;

                }
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {
                        ICell cell = DyRows.CreateCell(CellCount);
                        cell.SetCellValue(dtRow[dc].ToString());
                        if (intColomnIndex.Contains(CellCount + 1))
                        {
                            cell.SetCellType(CellType.Numeric);
                            int qty = 0;
                            int.TryParse(dtRow[dc].ToString(), out qty);
                            cell.SetCellValue(qty);
                        }
                        cell.CellStyle = RowBG;
                        CellCount++;
                    }
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetOnboardingSheet. The query was executed :", ex.ToString(), "GetOnboardingSheet()", "Export", "Export", 0, "");
            }

            return sheet;
        }

        public DataSet GetLogin_Users_Export(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetLogin_Users_Export", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLogin_Users_Export", "GetLogin_Users_Export", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public ISheet GetWorkbookSheet_Shorted(IWorkbook workbook, DataTable result, string Doctype)
        {
            var sheet = workbook.CreateSheet(Doctype);
            try
            {
                var BGGreen = SetCellColor("BGGreen", workbook);
                var BGYellow = SetCellColor("BGYellow", workbook);
                var BGGray = SetCellColor("BGGray", workbook);
                var BGdeafult = SetCellColor("", workbook);
                var r2 = sheet.CreateRow(0);
                int HeadCount = 0;
                // Bind header Start
                foreach (DataColumn item in result.Columns)
                {
                    r2.CreateCell(HeadCount, CellType.String).SetCellValue(item.ColumnName.Replace("@", ""));
                    r2.Cells[HeadCount].CellStyle = (HeadCount > 5 ? BGYellow : BGGreen);
                    HeadCount++;
                }
                // Bind header End
                int rowCount = 0, CellCount = 0;
                var RowBG = BGdeafult;
                foreach (DataRow dtRow in result.Rows)
                {
                    rowCount++;
                    RowBG = (rowCount % 2 == 1 ? BGdeafult : BGGray);
                    IRow DyRows = sheet.CreateRow(rowCount);
                    CellCount = 0;
                    foreach (DataColumn dc in result.Columns)
                    {

                        ICell cell = DyRows.CreateCell(CellCount);
                        cell.SetCellValue(dtRow[dc].ToString());
                        cell.CellStyle = RowBG;
                        if (dc.ColumnName.Contains('@'))
                        {
                            cell.SetCellType(CellType.Numeric);
                            float Amount = 0;
                            float.TryParse(dtRow[dc].ToString(), out Amount);
                            cell.SetCellValue(Amount);
                        }
                        CellCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetWorkbookSheet_Shorted. The query was executed :", ex.ToString(), "GetWorkbookSheet_Shorted()", "Export", "Export", 0, "");
            }

            return sheet;
        }
    }
}
