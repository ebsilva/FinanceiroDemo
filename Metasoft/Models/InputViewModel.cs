using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metasoft.Models
{
    public class InputViewModel
    {
        public string DivClass { get; set; }
        public string InputClass { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Mtop { get; set; }
        public string Value { get; set; }
        public bool  ValueBool { get; set; }
        public string  Height { get; set; }
        public string  StaticOptions { get; set; }
        public bool NoLabel { get; set; }
        public bool ReadOnly { get; set; }
    }
}