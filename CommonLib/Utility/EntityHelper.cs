using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CommonLib.Utility
{
    /// 操作实体工具类。
    /// </summary>
    /// <typeparam name="T">Entity类型。</typeparam>
    public static class EntityHelper<T> where T : new()
    {
        /// <summary>
        /// 将DataTable转换为Entity数组。
        /// </summary>
        /// <param name="table">要转换的DataTable。</param>
        /// <returns>Entity数组。</returns>
        public static List<T> DataTable2EntityList(DataTable table)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var entityList = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                entityList.Add(DataRow2Entity(row, type, properties));
            }

            return entityList;
        }

        /// <summary>
        /// 将DataTable转换为Entity数组。
        /// </summary>
        /// <param name="table">要转换的DataTable。</param>
        /// <returns>Entity数组。</returns>
        public static T[] DataTable2Entities(DataTable table)
        {
            return DataTable2EntityList(table).ToArray();
        }

        private static T DataRow2Entity(DataRow row, Type type, PropertyInfo[] properties)
        {
            var entity = new T();

            foreach (var property in properties)
            {
                var columnName = property.Name;

                if (!row.Table.Columns.Contains(columnName) || !property.CanWrite)
                {
                    continue;
                }

                var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);

                if (underlyingType == null)
                {
                    underlyingType = property.PropertyType;
                }

                if (underlyingType.IsEnum)
                {
                    if (row.IsNull(columnName))
                    {
                        property.SetValue(entity, null, null);
                    }
                    else
                    {
                        property.SetValue(entity, Enum.Parse(underlyingType, Convert.ToString(row[columnName])), null);
                    }
                }
                else
                {
                    if (underlyingType == typeof(int))
                    {
                        property.SetValue(entity, row.IsNull(columnName) ? default(int?) : Convert.ToInt32(row[columnName]), null);
                    }
                    else
                    {
                        property.SetValue(entity, row.IsNull(columnName) ? null : row[columnName], null);
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// 获取实体成员的名称。
        /// </summary>
        /// <param name="expression">访问实体成员的表达式。</param>
        /// <returns>成员名称。</returns>
        /// <example>
        /// <code>
        /// public calss DemoEntity
        /// {
        ///     public int Id{get;set;}
        ///     
        ///     public string Name{get;set;}
        /// }
        /// 
        /// EntityHelper.GetMemberName&lt;DemoEntity&lt;(entity=>entity.Id) 将返回 Id
        /// EntityHelper.GetMemberName&lt;DemoEntity&lt;(entity=>entity) 将返回 DemoEntity
        /// </code>
        /// </example>
        public static string GetMemberName(Expression<Func<T, object>> expression)
        {
            return GetMemberInfo(expression).Name;
        }

        private static MemberInfo GetMemberInfo(Expression<Func<T, object>> expression)
        {
            MemberInfo membarInfo = null;

            if (expression.Body is UnaryExpression)
            {
                membarInfo = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
            }
            else if (expression.Body is MemberExpression)
            {
                membarInfo = ((MemberExpression)expression.Body).Member;
            }
            else if (expression.Body is ParameterExpression)
            {
                membarInfo = ((ParameterExpression)expression.Body).Type;
            }

            return membarInfo;
        }
    }
}
