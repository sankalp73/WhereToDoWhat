using System;
using Windows.ApplicationModel.Chat;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WhereToDoWhat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void foreButtton_Click(object s, RoutedEventArgs e)
        {
            try
            {
                var newPoint = new BasicGeoposition()
                {
                    Latitude = (App.Current as App).latitude,
                    Longitude = (App.Current as App).longitude
                };
                var geofence = new Geofence(notifyTextBox.Text, new Geocircle(newPoint, double.Parse("100")),
                    MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited,
                    false, TimeSpan.FromSeconds(4));
                GeofenceMonitor.Current.Geofences.Add(geofence);
            }
            catch (Exception ex)
            {
                await new MessageDialog("Exception thrown: " + ex.Message).ShowAsync();
            }

            GeofenceMonitor.Current.GeofenceStateChanged += async (sender, args) =>
            {
                var geoReports = GeofenceMonitor.Current.ReadReports();
                foreach (var geofenceStateChangeReport in geoReports)
                {
                    var id = geofenceStateChangeReport.Geofence.Id;
                    var newState = geofenceStateChangeReport.NewState.ToString();
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog(newState + " : " + id)
                             .ShowAsync());
                    for (long i = 0; i < 100000000; ++i) ;

                    var message = new ChatMessage();
                    message.Recipients.Add("9808307198");
                    message.Body = "I have reached my destination!";
                    await ChatMessageManager.ShowComposeSmsMessageAsync(message);

                }
            };
            textBlock3.Text = "Monitoring Enabled";

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        /* private async void backButton_Click(object s, RoutedEventArgs e)
         {
             var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
             var geofenceTaskBuilder = new BackgroundTaskBuilder
             {
                 Name = "GeofenceBackgroundTask",
                 TaskEntryPoint = "BackgroundTask.GeofenceBackgroundTask"
             };

             var trigger = new LocationTrigger(LocationTriggerType.Geofence);
             geofenceTaskBuilder.SetTrigger(trigger);
             var geofenceTask = geofenceTaskBuilder.Register();
             geofenceTask.Completed += async (sender, args) =>
             {
                 var geoReports = GeofenceMonitor.Current.ReadReports();
                 foreach (var geofenceStateChangeReport in geoReports)
                 {
                     var id = geofenceStateChangeReport.Geofence.Id;
                     var newState = geofenceStateChangeReport.NewState.ToString();
                     await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog(newState + " : " + id)
                          .ShowAsync());
                 }
             };

         }
         */


    }
}
