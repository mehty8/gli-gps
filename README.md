# GLI GPSP PROJECT

## Summary

The application receives a json file containing 
records of latitude and longitude at the given time.
It calculates the distance and the bearing of each and every consecutive
record pairs (first-second, second-third and so forth).
The user can choose between the metric or the imperial system,
meaning the distance and the bearing can be calculated
either in metre-degree or foot-radian units.
At the end of the process it creates a new json file
with the result.

## Configurations

- It works only on Linux (different way other OS handles directory path)

- .Net 6.0 Framework

## How to Run

- 1 Pull the repository
- 2 Open Linux shell and If dotnet is not installed yet use commands:
  - sudo apt-get update
  - sudo apt-get install -y dotnet-sdk-6.0
- 3 Navigate to the project directory:
  - cd /mnt/c/yourProjectDirectory
- 4 Build the project with command:
  - dotnet build
- 5 Run it with command:
  - dotnet run

## How it Works

- 1 The user has to provide the full path of the incoming json file.

- 2 The user has to provide the full path of the newly created json file.

- 3 The user has to provide the unit type, either metric or imperial.

- 4 After computation the newly created json file is saved in the provided directory.

- 5 The app closes.

## Additional Information 

- It is configured to be able to process json files that
would extend the memory.

- It checks the inputs thoroughly and informs the user if seemingly something is wrong with provided information.

- The distances and the bearings are rounded down to integers.
