using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CandleSW
{
    /// <summary>
    /// Класс - форма
    /// </summary>
    public partial class CandleForm : Form
    {
        #region Fields
        /// <summary>
        /// Экземпляр класса CandleParametrs
        /// </summary>
        private CandleParametrs _parametr = new CandleParametrs();

        /// <summary>
        /// Экземпляр класса CandleBuilder
        /// </summary>
        private CandleBuilder _candle = new CandleBuilder();
        
        /// <summary>
        /// Наличие детали
        /// </summary>
        private bool _checkDetail = false;

        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор формы
        /// </summary>
        public CandleForm()
        {
            InitializeComponent();
            // Скрытие label
            LabelHiding();
            // Задаем максимальную длину в гравировке
            etching.MaxLength = 10;
        }

        #endregion

        #region FormMethods
        /// <summary>
        /// Обработчик нажатия на кнопку построить
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
                ReadTextBox();
                if (_checkDetail == true)
                {
                    if (_parametr.ExistDetail)
                    {
                        _candle.SwApp.CloseDoc("Свеча");
                    }
                    _candle.BuildCandle(_parametr);
                }
        }

        /// <summary>
        /// Обработчик выбора
        /// </summary>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (yesRadioButton.Checked)
            {
                headLength.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                groupBox1.Size = new System.Drawing.Size(215, 105);
                _parametr.ExistHead = true;
            }

            if (noRadioButton.Checked)
            {
                headLength.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                groupBox1.Size = new System.Drawing.Size(215, 90);
                _parametr.ExistHead = false;
            }
        }

        /// <summary>
        /// Обработчик кнопки по-умолчанию
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
           
            //List<TextBox> obj = new List<TextBox> { headLength, plinthLength };
            //Initialize();
            //TODO: По умолчанию хранить как структуру
            headLength.Text= "8";
            plinthLength.Text = "30";
            nutLength.Text = "9";
            isolatorLength.Text = "8";
            carvingLength.Text = "12";
            electrodeLength.Text = "2";
            sizeNut.Text = "16";
            carvingType.Text = "M12x1.25";
            etching.Text = "DENSO";
            yesRadioButton.Checked = true;
        }

        //public Dictionary<TextBox, string> Parameters { get; private set; }

        private void Initialize()
        {
            

        }

        /// <summary>
        /// Выбор размера гайки
        /// </summary>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ChoiseNutSize();
        }

        /// <summary>
        /// Обработка нажатия на закрытие формы
        /// </summary>
        private void CandleForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                _candle.SwApp.ExitApp();
                _candle.SwApp = null;
                Process[] processes = Process.GetProcessesByName("SLDWORKS");
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Выбор типа резьбы
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChooseTypeOfCarving();
        }

        /// <summary>
        /// Выбор длины электрода
        /// </summary>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label20.Visible = true;
            label18.Visible = true;
            label17.Visible = true;
            ChooseLengthOfElectrode();
            
        }

        #endregion

        #region Methods
        /// <summary>
        /// Метод считывающий данные с полей
        /// </summary>
        private void ReadTextBox()
        {
            try
            {
                _parametr.CarvingLength = Convert.ToDouble(carvingLength.Text);
                _parametr.NutLength = Convert.ToDouble(nutLength.Text);
                _parametr.IsolatorLength = Convert.ToDouble(isolatorLength.Text);
                _parametr.PlinthLength = Convert.ToDouble(plinthLength.Text);
                if (_parametr.ExistHead == true)
                {
                    _parametr.HeadLength = Convert.ToDouble(headLength.Text);
                }
                _parametr.TextEtching = Convert.ToString(etching.Text);
                _checkDetail = true;

            }
            catch (Exception e)
            {
                DialogResult res = MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                    _checkDetail = false;
            }
        }

        /// <summary>
        /// Выбор типа резьбы
        /// </summary>
        private void ChooseTypeOfCarving()
        {
            switch (carvingType.SelectedIndex)
            {
                case 0:
                    _parametr.CarvingRadius = 5;
                    _parametr.PitchSize = 1.25;
                    break;
                case 1:
                    _parametr.CarvingRadius = 5;
                    _parametr.PitchSize = 1.5;
                    break;
            }
        }

        /// <summary>
        /// Выбор длины электрода
        /// </summary>
        private void ChooseLengthOfElectrode()
        {
            switch (electrodeLength.SelectedIndex)
            {
                case 0:
                    _parametr.ElectrodeLength = 1;
                    label20.Text = "3";
                    break;
                case 1:
                    _parametr.ElectrodeLength = 2;
                    label20.Text = "2";
                    break;
                case 2:
                    _parametr.ElectrodeLength = 3;
                    label20.Text = "1";
                    break;
            }
        }

        /// <summary>
        /// Выбор размера гайки
        /// </summary>
        private void ChoiseNutSize()
        {
            switch (sizeNut.SelectedIndex)
            {
                case 0:
                    _parametr.NutSize = Convert.ToDouble(sizeNut.Text);
                    _parametr.ChamferRadius = 0.008;
                    break;
                case 1:
                    _parametr.NutSize = Convert.ToDouble(sizeNut.Text);
                    _parametr.ChamferRadius = 0.0095;
                    break;
                case 2:
                    _parametr.NutSize = Convert.ToDouble(sizeNut.Text);
                    _parametr.ChamferRadius = 0.01;
                    break;
            }
        }

        /// <summary>
        /// Скрытие надписей
        /// </summary>
        private void LabelHiding()
        {
            // Скрываем label
            label20.Visible = false;
            label18.Visible = false;
            label17.Visible = false;
        }

        #endregion

    }
}
