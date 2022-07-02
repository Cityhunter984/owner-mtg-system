using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBetProject.Models.Form
{
    public class TestForm
    {
        public string date { get; set; }
        public string docType { get; set; }
        public string attetion { get; set; }
        public string customer { get; set; }
        public string phone { get; set; }
        public string quoteNo { get; set; }
        public string carModel { get; set; }
        public string year { get; set; }
        public string plateNo { get; set; }
        public string vin { get; set; }
        public string mileage { get; set; }
        public float VAT { get; set; }
        public float discount { get; set; }
        public float subTotal { get; set; }
        public float grandTotal { get; set; }
    }
}