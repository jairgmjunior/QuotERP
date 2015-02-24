using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Fashion.Framework.Domain;
using Fashion.Framework.UnitOfWork;
using FluentNHibernate.Conventions;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class EnumerableExtensions
    {
        #region IsNullOrEmpty
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.IsEmpty();
        }
        #endregion

        #region Inflate
        /// <summary>
        /// Colocar todas as colunas com a mesma quantidade de linhas
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Inflate<T>(
                 this IEnumerable<IEnumerable<T>> source)
        {
            var rows = source.ToList();
            var maxRows = rows.Max(m => m.Count());

            foreach (var row in rows)
            {
                var enumerator = row.GetEnumerator();
                var newrow = new List<T>();

                for (int i = 0; i < maxRows; i++)
                    newrow.Add(enumerator.MoveNext() ? enumerator.Current : default(T));

                yield return newrow;
            }
        }
        #endregion

        #region Transpose
        /// <summary>
        /// Troca as linhas e colunas de uma sequência aninhada.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(
                 this IEnumerable<IEnumerable<T>> source)
        {
            // Transpor as colunas em linhas
            return from row in source
                   from col in row.Select(
                       (x, i) => new KeyValuePair<int, T>(i, x))
                   group col.Value by col.Key into c
                   select c as IEnumerable<T>;
        }
        #endregion

        //todo só funciona para agrupar propriedades string
        #region GroupBy
        public static IEnumerable<IGrouping<string, T>> GroupBy<T>(this IEnumerable<T> source, string keySelector)
        {
            var parameter = Expression.Parameter(typeof(T));

            var keys = keySelector.Split('.');

            MemberExpression body = null;

            foreach (var key in keys)
            {
                if (body == null)
                    body = Expression.Property(parameter, key);
                else
                    body = Expression.Property(body, key);
            }

            // ReSharper disable AssignNullToNotNullAttribute
            return source.GroupBy(Expression.Lambda<Func<T, string>>(body, parameter).Compile());
            // ReSharper restore AssignNullToNotNullAttribute
        }
        #endregion

        #region ToSelectList
        /// <summary>
        /// Cria um SelectList para popular uma combo.
        /// Se o item selecionado não estiver na lista, adiciona-o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="dataTextField"></param>
        /// <param name="selectedValue"></param>
        /// <param name="dataValueField"></param>
        /// <returns></returns>
        public static SelectList ToSelectList<T>(this IList<T> collection, string dataTextField, long? selectedValue = null, string dataValueField = "Id") where T : DomainObject, new()
        {
            if (selectedValue != null && collection.Any(c => c.Id == selectedValue) == false)
            {
                var selectedItem = Session.Current.Get<T>(selectedValue);
                if (selectedItem != null)
                {
                    var list = new List<T> { selectedItem };
                    list.AddRange(collection);
                    collection = list;
                }
            }

            return new SelectList(collection, dataValueField, dataTextField, selectedValue);
        }
        #endregion
    }
}