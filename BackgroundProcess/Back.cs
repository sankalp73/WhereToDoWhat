using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Notifications;

namespace BackgroundProcess
{
    class Back
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var reports = GeofenceMonitor.Current.ReadReports();
            var report = reports.FirstOrDefault(r => (r.Geofence.Id == "MyGeofenceId") && (r.NewState == GeofenceState.Entered));

            if (report == null) return;

            // Create a toast notification to show a geofence has been hit
            var toastXmlContent = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var txtNodes = toastXmlContent.GetElementsByTagName("text");
            txtNodes[0].AppendChild(toastXmlContent.CreateTextNode("Geofence triggered toast!"));
            txtNodes[1].AppendChild(toastXmlContent.CreateTextNode(report.Geofence.Id));

            var toast = new ToastNotification(toastXmlContent);
            var toastNotifier = ToastNotificationManager.CreateToastNotifier();
            toastNotifier.Show(toast);
        }
       


           
    }

    
}
