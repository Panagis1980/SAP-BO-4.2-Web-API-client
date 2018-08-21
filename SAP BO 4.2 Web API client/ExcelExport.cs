using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAP_BO_4._2_Web_API_client
{
    class ExcelExport
    {
        private SAPDocumentList DocList { get; set; }
        private Workbook xlWorkBook;
        private Worksheet xlWorkSheet;
        private object misValue = System.Reflection.Missing.Value;

        public ExcelExport(SAPDocumentList docList)
        {
            DocList = docList;
        }

        public ExcelExport()
        {
        }

        public void GenerateExcel(string FileName) {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[1, 3] = "Path";
            xlWorkSheet.Cells[1, 4] = "Updated";
            int i = 2;
            foreach(SAPDocument doc in this.DocList)
            {
                xlWorkSheet.Cells[i, 1] = doc.SI_ID;
                xlWorkSheet.Cells[i, 2] = doc.SI_NAME;
                xlWorkSheet.Cells[i, 3] = doc.SI_PATH;
                xlWorkSheet.Cells[i, 4] = doc.SI_UPDATE_TS;
                i++;
            }

            xlWorkBook.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file "+ FileName);
        }
    }
}
