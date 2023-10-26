namespace gli_gps.service;

public interface IUnitType
{
    public bool IsRequestedType(string unitType);

    public Dictionary<string, int> Calculate(double lat1, double lat2, double lon1, double lon2);
}