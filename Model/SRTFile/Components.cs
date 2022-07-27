using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antheap.Model.SRTFile
{
    internal class Component
    {
        public long Count { get; set; }
        public string TextLineOne { get; set; }
        public string TextLineTwo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
