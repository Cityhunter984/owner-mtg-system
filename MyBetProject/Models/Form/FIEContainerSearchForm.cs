using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBetProject.Models.Form
{
    public class FIEContainerSearchForm
    {
        public int dateType { get; set; }
        public DateTime searchDate { get; set; }
        public int searchType { get; set; }
        public string searchData { get; set; }
    }
}