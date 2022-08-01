using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antheap.Model.APIwl.Data
{
    internal class Exception
    {
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        //[DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }
    }
}
