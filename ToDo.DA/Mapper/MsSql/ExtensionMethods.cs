using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ToDo.DA.Mapper.MsSql
{
    public static class ExtensionMethods
    {
        public static Guid GetNullableGuid(this IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
            {
                return Guid.Empty;
            }
            else
            {
                return reader.GetGuid(ordinal);
            }
        }
    }
}
