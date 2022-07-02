using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBetProject.Models.Form
{
    public class FIEContainerForm
    {
        public int ID { get; set; }
        public string ArrivePlace { get; set; }
        public DateTime ArriveDate { get; set; }
        public string ArriveDateStr { get; set; }
        public string VNTruck { get; set; }
        public string ContNo { get; set; }
        public int ContSize { get; set; }
        public bool PKL { get; set; }
        public string PKLStr { get; set; }
        public string ContOwner { get; set; }
        public string GoodsType { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryDateStr { get; set; }
        public string DeliveryPlace { get; set; }
        public int DeliveryStatus { get; set; }
        public string DeliveryStatusStr { get; set; }
        public string ThridParty { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }

        public int dateType { get; set; }
        public DateTime searchDate { get; set; }
        public string color { get; set; }
        public string DeliveryStatusName { get; set; }

        public int searchType { get; set; }
        public string searchData { get; set; }
    }
}