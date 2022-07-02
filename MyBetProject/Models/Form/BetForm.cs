using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBetProject.Models.Form
{
    public class BetForm
    {
        public int ID { get; set; }
        public string League { get; set; }
        public string Team { get; set; }
        public string BetType { get; set; }
        public DateTime BetDate { get; set; }
        public DateTime BetTime { get; set; }
        public string BetID { get; set; }
        public string BetPlace { get; set; }
        public DateTime CreateDate { get; set; }

        public string BetDateString { get; set; }

        public string strDate { get; set; }
        public string strData { get; set; }
    }
}