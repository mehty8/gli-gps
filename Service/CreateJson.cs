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
    
    //Creates the new json file with the calculations done by one of the uniType class.
    //Configured to handle json files that would extend the memory
    //Checks more-difficult-to-fix errors
    public void WriteJson(string incomingFilePath, string outgoingFilePath, string unitType)
    {
        var requestedUnitType = _unitTypes.Find(unit => unit.IsRequestedType(unitType));
       
        using (FileStream fs = File.OpenRead(incomingFilePath))
        using (JsonDocument doc = JsonDocument.Parse(fs))
        {
            var root = doc.RootElement;
            if (root.TryGetProperty("input", out var inputArray)
                && inputArray.ValueKind == JsonValueKind.Array)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(outgoingFilePath))
                    {
                        writer.Write("{");
                        writer.Write("\"output\": [");

                        for (int i = 1; i < inputArray.GetArrayLength(); i++)
                        {
                            OutgoingData outgoingData = GetResult(inputArray[i - 1],
                                inputArray[i], requestedUnitType);
                            string outputJson = JsonSerializer.Serialize(outgoingData);
                            writer.Write(outputJson);

                            if (i + 1 < inputArray.GetArrayLength())
                            {
                                writer.Write(", ");
                            }
                        }
                        writer.Write("]");
                        writer.Write("}");
                        
                        Console.WriteLine("Json successfully created in the provided directory");
                    }
                }
                catch (UnauthorizedAccessException ex) 
                {
                    Console.WriteLine(ex.Message + "\nPlease have the right authorization, " +
                                      "and relaunch the application then.");
                }
            } else {
                Console.WriteLine("The provided json has a different structure from the expected one.\n" +
                                  "The main key has to be 'input', which must have an array as a value,\n" +
                                  "that contains the 'date' the 'time' and the 'GPSP'('lat' 'lon') data records.\n" +
                                  "Please have one accordingly, and relaunch the application then.");
            }
        }
    }
    
    //Deserialize json record pairs
    private List<IncomingData> JsonDeserialize(JsonElement jsonElement1, JsonElement jsonElement2)
    {
        List<IncomingData> desJson = new();
        var jsonElement1ToObj = jsonElement1.Deserialize<IncomingData>();
        var jsonElement2ZoObj = jsonElement2.Deserialize<IncomingData>();
        desJson.Add(jsonElement1ToObj);
        desJson.Add(jsonElement2ZoObj);
        return desJson;
    }

    //returns the result of the calculation
    private Dictionary<string, int> Calculate(IncomingData jsonElement1ToObj, IncomingData jsonElement2ZoObj, IUnitType requestedUnitType)
    {
        double radLat1 = (Math.PI / 180) * jsonElement1ToObj.GPSP.lat;
        double radLat2 = (Math.PI / 180) * jsonElement2ZoObj.GPSP.lat;
        double radLon1 = (Math.PI / 180) * jsonElement1ToObj.GPSP.lon;
        double radLon2 = (Math.PI / 180) * jsonElement2ZoObj.GPSP.lon;

        Dictionary<string, int> calculationResult = requestedUnitType.Calculate(radLat1, radLat2, radLon1, radLon2);
        return calculationResult;
    }

    //returns the new object to serialize to json, containing the calculation result
    private OutgoingData GetResult(JsonElement jsonElement1, JsonElement jsonElement2, IUnitType requestedUnitType)
    {
        List<IncomingData> desJson = JsonDeserialize(jsonElement1, jsonElement2);
        IncomingData jsonElement1ToObj = desJson[0];
        IncomingData jsonElement2ToObj = desJson[1];
        
        Dictionary<string, int> calculationResult =
            Calculate(jsonElement1ToObj, jsonElement2ToObj, requestedUnitType);
        int resultDistance = calculationResult["distance"];
        int resultBearing = calculationResult["bearing"];
        
        Gpsp gpsp = new Gpsp(jsonElement1ToObj.GPSP.lat, jsonElement1ToObj.GPSP.lon);
        OutgoingData outgoingData = new OutgoingData(gpsp, resultDistance, resultBearing);
        return outgoingData;
    }
}