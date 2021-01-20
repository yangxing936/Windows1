using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace Windows1
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            this.textBox2.Hide();
            this.textBox4.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = this.textBox2.Text;
            if (string.IsNullOrEmpty(text))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("未选中路径!");
                    return;
                }
                GetExcel getExcelColumnValueClass = new GetExcel();
                var data = getExcelColumnValueClass.GetExcelColumnValue(path, 1, new List<int>() { 1, 2, 3, 4, 5, 7, 8, 9 });

                if (data != null && data.Count > 0)
                {
                    MessageBox.Show("成功!");
                    this.textBox2.Text = JsonConvert.SerializeObject(data);
                    return;
                }

                MessageBox.Show("失败!");
                return;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("未选中路径!");
                    return;
                }
                GetExcel getExcelColumnValueClass = new GetExcel();
                var data = getExcelColumnValueClass.GetExcelColumnValue(path, 1, new List<int>() { 1, 2, 3, 4, 5, 7, 8, 9 });

                var oldText = text;//老数据

                var storeInfos = JsonConvert.DeserializeObject<List<StoreInfo>>(oldText);

                foreach (var item in data)
                {
                    storeInfos.Add(item);
                }

                if (storeInfos != null && storeInfos.Count > 0)
                {
                    MessageBox.Show("成功!");
                    this.textBox2.Text = JsonConvert.SerializeObject(storeInfos);
                    return;
                }

                MessageBox.Show("失败!");
                return;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("未选中路径!");
                return;
            }
            this.textBox1.Text = path;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = this.textBox2.Text;
            var dataPath = this.textBox1.Text;
            string nameList = "";
            DataTable table = new DataTable();
            CSVHelper.OpenCSVFile(ref table, ref nameList, dataPath);
            GetExcel getExcel = new GetExcel();
            //导入前有值 修改不导入
            if (table.Rows != null && table.Rows.Count > 0)
            {
                var path = this.textBox3.Text;//读取指定文件夹的所有xlsx;
                var externaPath = Directory.GetDirectories(path);//最外层路径 C:\Users\Administrator\Desktop\新建文件夹
                List<Store> store = new List<Store>();
                if (externaPath.Length > 0)
                {
                    for (int i = 0; i < externaPath.Length; i++)
                    {
                        //第二层路径 C:\Users\Administrator\Desktop\新建文件夹\1_73270
                        var newPath1 = externaPath[i];
                        //获取第二层路径下的子文件夹集合
                        var arrParentPath = Directory.GetDirectories(newPath1 + "\\");
                        //获取子文件夹集合中的第一个  C:\Users\Administrator\Desktop\新建文件夹\1_73270\1
                        var newPath3 = arrParentPath[0];
                        //获取子文件夹集合中的第二层子文件夹 C:\Users\Administrator\Desktop\新建文件夹\1_73270\1\亳州三中店
                        var arrChildPath = Directory.GetDirectories(newPath3 + "\\");

                        foreach (var item in arrChildPath)
                        {
                            var xlsxPaths = Directory.GetFiles(item, "*.xlsx", SearchOption.TopDirectoryOnly);

                            var index = item.IndexOf("1\\");
                            var xlsxPath = item.Substring(index, item.Length - index).Replace("1\\", "");
                            store.Add(new Store()
                            {
                                StoreName = xlsxPath,
                                Path = xlsxPaths.FirstOrDefault(),
                                Tel = "",
                                Name = ""
                            });
                        }
                    }
                }
                List<StoreInfo> stores = new List<StoreInfo>();
                foreach (var item in store)
                {
                    if (string.IsNullOrEmpty(item.Path))
                    {
                        continue;
                    }
                    var storeInfo = getExcel.GetExcelColumnRowValue(item.Path, 0, new List<int>() { 2 }, item.StoreName);

                    stores.Add(storeInfo);
                }
                var rows = table.Rows;
                var columns = table.Columns;
                for (int i = 0; i < rows.Count; i++)
                {
                    var list = rows[i].ItemArray;
                    var storeName = list[3];

                    var storeInfo = stores.Where(s => s.StoreName == storeName.ToString()).FirstOrDefault();
                    if (storeInfo != null)
                    {
                        list[11] = storeInfo.Tel;
                        list[13] = "商户_" + storeInfo.Name;
                    }
                    rows[i].ItemArray = list;
                }

                CSVHelper.SaveCSV(table, dataPath);

            }
            else
            {
                List<StoreInfo> storeInfos = JsonConvert.DeserializeObject<List<StoreInfo>>(data);

                DataTable dt = new DataTable();
                dt.Columns.Add("商家门店编码", typeof(String));
                dt.Columns.Add("品牌名称", typeof(String));
                dt.Columns.Add("门店名称", typeof(String));
                dt.Columns.Add("分店名称", typeof(String));
                dt.Columns.Add("省", typeof(String));
                dt.Columns.Add("市", typeof(String));
                dt.Columns.Add("区", typeof(String));
                dt.Columns.Add("街道地址", typeof(String));
                dt.Columns.Add("地址辅助描述", typeof(String));
                dt.Columns.Add("纬度", typeof(String));
                dt.Columns.Add("经度", typeof(String));
                dt.Columns.Add("门店电话", typeof(String));
                dt.Columns.Add("经营时间", typeof(String));
                dt.Columns.Add("收款主体", typeof(String));
                dt.Columns.Add("收款商户号", typeof(String));
                dt.Columns.Add("创建人联系电话", typeof(String));
                foreach (var item in storeInfos)
                {
                    var arr = item.XY.Split(',');
                    if (arr.Length == 0)
                    {
                        arr = "0,0".Split(',');
                    }
                    dt.Rows.Add("", "", "", item.StoreName, item.Sheng, item.Shi, item.Qu, item.Address, "", arr[1], arr[0], "110", item.Time, "商户_", "", "");
                }

                CSVHelper.SaveCSV(dt, dataPath);
            }






        }

    }
    public class Store
    {
        public string StoreName { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
