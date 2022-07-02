using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBetProject.Models.Form
{
    public class UserForm
    {
        public string UserID{get;set;}
        public string UserName{get;set;}
        public string Pass{get;set;}
        public int PositionID{get;set;}
        public int ID { get; set; }
        public string Position { get; set; }
    }
}