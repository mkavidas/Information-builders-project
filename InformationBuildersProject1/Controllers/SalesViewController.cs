using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationBuildersProject1.Models;

namespace InformationBuildersProject1.Controllers
{
    public class SalesViewController : Controller
    {
        // GET: SalesView
        public ActionResult Index()
        {
            return View();
        }
        //This function loads data from sql server into a list of SalesViewDetails
        private List<SalesViewDetails> LoadData()
        {
            List<SalesViewDetails> sales = new List<SalesViewDetails>();

            string sql = "SELECT * FROM simple_sales_view";

            string strCon = System.Web
                                  .Configuration
                                  .WebConfigurationManager
                                  .ConnectionStrings["wf_retail"].ConnectionString;

            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                DateTime cur = (DateTime)nwReader["TIME_DATE"]; //Get the time here so we can convert to string
                sales.Add(new SalesViewDetails
                {
                    TIME_DATE = cur.ToString("yyyy/MM/dd "), //Convert time to formatted string
                    STATE_PROV_CODE_ISO_3166_2 = (string)nwReader["STATE_PROV_CODE_ISO_3166_2"],
                    STORE_TYPE = (string)nwReader["STORE_TYPE"],
                    BRANDTYPE = (string)nwReader["BRANDTYPE"],
                    BRAND = (string)nwReader["BRAND"],
                    MODEL = (string)nwReader["MODEL"],
                    QUANTITY_SOLD = (int)nwReader["QUANTITY_SOLD"],
                    REVENUE_US = Convert.ToDouble(nwReader["REVENUE_US"])
                });
            }
            nwReader.Close();
            conn.Close();


            return sales;
        }
        //The following implementation was made by following this guide (with some modifications): https://www.c-sharpcorner.com/article/asp-net-mvc5-datatables-plugin-server-side-integration/
        public ActionResult GetData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Initialization.

                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                // Loading.
                List<SalesViewDetails> data = this.LoadData();
                
                // Total record count.
                int totalRecords = data.Count;

                // Verification.
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.TIME_DATE.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.STATE_PROV_CODE_ISO_3166_2.ToLower().Contains(search.ToLower()) ||
                                           p.STORE_TYPE.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.BRANDTYPE.ToLower().Contains(search.ToLower()) ||
                                           p.BRAND.ToLower().Contains(search.ToLower()) ||
                                           p.MODEL.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.QUANTITY_SOLD.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.REVENUE_US.ToString().ToLower().Contains(search.ToLower())
                                           ).ToList();
                }

                // Sorting.
                data = this.SortByColumnWithOrder(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                // Info
                Console.Write(ex);
                //Make sure to return empty data on exception
                return Json(new { draw = Convert.ToInt32(0), recordsTotal = 0, recordsFiltered = 0, data = new List<SalesViewDetails>() }, JsonRequestBehavior.AllowGet);
            }

            // Return info.
            return result;
        }


        
        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        private List<SalesViewDetails> SortByColumnWithOrder(string order, string orderDir, List<SalesViewDetails> data)
        {
            // Initialization.
            List<SalesViewDetails> lst = new List<SalesViewDetails>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.TIME_DATE).ToList()
                                                                                                 : data.OrderBy(p => p.TIME_DATE).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.STATE_PROV_CODE_ISO_3166_2).ToList()
                                                                                                 : data.OrderBy(p => p.STATE_PROV_CODE_ISO_3166_2).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.STORE_TYPE).ToList()
                                                                                                 : data.OrderBy(p => p.STORE_TYPE).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.BRANDTYPE).ToList()
                                                                                                 : data.OrderBy(p => p.BRANDTYPE).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.BRAND).ToList()
                                                                                                   : data.OrderBy(p => p.BRAND).ToList();
                        break;

                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MODEL).ToList()
                                                                                                 : data.OrderBy(p => p.MODEL).ToList();
                        break;

                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.QUANTITY_SOLD).ToList()
                                                                                                 : data.OrderBy(p => p.QUANTITY_SOLD).ToList();
                        break;

                    case "7":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.REVENUE_US).ToList()
                                                                                                 : data.OrderByDescending(p => p.REVENUE_US).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.TIME_DATE).ToList()
                                                                                                 : data.OrderByDescending(p => p.TIME_DATE).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }
    }
}