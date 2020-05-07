﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Missile_Falls_Project.Views
{
    public partial class Comments 
    {
        public Comments()
        {
            InitializeComponent();

        }
        private List<string> getCommentsFromUsers = new List<string>();

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;

            if (txtTo.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbTo.Text = "Required";
                txtTo.Focus();
            }
            else if (!Regex.IsMatch(txtTo.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            {
                flag = 0;
                txtbTo.Text = "Invalid";
                txtTo.Focus();
            }
            else
            {
                flag = 1;
                txtbTo.Text = "";
            }
            if (txtSubject.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbSubject.Text = "Required";
                txtSubject.Focus();
            }
            if (txtContent.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbContent.Text = "Required";
                txtContent.Focus();
            }

            if (flag == 1)
            {

                getCommentsFromUsers.Add(Convert.ToString(txtbContent));

                var smtpServerName = ConfigurationManager.AppSettings["SmtpServer"];
                var port = ConfigurationManager.AppSettings["Port"];
                var senderEmailId = ConfigurationManager.AppSettings["SenderEmailId"];
                var senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

                var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmailId, senderPassword),
                    EnableSsl = true
                };
                txtbContent.Text = "Thank you about your share, we hope to see you agein in our app";
                smptClient.Send(senderEmailId, txtTo.Text.Trim(), txtSubject.Text, txtContent.Text);
                MessageBox.Show("Message Sent Successfully");
                txtTo.Text = "";
                txtSubject.Text = "";
                txtContent.Text = "";
                txtTo.Focus();
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTo.Text = "";
            txtSubject.Text = "";
            txtContent.Text = "";
            txtTo.Focus();

            txtbTo.Text = "";
            txtbSubject.Text = "";
            txtbContent.Text = "";
        }
    }
}