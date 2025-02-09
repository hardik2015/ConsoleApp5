## Documentation about Terms

- **Hurdle Types 
    In this program we have 3 types of Hurdles(Obstacles)
    1 - Block Type : Robot cannot go to that location
    2 - Transfer Type : User will give another location as perameter and where robot will be transfer if robot go to this location
    3 - Rotating Type : User will give rotation degree like 90, 180, 270 as perameter and Robot direction will chnage by that degree

- **Hurdle 
    As each robot have different terminology for Hurdles each hurdles have one of the above type and have its own name
    User can create any number of hurdles

- **Hurdle Item
    Each hurdle item has hurdle and it's parameter if any

- **Hurdle Grid
    it is Dictonary of key as Hurdle Item Location and value as Hurdle

- **Robot Configuration
    Robot Configuration contains Hurdle Grid and Grid Size and Hurdles List

## Test Cases
Default configuration for Hurdles in Test cases are shown in below Graph

![image_with_grid (2)](https://github.com/user-attachments/assets/2500298e-af5d-45c2-9ce2-b4fc6e10dd87)


## Restrictions
You can not able to put two hurdles at same location

If User makes mistakes at time of creationg new configuration then it is irreversible so please pay attention at that time
