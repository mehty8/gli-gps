namespace gli_gps.service;

public class UnitTypeMetric : IUnitType
{
    public bool IsRequestedType(string unitType)
    {
        return unitType.Equals("metric");
    }
    public Dictionary<string, int> Calculate(double lat1, double lat2, double lon1, double lon2)
    {
        return null;
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

        int distance = (int)Math.Round(earthRadius * c);
   
        return distance;
    }
}