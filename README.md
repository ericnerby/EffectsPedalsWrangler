# Effects Pedal Wranger

## What is it?

The ***Effects Pedal Wranger*** is a C# application that will help guitar players keep track of their pedal boards and different 'presets' for different songs/sets.
All the basic functionality is here in a command line application, including saving and loading pedals and boards with json files.
That being said, there's still a lot of ways I'd like to improve the functionality in the future, so keep an eye out!

## What's the plan?

* I built the core functionality of this application using *Test Driven Development*, stubbing out class fields and writing unit tests before writing the code. The only methods I didn't unit test are the interactive methods that require user input from the console.
* I've written classes to represent individual *settings* for pedals, the *pedals* themselves, and a *pedalboard* with presets.
* The *static class* `ListSerializer` writes and reads the *Pedals* and *PedalBoards* to and from json files so that you can save everything between runtimes.

## Project Requirements

* Implements a **master loop** interface to interact with the user.
* Many **Dictionaries** and **Lists** instantiated and used throughout, for instance in the `Pedal` and `PedalBoard` classes, as well as within `Program`.
* Implements a **regular expression** when confirming proper clockface format for pedal settings that don't have numbers.
* **Many unit tests written throughout** all `Settings`, `Utils`, `Pedal`, and `PedalBoard` classes.
* Implements a **conversion tool** to go back and forth between clockface strings and integer values for settings (`Utils.ClockFaceConverter`).
* Implements **Linq queries** in `PedalBoards.PedalBoard` and its unit tests.