using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyBetProject.Models.Form;
using System.Globalization;
using MyBetProject.Models.Management;

using MODEL;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace MyBetProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult FIEContMgm()
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<DeliveryStatusForm> lst = FIEContainerManagement.getAllDeliveryStatus();
                ViewData[CONSTANT.listGetAllDeliveryStatus] = lst;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult User()
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<UserPositionForm> lstGetAllPosition = UserManagement.getAllPosition();
                ViewData[CONSTANT.getAllPosition] = lstGetAllPosition;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult getSave(BetForm f)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                int result = 0;

                List<string> lst = f.strData.Split(new string[] { "\n\n" }, StringSplitOptions.None).ToList();

                List<string> lstBetID = lst[lst.Count() - 1].Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                string betID = lstBetID[0].Split(':')[1];
                string betPlace = lstBetID[1].Split(new string[] { ": " }, StringSplitOptions.None)[1];

                for (int i = 0; i <= lst.Count() - 2; i++)
                {
                    List<string> rowData = lst[i].Split(new string[] { "\n" }, StringSplitOptions.None).ToList();

                    BetForm b = new BetForm();
                    b.League = rowData[0];
                    b.Team = rowData[1];
                    b.BetType = rowData[3].Split(',')[0];

                    List<string> temp = rowData[3].Split(',')[1].Split(' ').ToList();
                    int year = DateTime.Now.Year;
                    string date = temp[0] + "/" + temp[2] + "/" + year + " " + temp[3];
                    b.BetDate = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    b.BetTime = b.BetDate;
                    b.BetID = betID;
                    b.BetPlace = betPlace;
                    b.CreateDate = DateTime.Now;

                    result = BetManagement.addBetData(b);
                }

                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getSearchData(BetForm f)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<BetForm> lst = new List<BetForm>();
                DateTime searchDate = DateTime.ParseExact(f.strDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                lst = BetManagement.getBetDataByDate(searchDate);
                return Json(new { result = lst }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddContInfo(FIEContainerForm frm)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                FIEContainerManagement.addFIEContainerInfo(frm);
                List<FIEContainerForm> result = FIEContainerManagement.getFIEContainerInfoByCreateDate(DateTime.Now);
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getSearchByDate(FIEContainerSearchForm frm)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<FIEContainerForm> lst = FIEContainerManagement.getSearchDataByDate(frm);

                return Json(new { result = lst }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getContInfoByID(string ID)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                FIEContainerForm f = FIEContainerManagement.getContInfoByID(Convert.ToInt32(ID));

                return Json(new { result = f }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult updateContInfo(FIEContainerForm frm)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                FIEContainerManagement.getUpdateContInfo(frm);

                FIEContainerSearchForm f = new FIEContainerSearchForm();
                f.dateType = frm.dateType;
                f.searchDate = frm.searchDate;
                f.searchType = frm.searchType;
                f.searchData = frm.searchData;

                List<FIEContainerForm> lst = FIEContainerManagement.getSearchDataByDate(f);

                return Json(new { result = lst }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getAllUser()
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<UserForm> result = UserManagement.getAllUser();

                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getUserByID(string userID)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                int id = Convert.ToInt32(userID);
                UserForm result = UserManagement.getUserByID(id);

                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getDeleteUser(string userID)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                int id = Convert.ToInt32(userID);
                bool result = UserManagement.getDeleteUserByID(id);

                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getAddNewUser(UserForm frm)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                bool result = UserManagement.getAddNewUser(frm);

                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                return View();
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getExportToExcel(FIEContainerSearchForm frm)
        {
            if (Request.Cookies[CONSTANT.cookieLogin] != null)
            {
                List<FIEContainerForm> lst = FIEContainerManagement.getSearchDataByDate(frm);



                //var products = new System.Data.DataTable("teste");
                //products.Columns.Add("col1", typeof(int));
                //products.Columns.Add("col2", typeof(string));

                //products.Rows.Add(1, "product 1");
                //products.Rows.Add(2, "product 2");
                //products.Rows.Add(3, "product 3");
                //products.Rows.Add(4, "product 4");
                //products.Rows.Add(5, "product 5");
                //products.Rows.Add(6, "product 6");
                //products.Rows.Add(7, "product 7");


                //var grid = new GridView();
                //grid.DataSource = products;
                //grid.DataBind();

                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
                //Response.ContentType = "application/ms-excel";

                //Response.Charset = "";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);

                //grid.RenderControl(htw);

                //Response.Output.Write(sw.ToString());
                //Response.Flush();
                //Response.End();

                TestForm test = new TestForm();
                ExportExcel(test);

                return Json(new { result = lst }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        private void ExportExcel(TestForm doc)
        {
            //string path = System.Configuration.ConfigurationSettings.AppSettings["ExcelTemplatePath"];
            string path = System.Configuration.ConfigurationManager.AppSettings["ExcelTemplatePath"];
            Workbook wb;
            Worksheet ws;

            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();


            wb = xcelApp.Application.Workbooks.Open(path);
            ws = wb.Worksheets["Sheet1"];

            //add data to Cells of Excel
            ws.Cells[9, 1] = doc.docType;
            ws.Cells[11, 4] = doc.quoteNo;
            ws.Cells[12, 4] = doc.carModel;
            ws.Cells[13, 4] = doc.year;
            ws.Cells[14, 4] = doc.plateNo;
            ws.Cells[15, 4] = doc.vin;
            ws.Cells[16, 4] = doc.mileage;

            ws.Cells[11, 17] = doc.attetion;
            ws.Cells[12, 17] = doc.customer;
            ws.Cells[13, 17] = doc.phone;

            //add data to description in Excel
            int firstRow = 19;
            int i = 1;
            //foreach (ProductForm p in doc.lstDescription)
            //{
            //    ws.Rows[firstRow].Insert();
            //    ws.Cells[firstRow, "A"].value = i;
            //    ws.Cells[firstRow, "B"].value = p.PRODUCT_NAME;
            //    ws.Cells[firstRow, "L"].value = p.status;
            //    ws.Cells[firstRow, "O"].value = p.qty;
            //    ws.Cells[firstRow, "Q"].value = p.unitPrice;
            //    ws.Cells[firstRow, "T"].value = p.total;

            //    var range1 = ws.Range["B" + firstRow + ":K" + firstRow];
            //    var range2 = ws.Range["L" + firstRow + ":N" + firstRow];
            //    var range3 = ws.Range["O" + firstRow + ":P" + firstRow];
            //    var range4 = ws.Range["Q" + firstRow + ":S" + firstRow];
            //    var range5 = ws.Range["T" + firstRow + ":V" + firstRow];
            //    var range6 = ws.Range["W" + firstRow + ":Y" + firstRow];

            //    //merge cells
            //    range1.Merge();
            //    range2.Merge();
            //    range3.Merge();
            //    range4.Merge();
            //    range5.Merge();
            //    range6.Merge();

            //    //add border
            //    //ws.Cells[firstRow, "A"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range1.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range2.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range3.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range4.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range5.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            //    //range6.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //    //add cells style
            //    //ws.Cells[firstRow, "A"].Interior.ColorIndex = 0;
            //    //range1.Interior.ColorIndex = 0;
            //    //range2.Interior.ColorIndex = 0;
            //    //range3.Interior.ColorIndex = 0;
            //    //range4.Interior.ColorIndex = 0;
            //    //range5.Interior.ColorIndex = 0;
            //    //range6.Interior.ColorIndex = 0;

            //    //font style 
            //    //ws.Cells[firstRow, "A"].Cells.Font.Name = "Cambria";
            //    //ws.Cells[firstRow, "A"].Cells.Font.Size = 10;
            //    //ws.Cells[firstRow, "A"].Cells.Font.Bold = false;

            //    //range1.Cells.Font.Name = "Khmer OS System";
            //    //range1.Cells.Font.Size = 9;
            //    //range1.Cells.Font.Bold = false;
            //    //range1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //    //range2.Cells.Font.Name = "Cambria";
            //    //range2.Cells.Font.Size = 10;
            //    //range2.Cells.Font.Bold = false;

            //    //range3.Cells.Font.Name = "Cambria";
            //    //range3.Cells.Font.Size = 10;
            //    //range3.Cells.Font.Bold = false;

            //    //range4.Cells.Font.Name = "Cambria";
            //    //range4.Cells.Font.Size = 10;
            //    //range4.Cells.Font.Bold = false;
            //    //range4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            //    ws.Cells[firstRow, "Q"].Cells.Numberformat = "$ #,##0.00";

            //    //range5.Cells.Font.Name = "Cambria";
            //    //range5.Cells.Font.Size = 10;
            //    //range5.Cells.Font.Bold = false;
            //    //range5.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            //    ws.Cells[firstRow, "T"].Cells.Numberformat = "$ #,##0.00";

            //    i++;
            //    firstRow++;
            //}


            var range = ws.Range["A19:Y" + (firstRow - 1)];
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Interior.ColorIndex = 0;
            range.Cells.Font.Name = "Cambria";
            range.Cells.Font.Size = 10;
            range.Cells.Font.Bold = false;

            var rangeDes = ws.Range["B19:K" + (firstRow - 1)];
            rangeDes.Cells.Font.Name = "Khmer OS System";
            rangeDes.Cells.Font.Size = 9;
            rangeDes.Cells.Font.Bold = false;
            rangeDes.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            var rangeAccounting = ws.Range["Q19:V" + (firstRow - 1)];
            rangeAccounting.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            ws.Cells[firstRow, "T"].value = doc.subTotal;
            ws.Cells[firstRow, "T"].Cells.Numberformat = "$ #,##0.00";
            ws.Cells[firstRow + 1, "T"].value = doc.VAT;
            ws.Cells[firstRow + 1, "C"].value = doc.date;
            ws.Cells[firstRow + 2, "T"].value = doc.discount;
            ws.Cells[firstRow + 3, "T"].value = doc.grandTotal;
            ws.Cells[firstRow + 3, "T"].Cells.Numberformat = "$ #,##0.00";

            xcelApp.Visible = true;
        }
    }
}