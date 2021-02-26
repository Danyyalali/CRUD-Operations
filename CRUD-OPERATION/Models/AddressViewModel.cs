using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        
    }
}