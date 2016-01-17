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
        // ANDREI: added parent task properties
        private string _parentTaskId;
        private string _parentTaskTitle;


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

        // ANDREI: set parent task id property
        public string ParentTaskId
        {
            get { return _parentTaskId; }
            set { _parentTaskId = value; }
        }

        public string ParentTaskTitle
        {
            get { return _parentTaskTitle; }
            set { _parentTaskTitle = value; }
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
