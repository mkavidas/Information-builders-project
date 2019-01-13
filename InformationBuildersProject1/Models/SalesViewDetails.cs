using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationBuildersProject1.Models
{
    public class SalesViewDetails
    {
        public string TIME_DATE { get; set; }
        public string STATE_PROV_CODE_ISO_3166_2 { get; set; }

        public string STORE_TYPE { get; set; }
        public string BRANDTYPE { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public int QUANTITY_SOLD { get; set; }
        public string REVENUE_US { get; set; }
        public double _REVENUE_US { get; set; }
    }
}