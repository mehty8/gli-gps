namespace gli_gps.service;

public interface IUnitType
{
    //checks which calculating class to choose, metric or imperial
    public bool IsRequestedType(string unitType);

    //Calculates the distance and the bearing of two given lat and lon pair
    public Dictionary<string, int> Calculate(double lat1, double lat2, double lon1, double lon2);
}