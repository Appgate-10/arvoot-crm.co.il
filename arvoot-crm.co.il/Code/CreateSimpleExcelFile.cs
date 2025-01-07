#define INCLUDE_WEB_FUNCTIONS


using Simplexcel;
using System;
using System.Data;
using System.Diagnostics;
using cp.cappsino.co.Code;
using System.IO;

namespace ControlPanel.HelpersFunctions
{
    //
    //  September 2016
    //  http://www.mikesknowledgebase.com
    //
    //  Note: if you plan to use this in an ASP.Net application, remember to add a reference to "System.Web", and to uncomment
    //  the "INCLUDE_WEB_FUNCTIONS" definition at the top of this file.
    //
    //  Release history
    //  -  Sep 2016: 
    //        Make sure figures with a decimal part are formatted with a full-stop as a decimal point.
    //  -  Feb 2015: 
    //        Needed to replace "Response.End();" with some other code, to make sure the Excel was fully written to the HTTP Response
    //        New ReplaceHexadecimalSymbols() function to prevent hex characters from crashing the export. 
    //        Changed GetExcelColumnName() to cope with more than 702 columns (!)
    //   - Jan 2015: 
    //        Throwing an exception when trying to export a DateTime containing null.
    //        Was missing the function declaration for "CreateExcelDocument(DataSet ds, string filename, System.Web.HttpResponse Response)"
    //        Removed the "Response.End();" from the web version, as recommended in: https://support.microsoft.com/kb/312629/EN-US/?wa=wsignin1.0
    //   - Mar 2014: 
    //        Now writes the Excel data using the OpenXmlWriter classes, which are much more memory efficient.
    //   - Nov 2013: 
    //        Changed "CreateExcelDocument(DataTable dt, string xlsxFilePath)" to remove the DataTable from the DataSet after creating the Excel file.
    //        You can now create an Excel file via a Stream (making it more ASP.Net friendly)
    //   - Jan 2013: Fix: Couldn't open .xlsx files using OLEDB  (was missing "WorkbookStylesPart" part)
    //   - Nov 2012: 
    //        List<>s with Nullable columns weren't be handled properly.
    //        If a value in a numeric column doesn't have any data, don't write anything to the Excel file (previously, it'd write a '0')
    //   - Jul 2012: Fix: Some worksheets weren't exporting their numeric data properly, causing "Excel found unreadable content in '___.xslx'" errors.
    //   - Mar 2012: Fixed issue, where Microsoft.ACE.OLEDB.12.0 wasn't able to connect to the Excel files created using this class.
    //
    //
    //   (c) www.mikesknowledgebase.com 2016 
    //   
    //   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
    //   (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
    //   publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
    //   subject to the following conditions:
    //   
    //   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    //   
    //   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    //   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    //   FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    //   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    //   
    public class CreateSimpleExcelFile
    {
        //public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(ListToDataTable(list));

        //    return CreateExcelDocument(ds, xlsxFilePath);
        //}
        #region HELPER_FUNCTIONS
        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".
  


        #endregion

#if INCLUDE_WEB_FUNCTIONS
        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>True if it was created succesfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataSet ds, string filename, System.Web.HttpResponse Response)
        {

            var sheet = new Worksheet("report");
            //sheet.PageSetup.PrintRepeatRows = 2; // How many rows (starting with the top one)
            // // How many columns (starting with the left one, 0 is default)
            //sheet.PageSetup.Orientation = Orientation.Landscape;

            foreach (DataTable data in ds.Tables)
            {
                sheet.PageSetup.PrintRepeatColumns = data.Columns.Count;
                sheet.PageSetup.PrintRepeatRows = data.Rows.Count;

                
                for(int i=0;i<data.Rows.Count; i++)
                {
               
                    DataRow dataRow = data.Rows[i];
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        String nameCell = convertNumToLetter(j) + (i+1).ToString();
                        String valCell = dataRow[j].ToString();
                       
                       
                        sheet.Cells[nameCell] = valCell;
                        
                            double valCellFloat = 0;
                            int valCellInt = 0;
                            if (int .TryParse(valCell, out  valCellInt))
                            {
                                sheet.Cells[nameCell] = valCellInt;

                            }
                            else
                                if (double.TryParse(valCell, out valCellFloat))
                                {
                                    sheet.Cells[nameCell] = valCellFloat;
                                    //sheet.Cells[nameCell].Format = BuiltInCellFormat.NumberTwoDecimalPlaces;
                                }
                        
                            
                         //  bool isInt = int.TryParse(valCell, out int valCellInt);
                         //  bool isFloat = float.TryParse(valCell, out float valCellFloat);
                         

                       

                        sheet.Cells[nameCell].Border = CellBorder.All;
                        sheet.Cells[nameCell].HorizontalAlignment = HorizontalAlign.Center;
                        sheet.Cells[nameCell].VerticalAlignment = VerticalAlign.Middle;
                      
                    
                     
                      
                    }
                }
            }


           
          
