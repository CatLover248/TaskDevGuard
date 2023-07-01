using Siticone.Desktop.UI.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskDevGaurd
{
    public partial class Form1 : Form
    {

        private bool mouseDown;
        private bool mouseUp;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Opacity = 0.95;
            new Thread(() => check()) {
                IsBackground = true
            }.Start();
        }

        public void check()
        {
            while (true)
            {
                Settings.renamer = siticoneCheckBox1.Checked;
                Settings.strenc = siticoneCheckBox2.Checked;
            }
        }

        private void siticoneGradientButton1_Click(object sender, EventArgs e)
        {
            Protection.execute();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }
    }
}
