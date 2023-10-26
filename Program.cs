using gli_gps.Input;
using gli_gps.service;

void Main()
{
    IHandleInput handleInput = new HandleInput();
    string incomingFilePath = handleInput.GetIncomingFilePath("Please provide the entire path of the json file\n" +
                                                              "(example:/mnt/c/yourDirectory/yourJsonFile.json):");
    
    string outgoingFilePath = handleInput.GetOutgoingFilePath("Please provide the entire directory along with" +
                                                              " the file's name to save the output json file\n" +
                                                              "(example:/mnt/c/yourDirectory/newJsonFile.json):");
    
    string unit = handleInput.GetUnitType("Please type 'metric' if You want the distance in metres and the bearing" +
                                          " in degree,\nor type 'imperial', if You want the distance in feet and" +
                                          " the bearing in radian:");
    
    List<IUnitType> unitTypes = new() { new UnitTypeMetric(), new UnitTypeImperial() };
    CreateJson createJson = new CreateJson(unitTypes);
    createJson.WriteJson(incomingFilePath, outgoingFilePath, unit);
}
Main();
