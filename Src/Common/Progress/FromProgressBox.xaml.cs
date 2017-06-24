﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Common
{
    /// <summary>
    /// ProgressBox.xaml 的交互逻辑
    /// </summary>
    public partial class FromProgressBox : Window
    {
        private bool m_bShowDescription = true;
        private bool m_bShowProgressNumber = true;
        private volatile bool _shouldStop;
        public FromProgressBox()
        {
            InitializeComponent();
        }

       

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            get { return this.Title.ToString(); }
            set { this.Title = value; }
        }

        /// <summary>
        /// 是否显示描述信息
        /// </summary>
        public bool ShowDescription
        {
            get { return m_bShowDescription; }
            set { m_bShowDescription = value; }
        }

        /// <summary>
        /// 是否显示进度数量
        /// </summary>
        public bool ShowProgressNumber
        {
            get { return m_bShowProgressNumber; }
            set { m_bShowProgressNumber = value; }
        }

        /// <summary>
        /// 进度条最大值
        /// </summary>
        public double MaxValue
        {
            get { return this.progressBar.Maximum; }
            set { this.progressBar.Maximum = value; }
        }

        /// <summary>
        /// 进度条最小值
        /// </summary>
        public double MinValue
        {
            get { return this.progressBar.Minimum; }
            set { this.progressBar.Minimum = value; }
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        
        private delegate void ShowLogLabelDelegate(string strLog);
        /// <summary>
        /// 设置进度条描述信息
        /// </summary>
        /// <param name="strLog"></param>
        public void SetProgressText(string strLog)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new ShowLogLabelDelegate(SetProgressText), strLog);
                return;
            }
            this.lblDescirption.Content = strLog;
        }

        public void SetProgressText2(string strLog)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new ShowLogLabelDelegate(SetProgressText2), strLog);
                return;
            }
            this.lblDescription2.Content = strLog;
        }

        private delegate void SetProgressValueDelegate(double nValue);
        /// <summary>
        /// 设置进度条进度值
        /// </summary>
        /// <param name="nValue"></param>
        public void SetProgressValue(double nValue)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new SetProgressValueDelegate(SetProgressValue), nValue);
                return;
            }
            if (nValue > MaxValue)
                nValue = (int)MaxValue;
            nValue = Math.Round(nValue, 2);
            this.progressBar.Value = nValue;
            this.lblPercent.Content = nValue + "%";
        }

        private delegate void CloseDelegate();
        /// <summary>
        /// 关闭
        /// </summary>
        public void RequestStop()
        {
            _shouldStop = true;
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new CloseDelegate(Close));
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
