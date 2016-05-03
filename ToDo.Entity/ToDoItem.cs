using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo.Entity
{
    public class ToDoItem : IToDoItem
    {
        private string _id;
        private string _parentId;
        private string _title;
        private string _description;
        private bool _complete;

        public string ParentId
        {
            // Would have used an auto prop here. But just to keep the coding style the same will use backing field.
            get { return _parentId; }
            set { _parentId = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
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
