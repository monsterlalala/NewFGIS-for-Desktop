﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 进度条对话框
    /// </summary>
    public class CircleProgressBox
    {
        private CircleProgress m_frmProgressBox = null;
        private Thread m_thread = null;

        /// <summary>
        /// 构造
        /// </summary>
        public CircleProgressBox()
        {

        }

        /// <summary>
        /// 显示进度条
        /// </summary>
        public void ShowPregress()
        {
            m_thread = new Thread(Show);
            m_thread.IsBackground = true;
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
        }

        private void Show()
        {
            m_frmProgressBox = new CircleProgress();
            m_frmProgressBox.ShowDialog();
        }

        /// <summary>
        /// 关闭进度条
        /// </summary>
        public void CloseProgress()
        {
            while (!m_thread.IsAlive) ;
            Thread.Sleep(1);
            m_frmProgressBox.RequestStop();
            m_thread.Join();
        }

        /// <summary>
        /// 设置进度条当前描述信息
        /// </summary>
        /// <param name="strValue"></param>
        public void SetProgressDescription(string strValue)
        {
            while (m_frmProgressBox == null)
            { }
            m_frmProgressBox.SetProgressText(strValue);
        }

        public void SetDefaultDescription()
        {
            while (m_frmProgressBox == null)
            { }
            string[] text = new string[] { "Loading", "Loading.", "Loading..", "Loading...",
                "Loading", "Loading.", "Loading..", "Loading...",
                "Loading", "Loading.", "Loading..", "Loading...",
                "Loading", "Loading.", "Loading..", "Loading..."};
            int i = 0;
            Random r = new Random();
            int ran = r.Next(8, 14);
            while(i < ran)
            {
                m_frmProgressBox.SetDefaultDescription(text[i]);
                Thread.Sleep(300);
                i++;
            }
        }
    }
}
