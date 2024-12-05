using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace MarketWpfProject.Views.UserView
{
    public partial class ForOrderMap : Window
    {
        public ForOrderMap()
        {
            InitializeComponent();
        }
        private Location marketLocation = new Location(40.4489, 49.7653); // Başlangıçta marketin yeri (Xırdalan)
        private Location userSelectedLocation;

        // Kullanıcı bir yere tıklayarak konum seçtiğinde
        private void MapControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point mousePoint = e.GetPosition(MapControl);
            userSelectedLocation = MapControl.ViewportPointToLocation(mousePoint);

            // Kullanıcı seçtiği konumu pushpin ile göster
            AddPushpin(userSelectedLocation, "User");
        }

        private void DrawRoute_Click(object sender, RoutedEventArgs e)
        {
            // Eğer kullanıcı bir konum seçmediyse, manuel olarak girilen yeri al
            string userLocation = UserLocationInput.Text;

            if (string.IsNullOrWhiteSpace(userLocation) && userSelectedLocation == null)
            {
                MessageBox.Show("Please provide a location or select one from the map.");
                return;
            }

            // Kullanıcı konumunu bul veya harita üzerinden seç
            if (!string.IsNullOrWhiteSpace(userLocation))
            {
                var userCoordinates = GetCoordinates(userLocation);
                if (userCoordinates != null)
                {
                    userSelectedLocation = userCoordinates;
                }
                else
                {
                    MessageBox.Show("Location not found. Please try again or select with the map.");
                    return;
                }
            }

            // Market ve Kullanıcı Konumlarını Göster
            AddPushpin(marketLocation, "Market");
            AddPushpin(userSelectedLocation, "User");

            // Yol Haritası Çiz
            DrawRoute(marketLocation, userSelectedLocation);
        }

        private Location GetCoordinates(string location)
        {
            // Burada, API ya da başka bir çözümle dinamik yer araması yapabilirsiniz.
            var locations = new Dictionary<string, Location>(StringComparer.OrdinalIgnoreCase)
            {
                { "baku", new Location(40.4093, 49.8671) }, // Baku
                { "xirdalan", new Location(40.4489, 49.7653) }, // Xırdalan
                { "sumqayit", new Location(40.5897, 49.6680) }, // Sumqayit
                // Yeni yerler ekleyebilirsiniz
            };

            string normalizedLocation = NormalizeString(location);
            return locations.FirstOrDefault(loc => NormalizeString(loc.Key) == normalizedLocation).Value;
        }

        private string NormalizeString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            var normalized = input.ToLower(new CultureInfo("az-Latn-AZ"));
            return normalized.Replace("ı", "i").Replace("ç", "c");
        }

        private void AddPushpin(Location location, string title)
        {
            Pushpin pushpin = new Pushpin
            {
                Location = location,
                Content = title
            };
            MapControl.Children.Add(pushpin);
        }

        private void DrawRoute(Location start, Location end)
        {
            var routeLayer = new MapLayer();
            var polyline = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 5
            };

            // Başlangıç ve bitiş noktalarını çiz
            polyline.Points.Add(MapControl.LocationToViewportPoint(start));
            polyline.Points.Add(MapControl.LocationToViewportPoint(end));

            routeLayer.Children.Add(polyline);
            MapControl.Children.Add(routeLayer);
        }

        // Hareketi simüle et, örneğin animasyon ekleyebilirsiniz
        private async void SimulateMovement(Location start, Location end)
        {
            // Hareket simülasyonu için basit bir örnek
            var currentLocation = start;
            for (int i = 0; i < 100; i++)
            {
                currentLocation.Latitude += (end.Latitude - start.Latitude) / 100;
                currentLocation.Longitude += (end.Longitude - start.Longitude) / 100;

                // Konumu güncelle ve haritada göster
                MapControl.Center = currentLocation;

                await Task.Delay(100); // 100 ms arayla güncelle
            }
        }
    }
}

