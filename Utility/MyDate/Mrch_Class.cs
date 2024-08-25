using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace Utility
{
    public class Mrch_Class
    {

        /// <summary>
        /// تولید کردن کد مخلوط از عدد و رشته بر اساس مقدار ورودی
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetDownloadToken(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strDownloadToken = "";
            Random objRandom = new Random(System.DateTime.Now.Millisecond);
            while (intCount < length)
            {
                intRandomNumber = objRandom.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) &&
                  (intRandomNumber <= intNine) ||
                  (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
                {
                    strDownloadToken = strDownloadToken + (char)intRandomNumber;
                    intCount++;
                }
            }
            return strDownloadToken;
        }


        public static string GetDownloadRequest(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strDownloadToken = "";
            Random objRandom = new Random(System.DateTime.Now.Millisecond);
            while (intCount < length)
            {
                intRandomNumber = objRandom.Next(intZero, 99999999);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine)))
                {
                    strDownloadToken = strDownloadToken + (char)intRandomNumber;
                    intCount++;
                }
            }
            return strDownloadToken;
        }

    }

    public static class Extention
    {

        public static string NumbersToEnglish(this string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }
        public static int GetPersianDaysDiffDate(this string Date1, string Date2, byte type)
        {
            int year1 = Convert.ToInt16(Date1.Substring(0, 4));
            int month1 = Convert.ToInt16(Date1.Substring(5, 2));
            int day1 = Convert.ToInt16(Date1.Substring(8, 2));
            int h1 = Convert.ToInt16(Date1.Substring(11, 2));
            int m1 = Convert.ToInt16(Date1.Substring(14, 2));
            int s1 = Convert.ToInt16(Date1.Substring(17, 2));
            int year2 = Convert.ToInt16(Date2.Substring(0, 4));
            int month2 = Convert.ToInt16(Date2.Substring(5, 2));
            int day2 = Convert.ToInt16(Date2.Substring(8, 2));
            int h2 = Convert.ToInt16(Date2.Substring(11, 2));
            int m2 = Convert.ToInt16(Date2.Substring(14, 2));
            int s2 = Convert.ToInt16(Date2.Substring(17, 2));
            DateTime dt1 = new DateTime(year1, month1, day1, h1, m1, s1, 0);
            DateTime dt2 = new DateTime(year2, month2, day2, h2, m2, s2, 0);
            TimeSpan span = dt2.Subtract(dt1);
            int sh = 0;
            switch (type)
            {
                case 1:
                    sh = span.Days;
                    break;
                case 2:
                    sh = span.Hours;
                    break;
                case 3:
                    sh = span.Minutes;
                    break;
                case 4:
                    sh = span.Seconds;
                    break;
                default:
                    return 0;
            }
            return sh;
        }



        public static string GetShortDate(this DateTime _datenoew)
        {
            return _datenoew.ToString("yyyy/MM/dd");
        }
        public static string GetRandomNum(this int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intCount = 0;
            int intRandomNumber = 0;
            string strDownloadToken = "";
            Random objRandom = new Random(System.DateTime.Now.Millisecond);
            while (intCount < length)
            {
                intRandomNumber = objRandom.Next(intZero, 99999999);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine)))
                {
                    strDownloadToken = strDownloadToken + intRandomNumber;
                    intCount++;
                }
            }
            return strDownloadToken;
        }
        static Regex ValidEmailRegex = CreateValidEmailRegex();

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailIsValid(this string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }
        public static string FormatByteSize(this decimal bytes)
        {
            try
            {
                string[] Suffix = { "B", "KB", "MB", "GB", "PB", "EB", "ZB", "YB" };
                int index = 0;
                do { bytes /= 1024; index++; }
                while (bytes >= 1024);
                return String.Format("{0:0.00} {1}", bytes, Suffix[index]);
            }
            catch { return "خطا!"; }
        }
        public static string RandomNumber(this int length)
        {
            int intZero = '0';
            int intNine = '9';

            int intCount = 0;
            int intRandomNumber = 0;
            string strDownloadToken = "";
            string val = System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Millisecond.ToString();
            Random objRandom = new Random(int.Parse(val));
            while (intCount < length)
            {
                intRandomNumber = objRandom.Next(intZero, 99999999);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine)))
                {
                    strDownloadToken = strDownloadToken + (char)intRandomNumber;
                    intCount++;
                }
            }
            return strDownloadToken;
        }
        public static bool IsMobile(this string input)
        {
            const string pattern = @"^9[1|3][0-9]{8}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }

        public static bool EvaluateIsValid(this string ControlToValidate)
        {
            string val = ControlToValidate;
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match = Regex.Match(val.Trim(), pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return true;
            else
                return false;
        }


        //convert shamsi to miladi
        public static string Shamsi2Miladi(this string _date)
        {
            int year = int.Parse(_date.Substring(0, 4));
            int month = int.Parse(_date.Substring(5, 2));
            int day = int.Parse(_date.Substring(8, 2));
            PersianCalendar p = new PersianCalendar();
            DateTime date = new DateTime(year, month, day, 0, 0, 0, 0, p);
            return date.ToString("yyyy/MM/dd");
        }

        public static string ToPersionFull(this DateTime dt)
        {
            try
            {
                if (dt != null)
                {

                    Class_date cdate = new Class_date();
                    string date = dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString();

                    return cdate.Shamsi(date) + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
                }
                else
                {
                    return "فرمت تاریخ نامتعبر";
                }
            }
            catch { return ""; }
        }

        public static string ConvertDate(this string date, string lan)
        {
            if (lan.ToLower() == "fa")
                return DateTime.Parse(date).ToPersionFull();
            return date;
        }


        public static object[] DateMiladi(this DateTime dt)
        {
            try
            {
                DateTime dtper = DateTime.Parse(dt.ToPersion());
                DateTime dtm = new DateTime(dtper.Year, dtper.Month, dtper.Day, dt.Hour, dt.Minute, dt.Second, new PersianCalendar());
                object[] tm = { dtm.ToString("yyyy/MM/dd HH:mm:ss"), true };
                return tm;
            }
            catch
            {
                object[] tm = { "", false };
                return tm;
            }

        }


        public static string DateMiladiME(this string dt)
        {
            try
            {
                string date = dt.Split(' ')[0];
                string time = date.Shamsi2Miladi() + " " + dt.Split(' ')[1];

                return time;
            }
            catch
            {
                return "";
            }

        }




        /// <summary>
        /// Validates the string is an Email Address...
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>bool</returns>
        public static bool IsValidEmailAddress(this string emailAddress)
        {
            var valid = true;
            var isnotblank = false;

            var email = emailAddress.Trim();
            if (email.Length > 0)
            {
                isnotblank = true;
                valid = Regex.IsMatch(email, @"\A([\w!#%&'""=`{}~\.\-\+\*\?\^\|\/\$])+@{1}\w+([-.]\w+)*\.\w+([-.]\w+)*\z", RegexOptions.IgnoreCase) &&
                    !email.StartsWith("-") &&
                    !email.StartsWith(".") &&
                    !email.EndsWith(".") &&
                    !email.Contains("..") &&
                    !email.Contains(".@") &&
                    !email.Contains("@.");
            }

            return (valid && isnotblank);
        }

        /// <summary>
        /// Validates the string is an Email Address or a delimited string of email addresses...
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>bool</returns>
        public static bool IsValidEmailAddressDelimitedList(this string emailAddress, char delimiter = ';')
        {
            var valid = true;
            var isnotblank = false;

            string[] emails = emailAddress.Split(delimiter);

            foreach (string e in emails)
            {
                var email = e.Trim();
                if (email.Length > 0 && valid) // if valid == false, no reason to continue checking
                {
                    isnotblank = true;
                    if (!email.IsValidEmailAddress())
                    {
                        valid = false;
                    }
                }
            }
            return (valid && isnotblank);
        }
        public static bool CheckMail(this string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                // Response.Write(email + " is correct");
                return true;
            }
            else
            {
                // Response.Write(email + " is incorrect");
                return false;
            }
        }
        /// <summary>
        /// enter only num
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool NumberOnly(this string val)
        {
            try
            {
                int ret = int.Parse(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool NumberOnly(this int val)
        {
            try
            {
                int ret = int.Parse(val.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// enter only num
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool NumberOnlyLong(this string val2)
        {
            try
            {
                long ret = long.Parse(val2);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// استفاده نشود خطا دارد
        /// </summary>
        /// <param name="emi"></param>
        /// <returns></returns>
        public static bool chmail(this string emi)
        {
            //Aspose.Email.Verify.EmailValidator ev = new Aspose.Email.Verify.EmailValidator();
            //Aspose.Email.Verify.ValidationResult result;
            //try
            //{


            //    ev.Validate(emi, ValidationPolicy.MailServer, out result);
            //    if (result.ReturnCode == ValidationResponseCode.ValidationSuccess)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    
            //}
            return false;
        }
        /// <summary>
        ///بدست آوردن نام سایت   
        ///به عنوان مثال اگه مقدار هاست http://www.mrchsoft.com باشد
        ///مقدار mrchsoft را بازمیگرداند
        /// HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        /// </summary>
        /// <param name="host">Request</param>
        /// <returns></returns>
        public static string Siname(this string host)
        {
            try
            {
                int lastDot = host.LastIndexOf('.');
                int secondToLastDot = host.Substring(0, lastDot).LastIndexOf('.');
                if (secondToLastDot > -1)
                    return host.Substring(secondToLastDot + 1);
                else
                    return host;
            }
            catch
            {
                return host;
            }
        }
        #region TimeCalculator
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        /// <summary>
        /// محاصبه فاصله زمانی 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Calculate(this DateTime dateTime, string lan)
        {
            dateTime = DateTime.Parse(dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (lan == "fa")
            {
                if (delta < 1 * MINUTE)
                {
                    return ts.Seconds == 1 ? "لحظه ای قبل" : ts.Seconds + " ثانیه قبل";
                }
                if (delta < 2 * MINUTE)
                {
                    return "یک دقیقه قبل";
                }
                if (delta < 45 * MINUTE)
                {
                    return ts.Minutes + " دقیقه قبل";
                }
                if (delta < 90 * MINUTE)
                {
                    return "یک ساعت قبل";
                }
                if (delta < 24 * HOUR)
                {
                    return ts.Hours + " ساعت قبل";
                }
                if (delta < 48 * HOUR)
                {
                    return "دیروز";
                }
                if (delta < 30 * DAY)
                {
                    return ts.Days + " روز قبل";
                }
                if (delta < 12 * MONTH)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "یک ماه قبل" : months + " ماه قبل";
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "یک سال قبل" : years + " سال قبل";
            }
            else
            {
                if (delta < 1 * MINUTE)
                {
                    return ts.Seconds == 1 ? "Moment ago" : ts.Seconds + "   Seconds before";
                }
                if (delta < 2 * MINUTE)
                {
                    return "A minute ago";
                }
                if (delta < 45 * MINUTE)
                {
                    return ts.Minutes + "Minutes ago";
                }
                if (delta < 90 * MINUTE)
                {
                    return "About an hour ago";
                }
                if (delta < 24 * HOUR)
                {
                    return ts.Hours + "  Hours";
                }
                if (delta < 48 * HOUR)
                {
                    return "LastDay";
                }
                if (delta < 30 * DAY)
                {
                    return ts.Days + " The day before";
                }
                if (delta < 12 * MONTH)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "A month ago" : months + " Month ago";
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "One year ago" : years + " Years ago";
            }
        }


        /// <summary>
        /// محاصبه فاصله زمانی 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Calculate(this DateTime dateTime, string _LastTime, string _LastSecend, string _LastMinut1
            , string _LastMinuts, string _LastHour1, string _LastHour, string _LastDay1, string _LastDay, string _LastMounth1, string _LastMounth,
            string _LsatYear1, string _LsatYear)
        {
            dateTime = DateTime.Parse(dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? _LastTime : ts.Seconds + _LastSecend;
            }
            if (delta < 2 * MINUTE)
            {
                return _LastMinut1;
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + _LastMinuts;
            }
            if (delta < 90 * MINUTE)
            {
                return _LastHour1;
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + _LastHour;
            }
            if (delta < 48 * HOUR)
            {
                return _LastDay1;
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + _LastDay;
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? _LastMounth1 : months + _LastMounth;
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? _LsatYear1 : years + _LsatYear;

        }
        #endregion TimeCalculator
        /// <summary>
        /// حملات تزریق sql injection
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SqlInjection(this string input)
        {
            return input.Replace("'", "''");
        }

        /// <summary>
        /// تابعی که کد ملی را بررسی می کند و صحت درستی ان را اعلام می کند.
        /// </summary>
        /// <param name="idMelli"></param>
        /// <returns></returns> 
        public static bool CodeMelli(this string idMelli)
        {
            int index = 10;//موقعيت مکاني که در اعداد آرايه ضرب ميشود
            int mul = 0;//جهت ذخيره حاصل ضرب
            int result = 0;//جهت ذخيره جمع حاصل ضرب ها
            int mod = 0;//جهت ذخيره باقيمانده
            bool check = false;// براي درست يا غلط بودن کد ملي (خروجي تابع)ا
            bool equal = true;//براي مقايسه اعداد آرايه
            int[] arrIdMelli = new int[10];
            int lentgh = idMelli.Length;
            try
            {
                if (idMelli.Length >= 8 && idMelli.Length <= 10)
                {
                    for (int i = index; i > 0; i--)
                    {
                        try
                        {
                            arrIdMelli[i - 1] = Convert.ToInt16(idMelli.Substring(lentgh - 1, 1));//برداشتن يک به يک اعداد از انتها و قرار دادن در آرايه از انديس 0
                            lentgh--;
                        }
                        catch { }
                    }
                    for (int i = 0; i <= 9; i++)// اين حلقه براي مقايسه اعداد استفاده مي شود
                    {
                        if (arrIdMelli[i] != arrIdMelli[i + 1])
                        {
                            equal = false; break;
                        }
                    }
                    if (!equal)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            try
                            {
                                mul = arrIdMelli[i] * index;
                            }
                            catch { }
                            index--;
                            result += mul;
                        }
                        mod = result % 11;
                        if (mod < 2)
                        {
                            if (arrIdMelli[9] == mod)
                                check = true;
                        }

                        else if (11 - mod == arrIdMelli[9])
                        {
                            check = true;
                        }
                        if (idMelli == "1234567891")
                        {
                            check = false;
                        }

                    }
                }
            }
            catch { }
            return check;
        }
        /// <summary>
        /// تبیدل تاریخ میلادی با شمسی
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToPersion(this DateTime dt)
        {
            try
            {
                if (dt != null)
                {

                    Class_date cdate = new Class_date();
                    string date = dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString();

                    return cdate.Shamsi(date);
                }
                else
                {
                    return "فرمت تاریخ نامتعبر";
                }
            }
            catch { return ""; }
        }

        /// <summary>
        /// بدست آوردن سن بر اساس تاریخ تولد
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static string GetAge(this DateTime birthDate, string ob)
        {
            try
            {
                var now = Convert.ToDateTime(DateTime.Now.ToPersion().ToString());
                var tmpDate = birthDate;
                int y = 0;
                int m = 0;
                int d = 0;
                while (tmpDate < now)
                {
                    if (tmpDate.AddYears(1) > now)
                        break;
                    tmpDate = tmpDate.AddYears(1);
                    y++;
                }

                while (tmpDate < now)
                {
                    if (tmpDate.AddMonths(1) > now)
                        break;
                    tmpDate = tmpDate.AddMonths(1);
                    m++;
                }

                while (tmpDate < now)
                {
                    tmpDate = tmpDate.AddDays(1);
                    d++;
                }
                string ret = "";
                if (ob == "y")
                    ret = y.ToString();
                else if (ob == "m")
                    ret = m.ToString();
                else if (ob == "d")
                    ret = d.ToString();
                else if (ob == "ymd")
                    ret = string.Format("{0}/{1}/{2}", y, m, d);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// بدست آوردن روز هفته با ارسال تاریخ شمسی
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDayOfWeekName(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "يکشنبه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Tuesday: return "سه‏ شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Thursday: return "پنجشنبه";
                case DayOfWeek.Friday: return "جمعه";
                default: return "";
            }
        }

        /// <summary>
        /// کد کردن رشته ها
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(this string str)
        {
            string strEncrKey = "?pws#QUesTion##EE@245";
            byte[] byKey;
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray()).Replace("+", "PULS").Replace("=", "EQULE").Replace("/", "DIV");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// دی کد کردن رشته ها
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt(this string str)
        {
            string sDecrKey = "?pws#QUesTion##EE@245";
            byte[] byKey;
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

            byte[] inputByteArray;

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(str.Replace("PULS", "+").Replace("EQULE", "=").Replace("DIV", "/"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string SubStringTest(this string Text, int Length)
        {
            try
            {
                string StringText = Text.ToString();
                if (Text == "")
                {
                    return Text + "...";
                }
                else
                {
                    int StringLenght = int.Parse(Length.ToString());
                    if (Length < 0)
                    {
                        return Text + "... ";
                    }
                    else
                    {
                        if (StringText.Length > StringLenght)
                        {
                            return StringText.Substring(0, StringLenght) + "... ";
                        }
                        else
                        {
                            return StringText + "... ";
                        }
                    }
                }
            }
            catch
            {
                return Text;
            }
        }
    }

}