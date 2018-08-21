using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace DemoThreadWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyMethod my = method;

            IAsyncResult asyncResult = my.BeginInvoke(MethodCompleted, my);
        }

        private delegate int MyMethod();
        private int method()
        {
            Thread.Sleep(10000);
            return 100;
        }
        private void MethodCompleted(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                return;
            }
            textBox1.Text = (asyncResult.AsyncState as MyMethod).EndInvoke(asyncResult).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.cnblogs.com");
            IAsyncResult asyncResult = request.BeginGetResponse(requestCompleted, request);
        }

        private void requestCompleted(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
                return;
            System.Net.HttpWebRequest hwr = asyncResult.AsyncState as System.Net.HttpWebRequest;

            System.Net.HttpWebResponse response =
        (System.Net.HttpWebResponse)hwr.EndGetResponse(asyncResult);

            System.IO.StreamReader sr = new
        System.IO.StreamReader(response.GetResponseStream());
            textBox1.Text = sr.ReadToEnd();
        }
        //private delegate System.Net.HttpWebResponse RequestDelegate(System.Net.HttpWebRequest request);
    }
}
