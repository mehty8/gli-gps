namespace gli_gps.service;

public class UnitTypeImperial : IUnitType
{
    public bool IsRequestedType(string unitType)
    {
        return unitType.Equals("imperial");
    }
    
    public Dictionary<string, int> Calculate(double lat1, double lat2, double lon1, double lon2)
    {
        Dictionary<string, int> calculations = new();
        int distance = CalculateDistance(lat1, lat2, lon1, lon2);
        int bearing = CalculateBearing(lat1, lat2, lon1, lon2);
        calculations.Add("distance", distance);
        calculations.Add("bearing", bearing);
        return calculations;
    }
    private int CalculateDistance(double lat1, double lat2, double lon1, double lon2)
    {
        double earthRadius = 6371000;

        double distanceLat = lat2 - lat1;
        double distanceLon = lon2 - lon1;
   
        double a = Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Sin(distanceLon / 2) * Math.Sin(distanceLon / 2);
   
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = earthRadius * c;
        double feet = 3.28084;
   
        return (int)(distance * feet);
    }

    private int CalculateBearing(double lat1, double lat2, double lon1, double lon2)
    {
        double lonDifference = lon2 - lon1;
        double y = Math.Sin(lonDifference) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lonDifference);

        double bearing = Math.Atan2(y, x);
        double bearingDegrees = (bearing * 180) / Math.PI;
        
        double initialBearing = (bearingDegrees + 360) % 360;
        double radians = Math.PI / 180;
        
        return (int)(initialBearing * radians);
    }
}