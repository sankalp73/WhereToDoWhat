using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace WhereToDoWhat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;
            var position = await locator.GetGeopositionAsync();
            await myMap.TrySetViewAsync(position.Coordinate.Point, 18D);
            mySlider.Value = myMap.ZoomLevel;
            AddPushpin(position.Coordinate.Point, "");
            
        }

        private void setPositionButton_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).latitude = myMap.Center.Position.Latitude;
            (App.Current as App).longitude = myMap.Center.Position.Longitude;
            AddPushpin1(myMap.Center, "");
            //Frame.Navigate(typeof(Page2));

        }

        private void mySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(myMap!=null)
            {
                myMap.ZoomLevel = e.NewValue;

            }
        }
        public void AddPushpin(Geopoint location, string text)
        {
            var pin = new Grid()
            {
                Width = 50,
                Height = 50,
                Margin = new Windows.UI.Xaml.Thickness(-12)
            };

            pin.Children.Add(new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.DodgerBlue),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 3,
                Width = 20,
                Height = 20
            });

            pin.Children.Add(new TextBlock()
            {
                Text = text,
                FontSize = 6,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            });

            MapControl.SetLocation(pin, location);
            myMap.Children.Add(pin);
        }
        public void AddPushpin1(Geopoint location, string text)
        {
            var pin = new Grid()
            {
                Width = 50,
                Height = 50,
                Margin = new Windows.UI.Xaml.Thickness(-12)
            };

            pin.Children.Add(new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Red),
                Stroke = new SolidColorBrush(Colors.Gray),
                StrokeThickness = 3,
                Opacity = 0.3,
                Width = 50,
                Height = 50
            });

            pin.Children.Add(new TextBlock()
            {
                Text = text,
                FontSize = 6,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            });

            MapControl.SetLocation(pin, location);
            myMap.Children.Add(pin);
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }
    }
}
