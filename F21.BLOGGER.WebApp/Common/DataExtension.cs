using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace F21.BLOGGER.WebApp.Common
{
    public static class DataExtension
    {
        /// <summary>
        /// UserDefinedTable을 사용하기 위해 Generic 개체를 DataTable로 변환한다.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="data">Parameter 대상 개체</param>
        /// <returns>Generic 대응 DataTable 개체</returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            var table = new DataTable();
            var properties = TypeDescriptor.GetProperties(typeof(T));
            typeof(T).GetCustomAttributes(false).Any(x => x is XmlIgnoreAttribute);

            foreach (PropertyDescriptor prop in properties)
            {
                if (!prop.PropertyType.IsGenericType)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    if (!prop.PropertyType.IsGenericType)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// UserDefinedTable을 사용하기 위해 int 배열 개체를 DataTable로 변환한다.
        /// </summary>
        /// <param name="data">int 배열 개체</param>
        /// <returns>int[] 대응 DataTable 개체</returns>
        public static DataTable ToDataTable(this int[] data)
        {
            var table = new DataTable();
            table.Columns.Add("NO", typeof(int));

            foreach (int item in data)
            {
                DataRow row = table.NewRow();
                row["NO"] = item;

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// UserDefinedTable을 사용하기 위해 string 배열 개체를 DataTable로 변환한다.
        /// </summary>
        /// <param name="data">string 배열 개체</param>
        /// <returns>int[] 대응 DataTable 개체</returns>
        public static DataTable ToDataTable(this string[] data)
        {
            var table = new DataTable();
            table.Columns.Add("TXT", typeof(string));

            foreach (string item in data)
            {
                DataRow row = table.NewRow();
                row["TXT"] = item;

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 클래스 형변환
        /// </summary>
        /// <typeparam name="T">Custom Type</typeparam>
        /// <param name="myobj">Target Class</param>
        /// <returns>형변환 클래스</returns>
        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);

            var x = Activator.CreateInstance(target, false);

            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;

            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();

            PropertyInfo propertyInfo;
            object value;

            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);

                var prop = myobj.GetType().GetProperty(memberInfo.Name);
                if (prop != null)
                {
                    value = prop.GetValue(myobj, null);

                    propertyInfo.SetValue(x, value, null);
                }
            }

            return (T)x;
        }
    }
}
