using System.Text.RegularExpressions;

namespace gli_gps.Input;

//the extension methods check the inputs for errors, and inform the user about them. Return true if no error found.
public static class HandleInputExtension
{
    public static bool CheckIncomingFilePath(this string input)
    {
        if (input.Length == 0)
        {
            Console.WriteLine("You did not provide any path. Please try again");
            return false;
        }
        bool regex = Regex.IsMatch(input.ToLower(), "\\.json$");
        if (!regex)
        {
            Console.WriteLine("You must have mistyped something, the path must end with .json. Please try again:");
            return false;
        }

        if (!File.Exists(input))
        {
            Console.WriteLine("You must have mistyped something, " +
                              "at the provided path there is no such a json file. Please try again:");
            return false;
        }
        return true;
    }

    public static bool CheckOutgoingFilePath(this string input)
    {
        if (input.Length == 0)
        {
            Console.WriteLine("You did not provide any path. Please try again");
            return false;
        }
        bool regex = Regex.IsMatch(input.ToLower(), "\\.json$");
        if (!regex)
        {
            Console.WriteLine("You must have mistyped something, the path must end with .json. Please try again:");
            return false;
        }

        int endOfDirectoryIndex = input.LastIndexOf("/");
        if (endOfDirectoryIndex == -1)
        {
            Console.WriteLine("You must have mistyped something, A correct path has to contain '/'. Please try again:");
            return false;
        }
        
        string outputDirectory = input.Substring(0,input.LastIndexOf("/"));
        if (!Directory.Exists(outputDirectory))
        {
            Console.WriteLine("You must have mistyped something, " +
                              "the provided directory does not exist. Please try again:");
            return false;
        }
        return true;
    }

    public static bool CheckUnitType(this string input)
    {
        if (input.Length == 0)
        {
            Console.WriteLine("You did not provide any path. Please try again");
            return false;
        }

        bool regex = Regex.IsMatch(input.ToLower(), "metric|imperial");
        if (!regex)
        {
            Console.WriteLine("You must have mistyped something, please type 'metric' " +
                              "if You want the distance in metres and the bearing in degree,\n or type 'imperial'," +
                              "If You want the distance in feet and the bearing in radian:");
            return false;
        }

        int metric = input.ToLower().IndexOf("metric");
        int imperial = input.ToLower().IndexOf("imperial");
       
        if (metric >= 0 && imperial >= 0)
        {
            Console.WriteLine("You must have mistyped, You provided both 'metric' and 'imperial'. " +
                              "Please choose only one:");
            return false;
        }

        return true;
    }
}