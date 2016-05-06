using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo.DA.Mapper
{
    public class MapperFactory
    {
        private Enumerations.DataStores _dataStore; 

        public MapperFactory(Enumerations.DataStores dataStoreType)
        {
            _dataStore = dataStoreType;
        }

        public IToDoItemMapper GetToDoItemMapper()
        {
            switch (_dataStore)
            { 
                default:
                case Enumerations.DataStores.MsSql:
                    return new MsSql.MsSqlToDoItemMapper();
            }
        }
    }
}