            var workbook = new Workbook();
            workbook.Add(sheet);
            using (var memoryStream = new MemoryStream())
            {
                workbook.Save(memoryStream);

                memoryStream.Flush();
                memoryStream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.ContentType = "application/vnd.ms-excel";

                Response.AppendHeader("content-length", memoryStream.Length.ToString());
                byte[] data1 = new byte[memoryStream.Length];
                memoryStream.Read(data1, 0, data1.Length);
                memoryStream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();

                //  Feb2015: Needed to replace "Response.End();" with the following 3 lines, to make sure the Excel was fully written to the Response
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.SuppressContent = true;
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

            return true;
        }

        public static bool CreateExcelDocument2(DataSet ds, string filename, System.Web.HttpResponse Response)
        {

            var sheet = new Worksheet("report");
            //sheet.PageSetup.PrintRepeatRows = 2; // How many rows (starting with the top one)
            // // How many columns (starting with the left one, 0 is default)
            //sheet.PageSetup.Orientation = Orientation.Landscape;

            foreach (DataTable data in ds.Tables)
            {
                int numOfColumns = data.Columns.Count - 3;
                sheet.PageSetup.PrintRepeatColumns = numOfColumns;
                sheet.PageSetup.PrintRepeatRows = data.Rows.Count;


                //   data.Columns.

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    bool bold = false;
                    bool boldUntilC = false;

                    DataRow dataRow = data.Rows[i];
                    for (int j = 0; j < numOfColumns; j++)
                    {
                        String nameCell = convertNumToLetter(j) + (i + 1).ToString();
                        String valCell = dataRow[j].ToString();
                        if (valCell.Equals("שם המזמין") || valCell.Equals("אשראי") ) bold = true;
                      //  if (valCell.Equals("הורדת עמלות") || valCell.Equals("הורדת דמי משלוח") || valCell.Equals("מזומן שהתקבל בבית העסק") || valCell.Equals("סהכ לתשלום")) boldUntilC = true;

                        //try
                        //{
                        //    int valCellInt = Int32.Parse(valCell);
                        //    sheet.Cells[nameCell] = valCellInt;

                        //}
                        //catch (Exeption e)
                        //{
                        //    try
                        //    {
                        //        DateTime valCallDate = DateTime.Parse(valCell);
                        //        sheet.Cells[nameCell] = valCallDate;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        sheet.Cells[nameCell] = valCell;

                        //    }
                        //}

                        sheet.Cells[nameCell] = valCell;
                        if (!valCell.StartsWith("0") || valCell.Length == 1)
                        {
                            double valCellFloat = 0;
                            int valCellInt = 0;

                            if (int.TryParse(valCell, out  valCellInt))
                            {
                                sheet.Cells[nameCell] = valCellInt;

                            }
                            else
                                if (double.TryParse(valCell, out valCellFloat))
                                {
                                    sheet.Cells[nameCell] = valCellFloat;
                                    //sheet.Cells[nameCell].Format = BuiltInCellFormat.NumberTwoDecimalPlaces;
                                }
                        }
                        sheet.Cells[nameCell].Border = CellBorder.All;
                        sheet.Cells[nameCell].HorizontalAlignment = HorizontalAlign.Center;
                        sheet.Cells[nameCell].VerticalAlignment = VerticalAlign.Middle;
                        sheet.Cells[nameCell].Bold = bold;

                        if (boldUntilC && j == 2)
                            sheet.Cells[nameCell].Bold = boldUntilC;
                        bool isCallCenter = false;
                        try
                        {
                            if (!dataRow[14].ToString().Equals(""))
                                isCallCenter = bool.Parse(dataRow[14].ToString());
                        }
                        catch (Exeption e)
                        {

                        }
                        if (j == 11 && isCallCenter)
                        {
                            sheet.Cells[nameCell].TextColor = Color.FromArgb(100, 255, 192, 0);
                            sheet.Cells[nameCell].Bold = true;

                        }
                        bool IsOneTime = false;
                        try
                        {
                            if (!dataRow[15].ToString().Equals(""))
                                IsOneTime = bool.Parse(dataRow[15].ToString());
                        }
                        catch (Exeption e)
                        {

                        }
                        if (j == 11 && IsOneTime)
                        {
                            sheet.Cells[nameCell].TextColor = Color.FromArgb(100, 153, 51, 255);
                            sheet.Cells[nameCell].Bold = true;

                        }
                        bool IsFromLink = false;
                        try
                        {
                            if (!dataRow[16].ToString().Equals(""))
                                IsOneTime = bool.Parse(dataRow[16].ToString());
                        }
                        catch (Exeption e)
                        {

                        }
                        if (j == 9 && IsFromLink)
                        {
                            sheet.Cells[nameCell].TextColor = Color.FromArgb(100, 198, 224, 180);
                            sheet.Cells[nameCell].Bold = true;

                        }
                    }
                }
            }




