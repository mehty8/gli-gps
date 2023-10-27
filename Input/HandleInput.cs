using System.Text.RegularExpressions;

namespace gli_gps.Input;

public class HandleInput : IHandleInput
{
    //checks and returns the incoming json filepath
    public string GetIncomingFilePath(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine();
        while (!input.CheckIncomingFilePath())
        {
            input = Console.ReadLine();
        }

        return input;
    }

    //checks and returns the outgoing json filepath
    public string GetOutgoingFilePath(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine();
        while (!input.CheckOutgoingFilePath())
        {
            input = Console.ReadLine();
        }

        return input;
    }

    //checks and returns the chosen unit type
    public string GetUnitType(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine();
        while (!input.CheckUnitType())
        {
            input = Console.ReadLine();
        }

        return Regex.Replace(input.ToLower(), ".*?(metric|imperial).*", "$1");
    }
}