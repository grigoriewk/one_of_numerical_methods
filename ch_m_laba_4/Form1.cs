using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ch_m_laba_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Функция
        private double f(double x)   
        {
            return (4 * Math.Pow(x, 2) - x + 7) / (4 * Math.Cos(4 * x));
            //return x / Math.Sqrt(Math.Pow(x, 2) + 1);
        }

        //Первая производная
        public double firstDerivate(double x, double h)   
        {
            return (f(x + h) - f(x - h)) / (2 * h);
        }

        //Вторая производная
        public double secondDerivate(double x, double h)  
        {
            return (f(x + h) - 2 * f(x) + f(x - h)) / Math.Pow(h, 2);
        }

        //Метод левых треугольников
        double leftRectMethod(double min, double max, int n)
        {
            double sum = 0;
            double step = (max - min) / n;
            for (double i = min; i<max; i += step)
                sum += f(i);
            return sum * step;
        }

        //Метод правых прямоугольников
        double rightRectMethod(double min, double max, int n) 
        {
            double sum = 0;
            double step = (max - min) / n;
            for (double i = min + step; i <= max; i += step)
                sum += f(i);
            return sum * step;
        }

        //Метод центральных прямоугольников
        double centerRectMethod(double min, double max, int n)
        {
            double sum = 0;
            double step = (max - min) / n;
            for (double i = min + step; i <= max; i += step)
                sum += f(i + step / 2);
            return sum * step;
        }

        //Метод трапеций
        double trapezMethod(double min, double max, int n)
        {
            double sum = 0;
            double step = (max - min) / n;
            for (double i = min; i < max; i += step)
                sum += f(i);

            return step * (((f(min) + f(max)) / 2) + sum);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.Show();
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisY.Maximum = 1;
                chart1.ChartAreas[0].AxisX.Maximum = 5;
                const int temp = 5;
                label5.Text = "Первая производная: ";
                label6.Text = "Вторая производная: ";
                double x = Convert.ToDouble(textBox1.Text);           //х
                double h = Convert.ToDouble(textBox2.Text);           //интервал 
                double minX = x - temp / (2 * h);
                label5.Text += Convert.ToString(firstDerivate(x, h).ToString("0.0000"));
                label6.Text += Convert.ToString(secondDerivate(x, h).ToString("0.0000"));
                textBox3.Text = "Таблица значений:\r\n" + "№\t  X\t  Y" + "\r\n";
                for (int i=0;i<temp;++i)
                {
                    double xValue = minX + h * i;
                    double yValue = f(xValue);
                    textBox3.Text += i + "\t" + xValue + "\t" + yValue.ToString("0.0000") + "\r\n";
                    chart1.Series[0].Points.AddXY(xValue, yValue);
                }
            } catch { MessageBox.Show("Упс.. где то ошибка. проверьте ОДЗ", "Херня какая то.."); }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Show();
            chart2.Series[0].Points.Clear();
            double max = Convert.ToDouble(textBox7.Text);    //верхний предел
            double min = Convert.ToDouble(textBox6.Text);    //нижний предел
            int n = Convert.ToInt32(textBox5.Text);      //N
            double h = (max - min) / 10;
            label15.Text = leftRectMethod(min, max, n).ToString("0.0000000");
            label16.Text = rightRectMethod(min, max, n).ToString("0.0000000");
            label17.Text = centerRectMethod(min, max, n).ToString("0.0000000");
            label18.Text = trapezMethod(min, max, n).ToString("0.0000000");
            textBox4.Text = "Таблица значений:\r\n" + "№\t  X\t  Y" + "\r\n";
            for (int i = 0; i < 10; ++i)
            {
                double xValue = min + h * i;
                double yValue = f(xValue);
                textBox4.Text += i + "\t" + xValue + "\t" + yValue.ToString("0.0000") + "\r\n";
                chart2.Series[0].Points.AddXY(xValue, yValue);
            }
        }
    }
}
