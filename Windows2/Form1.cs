using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows2
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            TaskHelper(10);
            TimeOut();
            Time();
        }
        public void Time()
        {
            int c = 0;
            var start = Watch.TimerStart();
            var str = "";
            for (int i = 0; i < 100000; i++)
            {
                str = str + i;
                c = c + 1;
            }
            var end = Watch.TimerEnd(start);
            this.textBox3.Text = end + "," + c;
        }
        public void TimeOut()
        {
            int c = 0;
            var start = Watch.TimerStart();
            var str = "";
            Task task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task2 = Task.Run(() =>
            {
  
            });
            Task task3 = Task.Run(() =>
            {
                for (int i = 20000; i < 30000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task4 = Task.Run(() =>
            {
                for (int i = 30000; i < 40000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task5 = Task.Run(() =>
            {
                for (int i = 40000; i < 50000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task6 = Task.Run(() =>
            {
                for (int i = 50000; i < 60000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task7 = Task.Run(() =>
            {
                for (int i = 60000; i < 70000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task8 = Task.Run(() =>
            {
                for (int i = 70000; i < 80000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task9 = Task.Run(() =>
            {
                for (int i = 80000; i < 90000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task task10 = Task.Run(() =>
            {
                for (int i = 90000; i < 100000; i++)
                {
                    str = str + i;
                    c = c + 1;
                }
            });
            Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);
            var end = Watch.TimerEnd(start);
            this.textBox1.Text = end + "," + c;
        }
        public void TaskHelper(int count)
        {
            if (count <= 0)
            {
                return;
            }
            int c = 0;
            var start = Watch.TimerStart();
            Task[] array = new Task[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = Task.Factory.StartNew(delegate
                {
                    var str = "";
                    for (int j = 0; j < 10000; j++)
                    {
                        str = str + j;
                        c = c + 1;
                    }

                });
            }
            Task.WaitAll(array);
            var end = Watch.TimerEnd(start);

            this.textBox2.Text = end + "," + c;
        }
    }
    public class Watch
    {
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
    }
}
