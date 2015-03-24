using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace TestRedis
{

    public partial class Form1 : Form
    {
        private static object m_res1 = new object();
        private static object m_res2 = new object();
        private static int m_count = 0;
        public Form1()
        {
            InitializeComponent();

            MyClass mc = new MyClass();
            mc.GetName();


            var cc = BitConverter.GetBytes(2);

            System.Console.WriteLine("int类型的最大值:  {0},最小值:  {1}", int.MaxValue, int.MinValue);
            System.Console.WriteLine("uint类型的最大值:  {0},最小值:  {1}", uint.MaxValue, uint.MinValue);
            System.Console.WriteLine("byte类型的最大值:  {0},最小值:  {1}", byte.MaxValue, byte.MinValue);
            System.Console.WriteLine("sbyte类型的最大值:  {0},最小值:  {1}", sbyte.MaxValue, sbyte.MinValue);
            System.Console.WriteLine("short类型的最大值:  {0},最小值:  {1}", short.MaxValue, short.MinValue);
            System.Console.WriteLine("ushort类型的最大值:  {0},最小值:  {1}", ushort.MaxValue, ushort.MinValue);
            System.Console.WriteLine("long类型的最大值:  {0},最小值:  {1}", long.MaxValue, long.MinValue);
            System.Console.WriteLine("ulong类型的最大值:  {0},最小值:  {1}", ulong.MaxValue, ulong.MinValue);
            System.Console.WriteLine("float类型的最大值:  {0},最小值:  {1}", float.MaxValue, float.MinValue);
            System.Console.WriteLine("double类型的最大值:  {0},最小值:  {1}", double.MaxValue, double.MinValue);
            System.Console.WriteLine("decimal类型的最大值:  {0},最小值:  {1}", decimal.MaxValue, decimal.MinValue);

            string a = "ddd";
            a.Firstdddddddddddd<int, int>(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(Thread1);
            Thread t2 = new Thread(Thread2);

            t1.Start();
            t2.Start();

            while (true)
            {
                int preCount = m_count;
                Thread.Sleep(0);  // 放弃当前线程的CPU时间片，Windows可能调度其他线程
                if (preCount == m_count)  // 数据没有变化，表明线程没有执行
                {
                    Console.WriteLine("dead lock! count: {0}", m_count);
                }
            }    



            //RedisClient rc = new RedisClient("127.0.0.1", 6379);
            
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 5000; i++)
            //{
            //    rc.Set<string>(i.ToString(), "values" + i.ToString());
            //}
            //sw.Stop();
            //string str = rc.Get<string>("test");
            //MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            //TestDic();
        }
        protected internal void TestDic()
        {
            Dictionary<string,string> dc = new Dictionary<string,string>();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 50000; i++)
            {
                dc.Add(i.ToString(), "values" + i.ToString());
            }
            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }



        private static void Thread1()
        {
            while (true)
            {
                Monitor.Enter(m_res2);  // 先锁 m_res2
                Monitor.Enter(m_res1);  // 再锁 m_res1

                m_count++;

                Monitor.Exit(m_res1);  // 释放锁不存在先后关系
                Monitor.Exit(m_res2);
            }
        }

        private static void Thread2()
        {
            while (true)
            {
                Monitor.Enter(m_res1);  // 先锁 m_res1
                Monitor.Enter(m_res2);

                m_count++;

                Monitor.Exit(m_res1);
                Monitor.Exit(m_res2);
            }
        }
    }
}
