using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;


namespace Weather1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string APIKey = "4e6dfa038110770781a4a16c89a6f377";

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                getWeather();
            }
            else
            {
                MessageBox.Show("Ошибка, заполните поле", "Ошибка");
            }
        }

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}", textBox1.Text, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                pictureBox1.ImageLocation = "http://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                label2.Text = Info.weather[0].main;                                          //condition
                label4.Text = Info.weather[0].main;                                          //details
                label8.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();          //sunset
                label7.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();         //sunrise
                label10.Text = Info.wind.speed.ToString();                                   //wind
                label11.Text = Info.main.pressure.ToString();                                //presure
                label13.Text = Info.main.temp.ToString();                                    //temp
            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = "";
            label2.Text = "*"; //condition
            label4.Text = "*"; //details
            label8.Text = "*"; //sunset
            label7.Text = "*"; //sunrise
            label10.Text = "*"; //wind
            label11.Text = "*"; //presure
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная программа предназначена для определения определения некоторых метеорологических данных\n\n\nv1.1");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
