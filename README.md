## Documentation about Terms

- Hurdle Types
  
    In this program, we have 3 types of Hurdles(Obstacles).
    1 - Block Type : Robot cannot go to that location.
    2 - Transfer Type : User will give another location as parameter and where robot will be transfer if robot go to this location.
    3 - Rotating Type : User will give rotation degree like 90, 180, 270 as parameter and Robot direction will change by that degree.

- Hurdle
  
    As each robot have different terminology for Hurdles each hurdles have one of the above type and have its own name.
    User can create any number of hurdles.

- Hurdle Item
  
    Each hurdle item has hurdle and it's parameter if any.

- Hurdle Grid
  
    it is Dictionary of key as Hurdle Item Location and value as Hurdle.

- Robot Configuration
  
    Robot Configuration contains Hurdle Grid and Grid Size and Hurdles List.
  
- Configuration.json
  
    It is a file which will store your previous config for hurdle grid and hurdle info so next time you can directly use that.

## Test Cases
    Default configuration for Hurdles in Test cases are shown in below Graph.

![image_with_grid (2)](https://github.com/user-attachments/assets/2500298e-af5d-45c2-9ce2-b4fc6e10dd87)


## Restrictions
    You can not able to put two hurdles at same location.
    
    If User makes mistakes at time of creating new configuration then it is irreversible so please pay attention at that time.
    
    User can not start from position which has hurdles there on grid.


## Environment 
    .NET 8.0

## Configuration
    I have two configuration with hurdles added in configuration.json file .
    
    Put that file as same location as exe file and then run you program you will se Robot 1 and Robot 2 configuration predefined.
    
