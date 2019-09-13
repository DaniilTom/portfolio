using GBVH_Tools.Common.Attributes;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GBVH_Tools.DAL
{
    public static class SQLStatementGenerator
    {
        /// <summary>
        /// Метод генерации запросов DELETE.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список предметов, для которых генерируются запросы</param>
        /// <param name="dbName">Имя БД</param>
        /// <returns></returns>
        public static string GenDelete<T>(IEnumerable<T> list, string dbName = "item_template")
        {
            Type type = typeof(T);
            PropertyInfo[] pInfo = type.GetProperties();

            string total = "";

            foreach(var item in list)
            {
                StringBuilder del = new StringBuilder($@"DELETE FROM '{dbName}' WHERE ");
                foreach(var prop in pInfo)
                {
                    var attrPK = prop.GetCustomAttribute<DBTargetKey>();
                    if (!(attrPK is null))
                    {
                        del.Append($"{prop.Name}={prop.GetValue(item).ToString()}");
                        break;
                    }
                }

                total += del.ToString() + ";" + Environment.NewLine;
            }

            return total;
        }

        /// <summary>
        /// Метод генерации запросов UPDATE.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список предметов, для которых генерируются запросы</param>
        /// <param name="dbName">Имя БД</param>
        /// <returns></returns>
        public static string GenUpdate<T>(IEnumerable<T> list, string dbName = "item_template")
        {
            Type type = typeof(T);
            PropertyInfo[] pInfo = type.GetProperties();

            string total = "";

            foreach (var item in list)
            {
                StringBuilder begin = new StringBuilder($@"UPDATE '{dbName}' " + Environment.NewLine);
                StringBuilder set = new StringBuilder("SET ");
                StringBuilder where = new StringBuilder("WHERE ");

                Func<string, string, string> setter = (string name, string value) => { return $"{name}={value}, " + Environment.NewLine; };

                foreach (var prop in pInfo)
                {
                    var attrNot = prop.GetCustomAttribute<DBNotInclude>();
                    if (!(attrNot is null))
                        continue;

                    var attrTarget = prop.GetCustomAttribute<DBTargetKey>();
                    if (!(attrTarget is null))
                    {
                        where.Append($"{prop.Name}={prop.GetValue(item).ToString()}");
                        continue;
                    }

                    var attrPrimary = prop.GetCustomAttribute<DBPrimaryKey>(); // обновлять первичный ключ не хорошо
                    if (!(attrPrimary is null))
                        continue;

                    #region Обработка примитивных типов и перечислений

                    var propType = prop.PropertyType;
                    if (propType == typeof(int))
                    {
                        set.Append(setter(prop.Name, prop.GetValue(item).ToString()));
                        continue;
                    }
                    else if (propType == typeof(float))
                    {
                        set.Append(setter(prop.Name, prop.GetValue(item).ToString()));
                        continue;
                    }
                    else if (propType.IsEnum)
                    {
                        set.Append(setter(prop.Name, ((int)prop.GetValue(item)).ToString()));
                        continue;
                    }
                    else if (propType == typeof(string))
                    {
                        string s = (string)prop.GetValue(item);
                        if (String.IsNullOrEmpty(s))
                            set.Append(setter(prop.Name, "''"));
                        else
                            set.Append(setter(prop.Name, $"'{prop.GetValue(item).ToString()}'"));
                        continue;
                    }

                    #endregion

                    #region Обработка словаря

                    var attr = prop.GetCustomAttribute<DBDictionaryAttribute>();
                    if (!(attr is null))
                    {
                        object propValue = prop.GetValue(item);
                        IDictionary d = (IDictionary)propValue;
                        int n = 1;
                        foreach (DictionaryEntry pair in d) // словарь реализует IDictionaryEnumerable
                        {
                            set.Append(setter($"{attr.KeyColumn}{n}", $"{((int)pair.Key).ToString()}"));
                            set.Append(setter($"{attr.ValueColumn}{n}", $"{pair.Value}"));
                            n++;
                        }
                        if (n - 1 > attr.MaxCount)
                            throw new IndexOutOfRangeException($"Количество элементов ({n}) коллекции {prop.Name} " +
                                $"первысило указанное в атрибуте ({attr.MaxCount})");

                        continue;
                    }

                    #endregion

                    #region Обработка списка

                    var attrL = prop.GetCustomAttribute<DBListAttribute>();
                    if (!(attrL is null))
                    {
                        object propValue = prop.GetValue(item);
                        IList l = (IList)propValue;
                        int n = 1;
                        foreach (int val in l)
                        {
                            set.Append(setter($"{attrL.ColumnName}{n}", $"{val}"));
                            n++;
                        }
                        if (n - 1 > attrL.MaxCount)
                            throw new IndexOutOfRangeException($"Количество элементов ({n}) коллекции {prop.Name} " +
                                $"первысило указанное в атрибуте ({attrL.MaxCount})");

                        continue;
                    }

                    #endregion
                }

                string setString = set.ToString();
                setString = setString.Remove(setString.LastIndexOf(','), 1); // лишняя запятая

                string whereString = where.ToString();

                total += begin + setString + whereString + ";" + Environment.NewLine + Environment.NewLine;
            }

            return total;
        }

        /// <summary>
        /// Метод генерации запросов CREATE.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список предметов, для которых генерируются запросы</param>
        /// <param name="dbName">Имя БД</param>
        /// <returns></returns>
        public static string GenCreate<T>(IEnumerable<T> list, string dbName = "item_template")
        {
            Type type = typeof(T);
            PropertyInfo[] pInfo = type.GetProperties();

            string total = "";

            foreach (var item in list)
            {
                StringBuilder result = new StringBuilder($@"INSERT INTO '{dbName}' ");
                StringBuilder columns = new StringBuilder("(");
                StringBuilder values = new StringBuilder("(");

                foreach (var prop in pInfo)
                {
                    var attrNot = prop.GetCustomAttribute<DBNotInclude>();
                    if (!(attrNot is null))
                        continue;

                    #region Обработка примитивных типов и перечислений

                    var propType = prop.PropertyType;
                    if (propType == typeof(int) || propType.IsEnum)
                    {
                        columns.Append($"{prop.Name}, ");
                        values.Append($"{(int)prop.GetValue(item)}, ");
                        continue;
                    }
                    else if (propType == typeof(float))
                    {
                        columns.Append($"{prop.Name}, ");
                        values.Append($"{(float)prop.GetValue(item)}, ");
                        continue;
                    }
                    else if (propType == typeof(string))
                    {
                        columns.Append($"{prop.Name}, ");
                        string s = (string)prop.GetValue(item);
                        if (String.IsNullOrEmpty(s))
                            values.Append("'', ");
                        else
                            values.Append($"'{(string)prop.GetValue(item)}', ");
                        continue;
                    }

                    #endregion

                    #region Обработка словаря

                    var attr = prop.GetCustomAttribute<DBDictionaryAttribute>();
                    if (!(attr is null))
                    {
                        object propValue = prop.GetValue(item);
                        IDictionary d = (IDictionary)propValue;
                        int n = 1;
                        foreach (DictionaryEntry pair in d) // словарь реализует IDictionaryEnumerable
                        {
                            columns.Append($"{attr.KeyColumn}{n}, {attr.ValueColumn}{n}, ");
                            values.Append($"{(int)pair.Key}, {(int)pair.Value}, ");
                            n++;
                        }
                        if (n - 1 > attr.MaxCount)
                            throw new IndexOutOfRangeException($"Количество элементов ({n}) коллекции {prop.Name} " +
                                $"первысило указанное в атрибуте ({attr.MaxCount})");

                        continue;
                    }

                    #endregion

                    #region Обработка списка

                    var attrL = prop.GetCustomAttribute<DBListAttribute>();
                    if (!(attrL is null))
                    {
                        object propValue = prop.GetValue(item);
                        IList l = (IList)propValue;
                        int n = 1;
                        foreach (int val in l)
                        {
                            columns.Append($"{attrL.ColumnName}{n}, ");
                            values.Append($"{val}");
                            n++;
                        }
                        if (n - 1 > attrL.MaxCount)
                            throw new IndexOutOfRangeException($"Количество элементов ({n}) коллекции {prop.Name} " +
                                $"первысило указанное в атрибуте ({attrL.MaxCount})");

                        continue;
                    }

                    #endregion
                }

                columns.Remove(columns.Length - 2, 2); // в конце строки есть лишний пробел и запятая
                columns.Append(")");
                values.Remove(values.Length - 2, 2);
                values.Append(")");

                result.Append(columns.ToString() + " VALUES " + values.ToString() + ";");
                total += result.ToString() + Environment.NewLine;
            }

            return total;
        }
    }
}
