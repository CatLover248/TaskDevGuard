﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskDevGaurd
{
    public partial class Form1 : Form
    {
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
                Settings.renamer = checkBox1.Checked;
                Settings.strenc = checkBox2.Checked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Protection.execute(); 
        }
    }
}
