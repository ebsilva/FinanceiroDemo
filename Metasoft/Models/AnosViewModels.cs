using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class AnosViewModel
    {

        public int CurrentYear = DateTime.Now.Year;
        public int Start { get; set; }
        public int End { get; set; }
        public string DivClass { get; set; }
        public string InputClass { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Mtop { get; set; }

        public string Height { get; set; }
        public bool NoLabel { get; set; }
    }
}