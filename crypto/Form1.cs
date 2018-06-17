using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace crypto
{
    public partial class Form1 : Form
    {
        ClassACM acm = new ClassACM();
        string chiave;
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.ReadOnly = true;
            textBox2.Text = "key";
            chiave = textBox2.Text;
            //this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<String> lista = new List<string>();
                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    lista.Add(openFileDialog1.FileNames[i]);
                }
                var log = acm.EncodeFileList(lista, textBox1.Text, chiave);
                foreach (var item in log)
                {
                    listBox2.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.InitialDirectory = ConfigurationManager.AppSettings.Get("LastPath");

            openFileDialog2.Multiselect = true;
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigurationManager.AppSettings.Set("LastPath", Path.GetDirectoryName(openFileDialog2.FileName));
                List<String> lista = new List<string>();
                for (int i = 0; i < openFileDialog2.FileNames.Length; i++)
                {
                    lista.Add(openFileDialog2.FileNames[i]);
                    //MessageBox.Show(openFileDialog2.FileNames[i]);
                }
                bool MD5;
                if (checkBox1.CheckState==CheckState.Checked)
                {
                    MD5 = true;
                }
                else
                {
                    MD5 = false;
                }
                var log = acm.DecodeFileList(MD5,lista, textBox1.Text, chiave);
                foreach (var item in log)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
            FolderBrowserDialog1.SelectedPath = ConfigurationManager.AppSettings.Get("LastDir");

            if (FolderBrowserDialog1.ShowDialog()==DialogResult.OK)
           {
                ConfigurationManager.AppSettings.Set("LastDir", Path.GetDirectoryName(FolderBrowserDialog1.SelectedPath)+"\\"+ Path.GetFileName(FolderBrowserDialog1.SelectedPath));
                string path = FolderBrowserDialog1.SelectedPath;
                textBox1.Text = path;
                button1.Enabled = true;
                button2.Enabled = true;
           }
        }
    }
}
