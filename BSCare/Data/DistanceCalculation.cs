namespace BSCare.Data
{
    static public class DistanceCalculation
    {
        static public double HaversineDistance(LatLng pos1, LatLng pos2)
        {
            double R = 6371;
            //double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = ConvertToRadiansToRadians(pos2.Latitude - pos1.Latitude);
            var lng = ConvertToRadiansToRadians(pos2.Longitude - pos1.Longitude);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadiansToRadians(pos1.Latitude)) * Math.Cos(ConvertToRadiansToRadians(pos2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        public enum DistanceUnit { Miles, Kilometers };

        static public double ConvertToRadiansToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
