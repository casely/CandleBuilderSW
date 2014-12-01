using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CandleSW
{
    public partial class CandleForm : Form
    {

        CandleParametrs _parametr = new CandleParametrs();
        CandleBuilder _candle = new CandleBuilder();
        bool _existDetail = false;

        /// <summary>
        /// Form constructor
        /// </summary>
        public CandleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create button handler
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            
            ReadTextBox();
            if (_existDetail == true)
                _candle.BuildCandle(_parametr);
        }

        /// <summary>
        /// Read from textbox method
        /// </summary>
        private void ReadTextBox()
        {
            try
            {
                _parametr.CarvingLength = Convert.ToDouble(textBox5.Text);
                _parametr.NutLength = Convert.ToDouble(textBox3.Text);
                _parametr.IsolatorLength = Convert.ToDouble(textBox4.Text);
                _existDetail = true;
            }
            catch (Exception e)
            {
                DialogResult res = MessageBox.Show(e.Message,"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                    _existDetail = false;
            }
        }

        /// <summary>
        /// RB handlers
        /// </summary>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                groupBox1.Size = new System.Drawing.Size(215, 105);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                groupBox1.Size = new System.Drawing.Size(215, 90);
            }
        }

        /// <summary>
        /// Default parametrs button handler
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text= "5";
            textBox2.Text = "30";
            textBox3.Text = "5";
            textBox4.Text = "8";
            textBox5.Text = "12";
            comboBox1.Text = "16";
        }

        /// <summary>
        /// Choose nut size handler
        /// </summary>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    break;
                case 1:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    break;
                case 2:
                    _parametr.NutSize = Convert.ToDouble(comboBox1.Text);
                    break;
            }
        }
    }
}
