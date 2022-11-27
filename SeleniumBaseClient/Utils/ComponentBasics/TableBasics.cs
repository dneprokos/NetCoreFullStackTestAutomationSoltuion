using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace SeleniumBase.Client.Utils.ComponentBasics
{
    public abstract class TableBasics
    {
        protected IList<IWebElement> GetRowCells(IWebElement row)
        {
            var cellsPerRow = row.FindElements(By.TagName("td"));
            return cellsPerRow;
        }

        protected List<string> GetHeaderNames(List<IWebElement> headerElements)
        {
            return headerElements.Select(h => h.Text.Trim()).ToList();
        }

        protected List<T> GetMappedTableRows<T>(List<IWebElement> headerElements, List<IWebElement> rowElements)
        {
            var headers = GetHeaderNames(headerElements);

            var mappedRowsWitData = new List<T>();

            //ForEach row
            for (int i = 0; i < rowElements.Count; i++)
            {
                var rowCells = GetRowCells(rowElements[i]).ToList();
                //var rowCells = _locators.RowCells(rows[i]);
                Dictionary<string, string> rowWithData = new Dictionary<string, string>();

                for (int j = 0; j < rowCells.Count; j++)
                {
                    rowWithData.Add(headers[j], rowCells[j].GetAttribute("innerHTML"));
                }
                mappedRowsWitData.Add(GetObject<T>(rowWithData));
            }

            return mappedRowsWitData;
        }

        protected T GetObject<T>(IDictionary<string, string> d)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            T res = Activator.CreateInstance<T>();
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].CanWrite && d.ContainsKey(props[i].Name))
                {
                    props[i].SetValue(res, d[props[i].Name], null);
                }
            }
            return res;
        }
    }
}
