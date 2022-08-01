using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Antheap.Model.APIwl.Data
{
  //  [DataContract]
    internal class EntityResponse
    {
       // [DataMember(Name = "exception", EmitDefaultValue = false)]
        public Exception Exception { get; set; }

        
       // [DataMember(Name = "result", EmitDefaultValue = false)]
        public EntityItem Result { get; set; }
    }
}
