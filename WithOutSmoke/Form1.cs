// раскидать по классам
// добавить чат
// сделать сон
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static WithOutSmoke.Globals;
using static WithOutSmoke.Utils;

namespace WithOutSmoke
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void WriteData()
        {
            if (dTPWithout.Value <= DateTime.Now)
            {
                Properties.Settings.Default.DateTimeQuit = new DateTime(dTPWithout.Value.Year, dTPWithout.Value.Month, dTPWithout.Value.Day, dTPWithout.Value.Hour, dTPWithout.Value.Minute, 0);
                Properties.Settings.Default.ForDay = Convert.ToInt32(nUDForDay.Text);
                Properties.Settings.Default.InPack = Convert.ToInt32(nUDInPack.Text);
                Properties.Settings.Default.TimeToCig = Convert.ToInt32(nUDTimeToCig.Text);
                Properties.Settings.Default.Puffs = Convert.ToInt32(nUDPuffs.Text);
                Properties.Settings.Default.Breath = Convert.ToDouble(nUDBreathTime.Text);
                Properties.Settings.Default.InAshtray = Convert.ToInt32(nUDAshtray.Text);
                Properties.Settings.Default.Resin = Convert.ToDouble(nUDResin.Text);
                Properties.Settings.Default.Nicotine = Convert.ToDouble(nUDNicotine.Text);
                Properties.Settings.Default.CarbonMonoxide = Convert.ToDouble(nUDCarbonMonoxide.Text);
                Properties.Settings.Default.StartPrice = Convert.ToDouble(nUDStartPrice.Text);
                Properties.Settings.Default.EndPrice = Convert.ToDouble(nUDEndPrice.Text);
                Properties.Settings.Default.HowOld = Convert.ToDouble(nUDHowOld.Text);
                Properties.Settings.Default.Currency = cBCurrency.Text;
                Properties.Settings.Default.StartMinimized = cBHide.Checked;
                Properties.Settings.Default.HideToTray = cBHideToTray.Checked;
                Properties.Settings.Default.Save();
            } else throw new Exception("Выбрано некоректное значение даты или времени!");
        }

        public void ReadData()
        {
            day = Properties.Settings.Default.DateTimeQuit.Day;
            month = Properties.Settings.Default.DateTimeQuit.Month;
            year = Properties.Settings.Default.DateTimeQuit.Year;
            hour = Properties.Settings.Default.DateTimeQuit.Hour;
            minute = Properties.Settings.Default.DateTimeQuit.Minute;

            forDay = Properties.Settings.Default.ForDay;
            inPack = Properties.Settings.Default.InPack;
            inAshtray = Properties.Settings.Default.InAshtray;
            timeToCig = Properties.Settings.Default.TimeToCig;
            puffs = Properties.Settings.Default.Puffs;
            breath = Properties.Settings.Default.Breath;
            resin = Properties.Settings.Default.Resin;
            nicotine = Properties.Settings.Default.Nicotine;
            carbonMonoxide = Properties.Settings.Default.CarbonMonoxide;
            startPrice = Properties.Settings.Default.StartPrice;
            endPrice = Properties.Settings.Default.EndPrice;
            howOld = Properties.Settings.Default.HowOld;
            currency = Properties.Settings.Default.Currency;
            startMinimized = Properties.Settings.Default.StartMinimized;
            checkUpdate = Properties.Settings.Default.CheckUpdate;
            hideTray = Properties.Settings.Default.HideToTray;

            DateWithout = new DateTime(year, month, day, hour, minute, 0);
        }

        public void SetSettingsFields() // установка значений в поля настроек
        {
            dTPWithout.Value = DateWithout;
            nUDForDay.Text = forDay.ToString();
            nUDInPack.Text = inPack.ToString();
            nUDTimeToCig.Text = timeToCig.ToString();
            nUDPuffs.Text = puffs.ToString();
            nUDBreathTime.Text = breath.ToString(CultureInfo.CurrentCulture);
            nUDAshtray.Text = inAshtray.ToString();
            nUDResin.Text = resin.ToString(CultureInfo.CurrentCulture);
            nUDNicotine.Text = nicotine.ToString(CultureInfo.CurrentCulture);
            nUDCarbonMonoxide.Text = carbonMonoxide.ToString(CultureInfo.CurrentCulture);
            nUDStartPrice.Text = startPrice.ToString(CultureInfo.CurrentCulture);
            nUDEndPrice.Text = endPrice.ToString(CultureInfo.CurrentCulture);
            nUDHowOld.Text = howOld.ToString(CultureInfo.CurrentCulture);
            
            cBHide.Checked = startMinimized;
            cBHideToTray.Checked = hideTray;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ReadData();
                SetStaticData();
                SetSettingsFields();
                
                if (startMinimized) WindowState = FormWindowState.Minimized;
                if (checkUpdate) CheckUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения данных.\r\n" + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            rTBInfo.Rtf = GetFileName();
            richTextBox1.Rtf = Properties.Resources.desease; // файл с болезнями
            richTextBox2.Rtf = Properties.Resources.benefit; // файл с пользой отказа
            richTextBox3.Rtf = Properties.Resources.advice; // файл с советами
            richTextBox4.Rtf = Properties.Resources.history; // файл с историей табака
            richTextBox5.Rtf = Properties.Resources.facts; // файл с фактами о курении
            richTextBox6.Rtf = Properties.Resources.composition; // файл состав дыма

            cBCurrency.Text = cBCurrency.Items[0].ToString();
        }

        public void SetStaticData()
        {
            lblSmokeIndex.Text = (forDay * howOld / 20).ToString(CultureInfo.CurrentCulture);
            lblSmokedBreath.Text = $@"{Math.Truncate(puffs * forDay * 365 * howOld)} шт.";
            lblDeathPeopleForSmoked.Text = $@"{365 * 86400 * howOld / 6} чел.";
            lblSmokedCigs.Text = $@"{Math.Truncate(365 * howOld * forDay)} шт."; // кол-во сиг за период курения
            lblSmokedPacks.Text = $@"{Math.Truncate(365 * howOld * forDay / inPack)} шт.";
            lblFilledAshtray.Text = $@"{Math.Truncate(365 * howOld * forDay / inAshtray)} шт."; 
            lblWastedMoney.Text = $@"{Math.Round((startPrice + endPrice) / 2 / inPack * 365 * howOld * forDay, 2)} {currency}"; // потрачено денег за период курения
            lblWastedTime.Text = $@"{GetWasteSaveTime(365 * howOld * forDay * timeToCig)}"; // потрачено денег за период курения
            lblBreathedResin.Text = $@"{Math.Round(forDay * 365 * resin * howOld / 1000, 2)} гр.";
            lblBreathedNicotine.Text = $@"{Math.Round(forDay * 365 * nicotine * howOld / 1000, 2)} гр.";
            lblBreathedMonoxide.Text = $@"{Math.Round(forDay * 365 * carbonMonoxide * howOld / 1000, 2)} гр.";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SubtractDays = DateTime.Now.Subtract(DateWithout).Days;
            SubtractHours = DateTime.Now.Subtract(DateWithout).Hours;
            SubtractMinutes = DateTime.Now.Subtract(DateWithout).Minutes;
            SubtractSeconds = DateTime.Now.Subtract(DateWithout).Seconds;

            lblDateTimeWithout.Text = GetWithoutTime();
            lblDateTimeQuit.Text = DateWithout.ToString("dd.MM.yy HH:mm");
            lblDeathPeopleForQuit.Text = $@"{Math.Truncate((SubtractDays * 86400 + SubtractHours * 3600 + SubtractMinutes * 60 + SubtractSeconds) / 6.0)} чел.";
            lblClearBreath.Text = $@"{Math.Truncate((SubtractDays * 86400 + SubtractHours * 3600 + SubtractMinutes * 60 + SubtractSeconds) / breath)} шт.";
            // сколько прошло времени с момента отказа от курения
            var timeElapsed = SubtractDays + DayInHour * SubtractHours + DayInMinute * SubtractMinutes;
            var cigSmoked = Math.Truncate(forDay * timeElapsed); // выкурено сигарет за период некурения

            lblNotSmokedCigs.Text = $@"{cigSmoked} шт."; // выкурено сигарет за период некурения
            lblNotSmokedPacks.Text = $@"{Math.Truncate(cigSmoked / inPack)} шт.";
            lblNotFilledAshtray.Text = $@"{Math.Truncate(cigSmoked / inAshtray)} шт.";
            lblSavedMoney.Text = $@"{Math.Round(endPrice / inPack * cigSmoked, 2)} {currency}"; // сэкономлено денег за период некурения
            lblSavedTime.Text = $@"{GetWasteSaveTime(timeToCig * cigSmoked)}";
            lblNotBreathedResin.Text = resin * cigSmoked / 1000 < 1 ? $"{resin * cigSmoked} мг." : $"{Math.Round(resin * cigSmoked / 1000, 2)} гр.";
            lblNotBreathedNicotine.Text = nicotine * cigSmoked / 1000 < 1 ? $"{nicotine * cigSmoked} мг." : $"{Math.Round(nicotine * cigSmoked / 1000, 2)} гр.";
            lblNotBreathedMonoxide.Text = carbonMonoxide * cigSmoked / 1000 < 1 ? $"{carbonMonoxide * cigSmoked} мг." : $"{Math.Round(carbonMonoxide * cigSmoked / 1000, 2)} гр.";

            notifyIcon1.Text = @"Вы не курите уже " + GetWithoutTime();
        }

        public string GetWithoutTime()
        {
            var resultTime = "";
            if (SubtractDays >= 7)
            {
                var weekCount = (int) Math.Truncate(SubtractDays / 7.0);
                resultTime = weekCount + " нед. ";
                if (SubtractDays - weekCount * 7 != 0) resultTime += SubtractDays - weekCount * 7 + " дн. ";
            }
            else if (SubtractDays != 0) resultTime = SubtractDays + " дн. ";
            if (SubtractHours != 0) resultTime += SubtractHours + " час. ";
            if (SubtractMinutes != 0) resultTime += SubtractMinutes + " мин. ";
            if (SubtractSeconds != 0) resultTime += SubtractSeconds + " сек. ";
            return resultTime;
        }

        public string GetWasteSaveTime(double timeNeed)
        {
            var resultTime = "";
            var saveDays = TimeSpan.FromMinutes(timeNeed).Days;
            var saveHours = TimeSpan.FromMinutes(timeNeed).Hours;
            var saveMinutes = TimeSpan.FromMinutes(timeNeed).Minutes;
            if (saveDays != 0) resultTime = saveDays + " д. ";
            if (saveHours != 0) resultTime += saveHours + " ч. ";
            if (saveMinutes != 0) resultTime += saveMinutes + " м.";
            if (resultTime == "") resultTime = "0 м.";
            return resultTime;
        }

        public string GetFileName()
        { // Сравнение дней без сигарет и выбор пути к файлу с информацией
            switch (SubtractDays)
            {
                case 0: return Properties.Resources._1;
                case 1: return Properties.Resources._2;
                case 2: return Properties.Resources._3;
                case 3: return Properties.Resources._4;
                case 4: return Properties.Resources._5;
                case 5: return Properties.Resources._6;
                case 6: return Properties.Resources._7;
                case 7: return Properties.Resources._8;
                case 8: return Properties.Resources._9;
                case 9: return Properties.Resources._10;
                case 10: return Properties.Resources._11;
                case 11: return Properties.Resources._12;
                case 12: return Properties.Resources._13;
                case 13: return Properties.Resources._14;
            }
            if (SubtractDays >= 14 && SubtractDays <= 30) return Properties.Resources._1m;
            if (SubtractDays >= 31 && SubtractDays <= 60) return Properties.Resources._2m;
            if (SubtractDays >= 61 && SubtractDays <= 90) return Properties.Resources._3m;
            if (SubtractDays >= 91 && SubtractDays <= 120) return Properties.Resources._4m;
            if (SubtractDays >= 121 && SubtractDays <= 150) return Properties.Resources._5m;
            if (SubtractDays >= 151 && SubtractDays <= 180) return Properties.Resources._6m;
            if (SubtractDays >= 181 && SubtractDays <= 210) return Properties.Resources._7m;
            if (SubtractDays >= 211 && SubtractDays <= 240) return Properties.Resources._8m;
            if (SubtractDays >= 241 && SubtractDays <= 270) return Properties.Resources._9m;
            if (SubtractDays >= 271 && SubtractDays <= 300) return Properties.Resources._10m;
            if (SubtractDays >= 301 && SubtractDays <= 330) return Properties.Resources._11m;
            return Properties.Resources._12m;
        }

        private void lblWithoutValue_TextChanged(object sender, EventArgs e)
        {
            rTBInfo.Rtf = GetFileName(); // календарь
            GetAllProgressValue();


            GetSmokeStatistic(lblLevel, 1440); // сколько угар в организме
            GetSmokeStatistic(label91, 2880); // сколько никотина в организме
            GetSmokeStatistic(label32, 30240); // физ зависимость
            GetSmokeStatistic(label16, 172800); // псих зависимость
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                WriteData();
                ReadData();
                SetStaticData();
                SetSettingsFields();
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            contextMenuStrip1.Items[0].Text = WindowState == FormWindowState.Minimized ? "Показать" : "Скрыть";
            if (WindowState == FormWindowState.Minimized) HideForm();
        }

        public void GetSmokeStatistic(Label lbl, double minutes)
        {
            var value = Math.Round(100 - (Convert.ToDouble(SubtractDays * 24 + SubtractHours) * 60 + Convert.ToDouble(SubtractMinutes)) / minutes * 100, 1);
            if (value > 50)
            {
                lbl.Text = $@"{value}%";
                lbl.ForeColor = Color.Red;
            }
            else if (value > 0)
            {
                lbl.Text = $@"{value}%";
                lbl.ForeColor = Color.Orange;
            }
            else
            {
                lbl.Text = @"0%";
                lbl.ForeColor = Color.Green;
            }
        }

        public void GetProgressValue(ProgressBar pBar, Label lbl, double hours)
        {
            var value = Math.Round((Convert.ToDouble(SubtractDays * 24 + SubtractHours) * 60 + Convert.ToDouble(SubtractMinutes)) / (hours * 60) * 100, 1);
            if (value < 100)
            {
                pBar.Value = Convert.ToInt32(value);
                lbl.Text = $@"{value}%";
            }
            else
            {
                pBar.Value = 100;
                lbl.Text = @"100%";
            }
        }

        public void GetAllProgressValue()
        {
            GetProgressValue(progressBar1, label27, 1);
            GetProgressValue(progressBar2, label12, 2);
            GetProgressValue(progressBar4, label29, 4);
            GetProgressValue(progressBar3, label33, 6); 
            GetProgressValue(progressBar6, label41, 8);
            GetProgressValue(progressBar5, label44, 10);
            GetProgressValue(progressBar8, label46, 12);
            GetProgressValue(progressBar7, label47, 24);
            GetProgressValue(progressBar9, label48, 36);
            GetProgressValue(progressBar10, label36, 48);
            GetProgressValue(progressBar11, label51, 72);
            GetProgressValue(progressBar12, label56, 96);
            GetProgressValue(progressBar13, label60, 120);
            GetProgressValue(progressBar14, label62, 144);
            GetProgressValue(progressBar15, label64, 168);
            GetProgressValue(progressBar16, label66, 336);
            GetProgressValue(progressBar17, label68, 504);
            GetProgressValue(progressBar18, label70, 672);
            GetProgressValue(progressBar19, label8, 1344);
            GetProgressValue(progressBar20, label10, 2016);
            GetProgressValue(progressBar21, label13, 2688);
            GetProgressValue(progressBar22, label15, 3360);
            GetProgressValue(progressBar23, label30, 4032);
            GetProgressValue(progressBar24, label37, 4704);
            GetProgressValue(progressBar25, label42, 5376);
            GetProgressValue(progressBar26, label57, 6048);
            GetProgressValue(progressBar27, label71, 6720);
            GetProgressValue(progressBar28, label73, 7392);
            GetProgressValue(progressBar30, label6, 8064);
            GetProgressValue(progressBar35, label23, 16128);
            GetProgressValue(progressBar34, label14, 24192);
            GetProgressValue(progressBar33, label11, 32256);
            GetProgressValue(progressBar32, label9, 40320);
            GetProgressValue(progressBar31, label7, 80640);
            GetProgressValue(progressBar29, label75, 120960);
        }

        private void ShowInfoForm(string diseaseRtf)
        {
            var dForm = new DiseaseForm {Owner = this};
            dForm.LoadRtfFile(diseaseRtf);
            dForm.ShowDialog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        public void ShowForm()
        {
            WindowState = FormWindowState.Normal;
            if (hideTray) ShowInTaskbar = true;
        }

        public void HideForm()
        {
            WindowState = FormWindowState.Minimized;
            if (hideTray) ShowInTaskbar = false;
        }

        private void показатьскрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) ShowForm();
            else HideForm();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 0;
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 1;
        }

        private void организмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 2;
        }

        private void достиженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 3;
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 4;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            tabControl1.SelectedIndex = 5;
        }

        private void btnReportBug_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.cc/5ffgWy");
        }
        
        private void lnkLblVk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://vk.cc/6hg48u");
        }

        #region ShowInfoForm
        private void linkLabel84_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources._5things); // 5 вещей
        }

        private void linkLabel80_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.weight); // влияет ли курение на вес?
        }

        private void linkLabel86_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.statistic); // статистика курения
        }

        private void linkLabel78_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.lightorhard); // какие сигареты вреднее?
        }

        private void linkLabel76_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.whonhow); // кто и как заставляет нас курить?
        }

        private void linkLabel74_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.withalc); // курение и алкоголь
        }

        private void linkLabel83_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.withsport); // курение и спорт
        }

        private void linkLabel75_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.hookah); // курение кальяна
        }

        private void linkLabel82_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.passive); // пассивное курение
        }

        private void linkLabel79_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.nicotine); // вред никотина
        }

        private void linkLabel81_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.whyuthink); // почему вы думаете, что вам повезет?
        }

        private void linkLabel85_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInfoForm(Properties.Resources.smokendeath); // курение и смерть
        }
        #endregion

        #region toolTipMessage
        private void label98_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label98, "Дата и время отказа от сигарет.");
        }

        private void label97_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label97, "Количество времени, прошедшее с Вашей последней сигареты.");
        }

        private void label102_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label102, "Складывается из количества сигарет в день и периода курения.\nЕсли индекс больше 10, то риск развития ХОБЛ очень высок и нужно срочно бросать курить.");
        }

        private void label86_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(lblMonoxide, "Количество угарного газа в организме с последней сигареты.");
        }

        private void label89_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(lblNicotine, "Количество никотина в организме с последней сигареты.");
        }
        #endregion
    }
}