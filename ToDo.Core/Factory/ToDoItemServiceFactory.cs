using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.Core.Service;
using ToDo.DA.Mapper;

namespace ToDo.Core.Service.Factory
{
    public class ToDoItemServiceFactory
    {
        public ToDoItemServiceFactory()
        {}

        public IToDoItemService CreateInstance()
        {
            // Instantiate to do item service with an MsSql mapper - hard coded for now
            MapperFactory mapperFactory = new MapperFactory(DA.Enumerations.DataStores.MsSql);

            IToDoItemService tis = new ToDoItemService(mapperFactory.GetToDoItemMapper());
            return tis;
        }
    }
}
