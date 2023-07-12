using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Matoapp.Infrastructure.Extensions
{
    public class DataExtension
    {
        private static DataColumn[] CreateTableColumn(Type type)
        {
            var column = new List<DataColumn>();

            var propertys = type.GetProperties();
            foreach (var prop in propertys)
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(DataMappingIgnoreAttribute)))
                {
                    Type pType = prop.PropertyType;

                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        pType = prop.PropertyType.GetGenericArguments()[0];
                    }

                    var map = new DataColumn(prop.Name, pType);
                    column.Add(map);
                }
            }
            return column.ToArray();
        }

        //public static DataTable ToDateTable<T>(this List<T> entitys)
        //{
        //    var helper = IocManager.Instance.Resolve<EntityHelper>();
        //    var dataTable = new DataTable();
        //    var column = CreateTableColumn(typeof(T));
        //    dataTable.Columns.AddRange(column);
        //    foreach (var entity in entitys)
        //    {
        //        helper.CheckAndSetValue(entity);
        //        var row = dataTable.NewRow();
        //        SetRowValue(entity, ref row);
        //        dataTable.Rows.Add(row);
        //    }

        //    return dataTable;
        //}

        public static List<SqlBulkCopyColumnMapping> CreateSqlBulkColumnMapping(Type type)
        {
            var mapping = new List<SqlBulkCopyColumnMapping>();

            var propertys = type.GetProperties();
            foreach (var prop in propertys)
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(DataMappingIgnoreAttribute)))
                {
                    var map = new SqlBulkCopyColumnMapping(prop.Name, prop.Name);
                    mapping.Add(map);
                }
            }
            return mapping;
        }
    }

    /// <summary>
    /// 忽略数据映射属性
    /// </summary>
    public class DataMappingIgnoreAttribute : Attribute
    {
        public DataMappingIgnoreAttribute()
        {
        }
    }
}
