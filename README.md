# Stay Alive

## Navigation

+ [Main](#stay-alive)
+ [Game Screenshots](#game-screenshots)
+ [It has been implemented](#it-has-been-implemented)
+ [Patterns](#patterns)
+ [Gameplay](#gameplay)

## Game Screenshots

![AllScreenshots](https://github.com/r2dum/StayAlive/blob/main/README%20Files/AllScreenshots.png?raw=true)

## It has been implemented

+ Two entry points to the game: GameStartup and Shop
+ Pause the game without using Time.timeScale and Singleton
+ CleanUpHandler: got rid of destructor in C# classes, and their independent cleaning when changing scene
+ JasonSaveSystem: works with different files and classes (GameData, ShopData)
+ Object Pooling (Generic class)
+ State Machine (SpawnerStation)
+ Adherence to SOLID principles
+ Dependency Injection
+ Async scene loading
+ GameSettings
+ Extension Methods

## Patterns

+ Strategy
+ Observer
+ State
+ Factory Method

## Gameplay

  ![GameplayGif](https://github.com/r2dum/StayAlive/blob/main/README%20Files/Gameplay.gif?raw=true)
