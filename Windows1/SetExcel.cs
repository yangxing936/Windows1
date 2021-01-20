using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows1
{
    public class SetExcel
    {
        public static void ExcelTest()
        {
            //导出：将数据库中的数据，存储到一个excel中

            //1、查询数据库数据  

            //2、  生成excel
            //2_1、生成workbook
            //2_2、生成sheet
            //2_3、遍历集合，生成行
            //2_4、根据对象生成单元格
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建工作表
            var sheet = workbook.CreateSheet("信息表");
            //创建标题行（重点）
            var row = sheet.CreateRow(0);
            //创建单元格
            var cellid = row.CreateCell(0);
            cellid.SetCellValue("编号");
            var cellname = row.CreateCell(1);
            cellname.SetCellValue("用户名");
            var cellpwd = row.CreateCell(2);
            cellpwd.SetCellValue("密码");
            var celltype = row.CreateCell(3);
            celltype.SetCellValue("类型");

            FileStream file = new FileStream(@"C:\Users\ibm\信息表.xls", FileMode.CreateNew, FileAccess.Write);
            workbook.Write(file);
            file.Dispose();
        }
    }
}
