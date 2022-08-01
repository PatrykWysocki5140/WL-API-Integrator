using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antheap.Model.APIwl.Data
{
    internal class EntityItem
    {
        //[DataMember(Name = "subject", EmitDefaultValue = false)]
        public Entity Subject { get; set; }

        /// <summary>
        /// Gets or Sets RequestId
        /// </summary>
       // [DataMember(Name = "requestId", EmitDefaultValue = false)]
        public string RequestId { get; set; }
    }
}