            var workbook = new Workbook();
            workbook.Add(sheet);
            using (var memoryStream = new MemoryStream())
            {
                workbook.Save(memoryStream);

                memoryStream.Flush();
                memoryStream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.ContentType = "application/vnd.ms-excel";

                Response.AppendHeader("content-length", memoryStream.Length.ToString());
                byte[] data1 = new byte[memoryStream.Length];
                memoryStream.Read(data1, 0, data1.Length);
                memoryStream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();

                //  Feb2015: Needed to replace "Response.End();" with the following 3 lines, to make sure the Excel was fully written to the Response
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.SuppressContent = true;
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

            return true;
        }

        private static string convertNumToLetter(int num)
        {
            switch (num){
               case 0: return "A";
               case  1: return "B";
               case  2: return "C";
               case  3: return "D";
               case  4: return "E";
               case  5: return "F";
               case  6: return "G";
               case  7: return "H";
               case  8: return "I";
               case  9: return "J";
               case 10: return "K";
               case 11: return "L";
               case 12: return "M";

            }
            return "N";
        }
   
        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
#endif      //  End of "INCLUDE_WEB_FUNCTIONS" section

        /// <summary>
        /// Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
 

      /*  private static void WriteExcelFileEPPlus(DataSet ds) {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Create a custom cell style with a yellow fill and bold font
                ExcelNamedStyleXml customStyle = excelPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");
                customStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
              //  customStyle.Style.Fill.BackgroundColor.SetColor(Colors.);
                customStyle.Style.Font.Bold = true;

                // Load data from a DataSet
                DataSet dataSet = new DataSet();
                dataSet.ReadXml("data.xml");
                DataTable dataTable = dataSet.Tables[0];

                // Write column headers
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                // Write data rows
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dataTable.Rows[i][j];

                        // Apply custom style to cells that contain the word "Total"
                        if (dataTable.Rows[i][j].ToString().Contains("Total"))
                        {
                            worksheet.Cells[i + 2, j + 1].StyleName = customStyle.Name;
                        }
                    }
                }

                // Save the Excel file
                FileInfo excelFile = new FileInfo("output.xlsx");
                excelPackage.SaveAs(excelFile);
            }

        }*/




        //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
        public static string GetExcelColumnName(int columnIndex)
        {
            //  eg  (0) should return "A"
            //      (1) should return "B"
            //      (25) should return "Z"
            //      (26) should return "AA"
            //      (27) should return "AB"
            //      ..etc..
            char firstChar;
            char secondChar;
            char thirdChar;

            if (columnIndex < 26)
            {
                return ((char)('A' + columnIndex)).ToString();
            }

            if (columnIndex < 702)
            {
                firstChar = (char)('A' + (columnIndex / 26) - 1);
                secondChar = (char)('A' + (columnIndex % 26));

                return string.Format("{0}{1}", firstChar, secondChar);
            }

            int firstInt = columnIndex / 676;
            int secondInt = (columnIndex % 676) / 26;
            if (secondInt == 0)
            {
                secondInt = 26;
                firstInt = firstInt - 1;
            }
            int thirdInt = (columnIndex % 26);

            firstChar = (char)('A' + firstInt - 1);
            secondChar = (char)('A' + secondInt - 1);
            thirdChar = (char)('A' + thirdInt);

            return string.Format("{0}{1}{2}", firstChar, secondChar, thirdChar);
        }

    }
}
