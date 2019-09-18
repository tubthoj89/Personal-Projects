using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdVM
    {
        public int dvdId { get; set; }
        public string title { get; set; }
        public string director{ get; set; }
        public string rating{ get; set; }
        public int realeaseYear { get; set; }
        public string notes { get; set; }
    }
}