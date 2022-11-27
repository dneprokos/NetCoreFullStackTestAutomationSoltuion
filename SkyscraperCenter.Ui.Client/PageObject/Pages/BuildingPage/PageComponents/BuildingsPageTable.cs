using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumBase.Client.Utils.ComponentBasics;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.BuildingPage.Components;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage.PageComponents
{
    public class BuildingsPageTable : TableBasics
    {
        private readonly BuildingsPageTableLocators _locators;

        public BuildingsPageTable()
        {
            _locators = new BuildingsPageTableLocators();
        }

        /// <summary>
        /// Returns table rows count
        /// </summary>
        /// <returns></returns>
        public int GetRecordsCount()
        {
            return _locators.RowElements.Count;
        }

        /// <summary>
        /// Gets table with static predefined columns 
        /// </summary>
        /// <returns></returns>
        public List<BuildingsTableComponentModel> GetTableWithStaticColumns()
        {
            var tableRecords = new List<BuildingsTableComponentModel>();
            List<IWebElement> rows = _locators.RowElements;

            for (int i = 0; i < rows.Count; i++)
            {
                var row = new BuildingsTableComponentModel();
                var rowCells = GetRowCells(rows[i]).ToList();

                for (int j = 0; j < rowCells.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            row.RANK = rowCells[j].Text;
                            break;
                        case 1:
                            row.NAME = rowCells[j].Text;
                            break;
                        case 2:
                            row.CITY = rowCells[j].Text;
                            break;
                        case 3:
                            row.STATUS = rowCells[j].Text;
                            break;
                        case 4:
                            row.COMPLETION = rowCells[j].Text;
                            break;
                        case 5:
                            row.HEIGHT = rowCells[j].Text;
                            break;
                        case 6:
                            bool isParsed = int.TryParse(rowCells[j].Text, out int floors);
                            row.FLOORS = isParsed ? floors : (int?)null;
                            break;
                        case 7:
                            row.MATERIAL = rowCells[j].Text;
                            break;
                        case 8:
                            row.FUNCTION = rowCells[j].Text;
                            break;
                    }
                }

                tableRecords.Add(row);
            }
            
            return tableRecords;
        }

        [Obsolete("Not all results are displayed correctly. Still need to work on it. Please use static table.")]
        public List<BuildingsTableComponentModelAllStringTypes> GetTableWithDynamicColumns()
        {
            return GetMappedTableRows<BuildingsTableComponentModelAllStringTypes>(_locators.Headers, _locators.RowElements);
        }
    }
}
