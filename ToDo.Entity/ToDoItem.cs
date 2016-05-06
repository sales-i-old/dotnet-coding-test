using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo.Entity
{
    public class ToDoItem : IToDoItem
    {
        private string _id;
        private string _title;
        private string _description;
        private bool _complete;
        private string _dependentOnId;



        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DependentOnId
        {
            get { return _dependentOnId; }
            set { _dependentOnId = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }
    }
}
