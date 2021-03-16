# Effects Pedal Wranger

## What is it?

The *Effects Pedal Wranger* is a C# application that will help guitar players keep track of their pedal boards and different 'presets' for different songs/sets.
Right now, it's a work in progress, but stay tuned for more info in the future!

## What's the plan?

* I'm building this application using *Test Driven Development*, stubbing out class fields and writing unit tests before writing the code.
* I've written classes to represent individual *settings* for pedals, the *pedals* themselves, and a *pedalboard* with presets (based on a *VersionedList* class that I wrote with this application in mind).
* Right now, the goal is to make a command line app for creating these pedals, pedalboards, and presets and serializing them to local storage for later recall.

## Project Requirements

* Implements a **master loop** interface to interact with the user.
* **Multiple classes inherit from parent classes**. For example, the `ISetting` interface and the `Setting` class, as well as the PedalBoard class which inherits from the VersionedList class.
* Implements a **regular expression** when confirming proper clockface format for pedal settings that don't have numbers.
* **Many unit tests written throughout** all `Settings`, `Utils`, and `Pedal` classes.
* Implements a **conversion tool** to go back and forth between clockface strings and integer values for settings (`Utils.ClockFaceConverter`).
* Implements **Linq queries** in `PedalBoards.PedalBoard` and its unit tests.