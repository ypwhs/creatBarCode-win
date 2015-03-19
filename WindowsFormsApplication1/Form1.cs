using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //A,B,C集    以及开始分隔结束码
        string[] Aji = {"0001101", "0011001", "0010011", "0111101", "0100011", 
         "0110001" ,"0101111", "0111011", "0110111", "0001011"};
        string[] Bji = {"0100111", "0110011", "0011011", "0100001", "0011101", 
         "0111001" ,"0000101", "0010001", "0001001", "0010111"};
        string[] Cji = {"1110010", "1100110", "1101100", "1000010", "1011100", 
         "1001110" ,"1010000", "1000100", "1001000", "1110100"};
        string start = "101";
        string fenge = "01010";

        private void button1_Click_1(object sender, EventArgs e)
        {
            //清空上一次画的条形码
            for (int i = 0; i < lastlabel.Count; i++)
            {
                lastlabel[i].Dispose();
            }
            lastlabel.Clear();

            string str = textBox1.Text;
            if (str.Count() == 11)
            {
                //生成检验码
                str = "6" + str;
                List<int> numbers = new List<int>();
                numbers.Add(0);
                for (int i = 0; i < str.Count(); i++)
                {
                    numbers.Add(str[11-i] - 48);
                }
                numbers.Add(6);
                int C1, C2, C;
                C1 = numbers[12] + numbers[10] + numbers[8] + numbers[6] + numbers[4] + numbers[2];
                C2 = numbers[11] + numbers[9] + numbers[7] + numbers[5] + numbers[3] + numbers[1];
                C = 10 - (C1 + C2 * 3) % 10;
                if (C == 10) C = 0;
                textBox2.Text = "目标条码：" + str + Convert.ToString(C) + "\n";
                numbers[0] = C;


                //生成条码的二进制数字
                string barcode = "";
                barcode = start;
                barcode += Aji[numbers[11]];
                barcode += Bji[numbers[10]];
                barcode += Bji[numbers[9]];
                barcode += Bji[numbers[8]];
                barcode += Aji[numbers[7]];
                barcode += Aji[numbers[6]];
                barcode += fenge;
                barcode += Cji[numbers[5]];
                barcode += Cji[numbers[4]];
                barcode += Cji[numbers[3]];
                barcode += Cji[numbers[2]];
                barcode += Cji[numbers[1]];
                barcode += Cji[numbers[0]];
                barcode += start;

                string text = "";
                text += "编码后:\n" + start + " ";
                text += Aji[numbers[11]] + " ";
                text += Bji[numbers[10]] + " ";
                text += Bji[numbers[9]] + " ";
                text += Bji[numbers[8]] + " ";
                text += Aji[numbers[7]] + " ";
                text += Aji[numbers[6]] + " ";
                text += fenge + " ";
                text += Cji[numbers[5]] + " ";
                text += Cji[numbers[4]] + " ";
                text += Cji[numbers[3]] + " ";
                text += Cji[numbers[2]] + " ";
                text += Cji[numbers[1]] + " ";
                text += Cji[numbers[0]] + " ";
                text += start + "\n";
                textBox2.AppendText(text);

                //开始画图
                for (int i = 0; i < barcode.Count(); i++)
                {
                    if (barcode[i] == '1') draw(i);
                }

                //最后添加条码的数字
                string c = "6   ";
                c += Convert.ToString(numbers[11])+" ";
                c += Convert.ToString(numbers[10]) + " ";
                c += Convert.ToString(numbers[9]) + " ";
                c += Convert.ToString(numbers[8]) + " ";
                c += Convert.ToString(numbers[7]) + " ";
                c += Convert.ToString(numbers[6]) + " ";
                c += " ";
                c += Convert.ToString(numbers[5]) + " ";
                c += Convert.ToString(numbers[4]) + " ";
                c += Convert.ToString(numbers[3]) + " ";
                c += Convert.ToString(numbers[2]) + " ";
                c += Convert.ToString(numbers[1]) + " ";
                c += Convert.ToString(numbers[0]) + " ";
                label2.Text = c;

                for (int i = 0; i < lastlabel.Count; i++)
                {
                    lastlabel[i].BringToFront();
                }
            }
            else
            {
                MessageBox.Show("长度有误！不是手机号！");
            }
        }
        List<Label> lastlabel = new List<Label>();
        private void draw(int x)
        {
            Label test = new Label();
            if ((x < 3) | ((x > 6 * 7 + 3) & (x < 6 * 7 + 3 + 5)) | (x > 12 * 7 + 5 + 2))
            {
                //前三位，第六个数字后面，还有十二个数字后面，都加长一点，好看
                test.Size = new Size(2, 110);
            }
            else
            {
                test.Size = new Size(2, 100);
            }
            test.Location = new Point(232 + x * 2, 288);    //计算某一条的位置
            test.BackColor = Color.Black;
            lastlabel.Add(test);
            this.Controls.Add(test);    //添加到窗口中
            //Application.DoEvents();
            //Thread.Sleep(100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

    }
}
