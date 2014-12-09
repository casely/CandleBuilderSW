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
        /// Словарь параметров по умолчанию
        /// </summary>
        private Dictionary<CandleParametrs.Params, int> _defaultParametr = new Dictionary<CandleParametrs.Params, int>();
        
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
            bool yesFlag = yesRadioButton.Checked;
            headLength.Visible = yesFlag;
            label2.Visible = yesFlag;
            label3.Visible = yesFlag;
            groupBox1.Size = yesFlag
                ? new System.Drawing.Size(215, 105)
                : new System.Drawing.Size(215, 90);
            _parametr.ExistHead = yesFlag;                  
        }

        /// <summary>
        /// Обработчик кнопки по-умолчанию
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            // Добавление в словарь значений
            AddToDefaultDictionary();

            // Вывод в textbox
            headLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.HeadLength]);
            plinthLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.PlinthLength]);
            nutLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.NutLength]);
            isolatorLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.IsolatorLength]);
            carvingLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.CarvingLength]);
            electrodeLength.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.ElectrodeLength]);
            sizeNut.Text = Convert.ToString(_defaultParametr[CandleParametrs.Params.NutSize]);
            carvingType.Text = "M12x1.25";
            etching.Text = "DENSO";
            yesRadioButton.Checked = true;
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
            _parametr.ElectrodeLength = electrodeLength.SelectedIndex + 1;
            label20.Text = Convert.ToString(3 - electrodeLength.SelectedIndex);
        }

        /// <summary>
        /// Выбор размера гайки
        /// </summary>
        private void ChoiseNutSize()
        {
            _parametr.NutSize = Convert.ToDouble(sizeNut.Text);
            switch (sizeNut.SelectedIndex)
            {
                case 0:
                    _parametr.ChamferRadius = 0.008;
                    break;
                case 1:
                    _parametr.ChamferRadius = 0.0095;
                    break;
                case 2:
                    _parametr.ChamferRadius = 0.01;
                    break;
            }
        }

        /// <summary>
        /// Добавление в словарь значений по умолчанию
        /// </summary>
        /// 
        private void AddToDefaultDictionary()
        {
            _defaultParametr.Add(CandleParametrs.Params.HeadLength, 8);
            _defaultParametr.Add(CandleParametrs.Params.PlinthLength, 30);
            _defaultParametr.Add(CandleParametrs.Params.NutLength, 9);
            _defaultParametr.Add(CandleParametrs.Params.IsolatorLength, 10);
            _defaultParametr.Add(CandleParametrs.Params.CarvingLength, 12);
            _defaultParametr.Add(CandleParametrs.Params.ElectrodeLength, 2);
            _defaultParametr.Add(CandleParametrs.Params.NutSize, 16);
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
