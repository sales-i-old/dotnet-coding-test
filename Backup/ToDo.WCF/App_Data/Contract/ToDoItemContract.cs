using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ToDo.WCF.Contract
{
    [DataContract]
    public class ToDoItemContract
    {
        [DataMember]
        public string Id
        { get; set; }

        [DataMember]
        public string Title
        { get; set; }

        [DataMember]
        public string Description
        { get; set; }

        [DataMember]
        public bool Complete
        { get; set; }
    }
}