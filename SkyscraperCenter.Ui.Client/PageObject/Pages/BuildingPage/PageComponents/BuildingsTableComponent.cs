using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.BuildingPage;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage.PageComponents
{
    public class BuildingsTableComponent
    {
        private readonly BuildingsPageLocators _locators;

        public BuildingsTableComponent(BuildingsPageLocators locators)
        {
            _locators = locators;
        }

        public List<string> GetHeaderNames()
        {
            return _locators.Headers.Select(h => h.Text.Trim()).ToList();
        }

        public int GetRecordsCount()
        {
            return _locators.RowElements.Count;
        }

        /// <summary>
        /// Gets table row values for Table with static columns
        /// </summary>
        /// <returns></returns>
        public List<BuildingsTableComponentModel> GetStaticTableRecords()
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

        //TODO: Create Table with dynamic columns

        public IList<IWebElement> GetRowCells(IWebElement row)
        {
            var cellsPerRow = row.FindElements(By.TagName("td"));
            return cellsPerRow;
        }
    }
}
