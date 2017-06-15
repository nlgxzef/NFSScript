## Contents
* [Variables](#variables)
* [Functions](#functions)

If you want to contribute please follow these steps!

# Variables

## Naming & adding variables to the addresses class
If you have a static address or a pointer to an address that is a variable of some sort then you should give it a proper name. For example: Say you've found a static address or got a pointer to an address that it's value is 1 when the player is in pursuit and 0 when the player is not in pursuit in NFS: Most Wanted then you should declare the address variable inside the struct `PlayerAddrs` which is inside the class `MWAddresses` which is located in `Core/Addresses.cs`, if it's a static address then declare the variable in NFSScript like this:
```cs
public const int STATIC_IS_PLAYER_IN_PURSUIT = 0xADDRESS;
```
However if it's a pointer to an address then you should declare the variable it like this:
```cs
public const int PNONSTATIC_IS_PLAYER_IN_PURSUIT = 0xADDRESS;
public const int POINTER_IS_PLAYER_IN_PURSUIT_1 = 0xPOINTER1;
public const int POINTER_IS_PLAYER_IN_PURSUIT_2 = 0xPOINTER2;
// So on and so forth depending on the amount of pointers you have.
```

## Declaring variables
After you are done naming & declaring the constant integers, write the variable to the class it should belong to. In our case we found a player variable for NFS: Most Wanted so we would declare a boolean in the class `MW.Player` named `isInPursuit` like this if we found a static address:
```cs
        public static bool isInPursuit
        {
            get
            {
                return memory.ReadByte((IntPtr)Addrs.PlayerAddrs.STATIC_IS_PLAYER_IN_PURSUIT) == 1;
            }
        }
```
If it's a pointer that we found then we should declare the boolean like this:
```cs
        public static bool isInPursuit
        {
            get
            {
                int address = memory.ReadInt32((IntPtr)Addrs.PlayerAddrs.PNONSTATIC_IS_PLAYER_IN_PURSUIT);
                address = memory.ReadInt32((IntPtr)address + Addrs.PlayerAddrs.POINTER_IS_PLAYER_IN_PURSUIT_1);
                return memory.ReadByte((IntPtr)address + Addrs.PlayerAddrs.POINTER_IS_PLAYER_IN_PURSUIT_2) == 1;
            }
        }
```

# Functions

## Naming & adding functions addresses to the functions class
If you have found a function address that has been executed properly without any crashes with the proper parameters then name then give it an appropriate name of what the function does in BIG_LETTERS_LIKE_THIS.
Let's say we found a function that spawns a minimap icon (we didn't yet :() in NFS: Carbon then you should add it to the CarbonFunctions class which is located inside `Functions.cs`.

```cs
public const int ADD_MINIMAP_ICON = 0xADDRESS;
```
## Declaring functions
After you're done adding the function address to `Functions.cs` then declare the function in the class it affects. In our case adding a minimap icon affects the minimap which is part of the UI so we will go to `Carbon/UI.cs`
and declare our function inside the class `Minimap` which is inside the class `UI` like this:

```cs
/// <summary>
/// Adds a minimap icon.
/// </summary>
/// <param name="position">The icon's position.</param>
/// <param name="icon">The icon ID.</param>
public const int AddMinimapIcon(Vector3 position, int icon)
{
        Function.Call(CarbonFunctions.ADD_MINIMAP_ICON, position, icon); // Use the function.call function!
}
```
