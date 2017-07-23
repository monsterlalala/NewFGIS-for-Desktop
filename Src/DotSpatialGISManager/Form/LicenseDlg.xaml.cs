﻿using Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DotSpatialGISManager
{
    /// <summary>
    /// LicenseDlg.xaml 的交互逻辑
    /// </summary>
    public partial class LicenseDlg : Window, INotifyPropertyChanged
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "License.dat";
        private bool _IsCorrect;
        public LicenseDlg()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public bool IsCorrect
        {
            get
            {
                return _IsCorrect;
            }
            set
            {
                if (_IsCorrect!=value)
                {
                    _IsCorrect = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsCorrect"));
                        if (_IsCorrect)
                        {
                            image = PathHelper.ResourcePath + "Ok.png";
                            toolTip = "Correct";
                        }
                        else
                        {
                            image = PathHelper.ResourcePath + "Cancel.png";
                            toolTip = "Error";
                        }
                    }
                }
            }
        }

        private string _image;
        public string image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image!=value)
                {
                    _image = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("image"));
                    }
                }
            }
        }

        private string _toolTip;
        public string toolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if (_toolTip != value)
                {
                    _toolTip = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("toolTip"));
                    }
                }
            }
        }

        private Visibility _visibility;
        public Visibility visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                if (_visibility!=value)
                {
                    if (_visibility!=value)
                    {
                        _visibility = value;
                        if (this.PropertyChanged != null)
                        {
                            this.PropertyChanged(this, new PropertyChangedEventArgs("visibility"));
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Stream s = File.Open(path, FileMode.Create);//创建a.bat文件 如果之前错在a.bat文件则覆盖，无则创建
            BinaryFormatter b = new BinaryFormatter();//创建一个序列化的对象
            string Date = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd hh:mm:ss");//使用期限1年
            b.Serialize(s, Date);//将数据序列化后给s
            s.Close();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void txtFieldName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime Now = DateTime.Now;
            DateTime Last = Now.AddMinutes(-1);
            string resultNow = Common.MD5Encryption.MD5Encode(Now.ToShortDateString() + " " + Now.ToShortTimeString());
            string resultLast = Common.MD5Encryption.MD5Encode(Last.ToShortDateString() + " " + Last.ToShortTimeString());
            if (this.txtFieldName.Text == resultNow||this.txtFieldName.Text == resultLast)
            {
                IsCorrect = true;
            }
            else
            {
                IsCorrect = false;
            }
        }
    }
}
