using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Graph
{

    public partial class Form1 : Form
    {
        private Form2 form2;
        private Form3 form3;
        private Form4 form4;
        private Form5 form5;
        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();
            form3 = new Form3();
            form4 = new Form4();
            form5 = new Form5();
        }

        public static Picture Graph = new Picture("Graph");


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();

            try
            {
                if (form2.DialogResult == DialogResult.OK)
                {
                    string undername;
                    Picture p = form2.GetInput(out undername);
                    if (!Graph.insert(undername, p)) // 請查看183行
                        MessageBox.Show("找不到此PICTURE " + undername);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("錯誤的格式內容");
            }

            textBox1.Text = Graph.show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
            try{
                if (form3.DialogResult == DialogResult.OK)
                {
                    string undername;
                    Rectangle p = form3.GetRectangle(out undername);
                    if (!Graph.insert(undername, p))// 請查看183行
                        MessageBox.Show("找不到此PICTURE " + undername);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("錯誤的格式內容");
            }

            textBox1.Text = Graph.show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form4.ShowDialog();
            try{
                if (form4.DialogResult == DialogResult.OK)
                {
                    string undername;
                    Triangle p = form4.GetTriangle(out undername);
                    if (!Graph.insert(undername, p))// 請查看183行
                        MessageBox.Show("找不到此PICTURE " + undername);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("錯誤的格式內容");
            }

            textBox1.Text = Graph.show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form5.ShowDialog();
            try{
                if (form5.DialogResult == DialogResult.OK)
                {
                    string undername;
                    Circle p = form5.GetCircle(out undername);
                    if (!Graph.insert(undername, p))// 請查看183行
                        MessageBox.Show("找不到此PICTURE " + undername);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("錯誤的格式內容");
            }

            textBox1.Text = Graph.show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = Graph.area() + " ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Graph.show();
        }
    }

    public class GraphBase
    {
        public GraphBase(string name)
        {
            this.name = name;
        }


        public string name {get;}

        public virtual string type()
        {
            return "GraphBase";
        }

        public virtual string show()
        {
            return "";
        }

        public virtual double area()
        {
            return 0.0;
        }

    }

    public class Picture : GraphBase
    {

        private List<GraphBase> child = new List<GraphBase>();

        public Picture(string name) : base(name)
        {
        }

        public override string type()
        {
            return "Picture";
        }

        public bool insert(string findname, GraphBase graphBase)
        {
            if (name == findname)
            {
                child.Add(graphBase);
                return true;
            }

            foreach (var i in child)
            {
                if (i.type() == "Picture" && ((Picture)i).insert(findname, graphBase))
                    return true;
            }

            return false;
            // true 代表有找到 false則代表沒有 這邊我是直接在樹狀結構遍歷 
            // 你也可以使用老師的方法 把所有Picture Type存在一個List裡面
            // 應該執行速度也會比較快 (我只是懶得這樣做)


        }

        public override string show()
        {
            string temp = "Picture "+name + (child.Count != 0 ? ":" : "");
            foreach (var graphBase in child)
            {
                temp += graphBase.show() + " ";
            }

            return "{"+ temp + "}";
        }

        public override double area()
        {
            double temp = 0.0;
            foreach (var graphBase in child)
            {
                temp += graphBase.area();
            }

            return temp;
        }
    }

    public class Circle : GraphBase
    {
        private double radius;
        public Circle(string name, double radius) : base(name)
        {
            this.radius = radius;
        }

        public override double area()
        {
            return radius * radius * Math.PI;
        }

        public override string show()
        {
            return "Circle " + name + "(" + radius + ")";
        }

        public override string type()
        {
            return "Circle";
        }
    }

    public class Rectangle : GraphBase
    {
        private double length;
        private double width;
        public Rectangle(string name, double length, double width) : base(name)
        {
            this.length = length;
            this.width = width;
        }

        public override double area()
        {
            return length * width;
        }

        public override string show()
        {
            return "Rectangle "+ name +"(" + length + "," + width + ")";
        }

        public override string type()
        {
            return "Rectangle";
        }
    }

    public class Triangle : GraphBase
    {
        private double length;
        private double width;
        public Triangle(string name, double length, double width) : base(name)
        {
            this.length = length;
            this.width = width;
        }

        public override double area()
        {
            return length * width * 0.5;
        }

        public override string show()
        {
            return "Triangle " + name + "(" + length + "," + width + ")";
        }

        public override string type()
        {
            return "Triangle";
        }
    }
}
