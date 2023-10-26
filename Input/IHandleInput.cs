namespace gli_gps.Input;

public interface IHandleInput
{
    string GetIncomingFilePath(string message);

    string GetOutgoingFilePath(string message);

    string GetUnitType(string message);
}