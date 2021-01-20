using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows1
{
    public class GetExcel
    {
        /// <summary>
        /// 读取Excel表格列数据
        /// </summary>
        /// <param name="path">Excel表格所在的路径</param>
        /// <param name="sheetIndex">需要读取的Sheet页码序号</param>
        /// <param name="columnIndex">需要读取的列序号</param>
        /// <returns></returns>
        public List<StoreInfo> GetExcelColumnValue(string path, int sheetIndex, List<int> intlist)
        {
            List<StoreInfo> list = new List<StoreInfo>();

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            ExcelPackage excel = new ExcelPackage(fileStream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelWorksheet sheet = excel.Workbook.Worksheets[sheetIndex];
            try
            {
                for (int i = 3; i <= sheet.Dimension.Rows; i++)
                {

                    StoreInfo storeInfo = new StoreInfo();
                    foreach (var item in intlist)
                    {
                        var cell = sheet.Cells[i, item];

                        if (cell != null && cell.Value != null)
                        {
                            var dataXY = cell.LocalAddress;//获取第三行的坐标

                            var cellXY = sheet.Cells[2, item];//获取第二行的坐标

                            if (cell != null && cell.Value != null)
                            {
                                if (cellXY.Value.ToString().IndexOf("门店名称") >= 0)
                                {
                                    storeInfo.StoreName = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("门店编号") >= 0)
                                {
                                    storeInfo.StoreCode = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("城市") >= 0)
                                {
                                    storeInfo.Shi = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("省份") >= 0)
                                {
                                    storeInfo.Sheng = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("区域") >= 0)
                                {
                                    storeInfo.Qu = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("营业时间") >= 0)
                                {
                                    storeInfo.Time = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("详情地址") >= 0)
                                {
                                    storeInfo.Address = cell.Value.ToString();
                                }
                                if (cellXY.Value.ToString().IndexOf("腾讯") >= 0)
                                {
                                    storeInfo.XY = cell.Value.ToString();
                                }
                                storeInfo.GonSi = "甜啦啦";
                            }


                        }


                    }
                    if (storeInfo != null && string.IsNullOrEmpty(storeInfo.StoreCode) == false)
                    {
                        list.Add(storeInfo);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sheet.Dispose();
                excel.Dispose();
                fileStream.Dispose();
            }

            return list;
        }
        /// <summary>
        /// 读取Excel表格列数据
        /// </summary>
        /// <param name="path">Excel表格所在的路径</param>
        /// <param name="sheetIndex">需要读取的Sheet页码序号</param>
        /// <param name="columnIndex">需要读取的列序号</param>
        /// <returns></returns>
        public StoreInfo GetExcelColumnRowValue(string path, int sheetIndex, List<int> intlist, string StoreName)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            ExcelPackage excel = new ExcelPackage(fileStream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelWorksheet sheet = excel.Workbook.Worksheets[sheetIndex];
            try
            {
                StoreInfo storeInfo = new StoreInfo();
                for (int i = 3; i <= sheet.Dimension.Rows; i++)
                {
                    foreach (var item in intlist)
                    {
                        var cell = sheet.Cells[i, item];
                        if (cell.Address == "B6")
                        {
                            storeInfo.Name = cell.Value == null ? "暂无" : cell.Value.ToString();
                        }
                        storeInfo.StoreName = StoreName;
                        if (cell.Address == "B20")
                        {
                            storeInfo.Tel = cell.Value == null ? "暂无" : cell.Value.ToString();
                        }
                    }
                }
                return storeInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sheet.Dispose();
                excel.Dispose();
                fileStream.Dispose();
            }
        }
    }
    public class StoreInfo
    {
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string Sheng { get; set; }
        public string Shi { get; set; }
        public string Qu { get; set; }
        public string GonSi { get; set; }
        public string Time { get; set; }
        public string Address { get; set; }
        public string XY { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
