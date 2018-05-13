using System;
using System.Net;

namespace WithOutSmoke
{
    internal static class Utils
    {
        public static void CheckUpdate()
        {
            try // проверка новой версии
            {
                var wClient = new WebClient();
                var versionServer = wClient.DownloadString("http://withoutsmokesrv.at.ua/version.txt");
                if (Convert.ToDouble(versionServer) > Globals.ClientVersion)
                {
                    var updateLog = new UpdateForm();
                    updateLog.ShowDialog();
                }
            }
            catch
            {
                //
            }
        }
    }
}
