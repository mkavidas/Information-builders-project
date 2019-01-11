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
            return View(LoadData());
        }
        //This function loads data from sql server into a list of SalesViewDetails
        private List<SalesViewDetails> LoadData()
        {
            List<SalesViewDetails> sales = new List<SalesViewDetails>();

            String sql = "SELECT * FROM simple_sales_view";

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
                DateTime cur = (DateTime)nwReader["TIME_DATE"];
                sales.Add(new SalesViewDetails
                {
                    TIME_DATE = cur.ToString("yyyy/MM/dd "),
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
    }
}