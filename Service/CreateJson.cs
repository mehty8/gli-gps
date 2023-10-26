using System.Text.Json;
using gli_gps.Data;

namespace gli_gps.service;

public class CreateJson
{
    private List<IUnitType> _unitTypes;

    public CreateJson(List<IUnitType> unitTypes)
    {
        _unitTypes = unitTypes;
    }
    
    private OutgoingData JsonDeserialize(JsonElement jsonElement1, JsonElement jsonElement2, IUnitType requestedUnitType)
    {
        var jsonElement1ToObj = jsonElement1.Deserialize<IncomingData>();
        var jsonElement2ZoObj = jsonElement2.Deserialize<IncomingData>();
        
        double radLat1 = (Math.PI / 180) * jsonElement1ToObj.GPSP.lat;
        double radLat2 = (Math.PI / 180) * jsonElement2ZoObj.GPSP.lat;
        double radLon1 = (Math.PI / 180) * jsonElement1ToObj.GPSP.lon;
        double radLon2 = (Math.PI / 180) * jsonElement2ZoObj.GPSP.lon;

        Dictionary<string, int> calculationResult = requestedUnitType.Calculate(radLat1, radLat2, radLon1, radLon2);
        int resultDistance = calculationResult["distance"];
        int resultBearing = calculationResult["bearing"];
        Gpsp gpsp = new Gpsp(jsonElement1ToObj.GPSP.lat, jsonElement1ToObj.GPSP.lon);
        OutgoingData outgoingData = new OutgoingData(gpsp, resultDistance, resultBearing);
        return outgoingData;
    }
}