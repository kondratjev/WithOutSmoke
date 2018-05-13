using System;

namespace WithOutSmoke
{
    public static class Globals
    {
        public static double ClientVersion = 0.4; // версия клиента
        public static double DayInHour = 0.04166666666666666666666666666667; // 1 сутки в 1 часе 
        public static double DayInMinute = 0.00069444444444444444444444444444444; // 1 сутки в 1 минуте

        public static DateTime DateWithout;
        public static int SubtractDays;
        public static int SubtractHours;
        public static int SubtractMinutes;
        public static int SubtractSeconds;

        public static int day, month, year, hour, minute; // дата и время отказа

        public static string currency; // валюта
        public static int forDay, inPack, timeToCig, inAshtray, puffs; // кол-во сигарет за день, сиг в пачке, время на 1 сигарету
        public static double howOld, startPrice, endPrice, resin, nicotine, carbonMonoxide, breath; // лет курения, цена одной пачки, смола, никотин, угарный газ в одной сигарете
        public static bool startMinimized, checkUpdate, hideTray; // запуск свернуто, проверка обновлений, скрытие в трей
    }
}
