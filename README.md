# Effects Pedal Wranger

## What is it?

The *Effects Pedal Wranger* is a C# application that will help guitar players keep track of their pedal boards and different 'presets' for different songs/sets.
Right now, it's a work in progress, but stay tuned for more info in the future!

## What's the plan?

* I'm building this application using *Test Driven Development*, stubbing out class fields and writing unit tests before writing the code.
* I've written classes to represent individual *settings* for pedals, the *pedals* themselves, and a *pedalboard* with presets (based on a *VersionedList* class that I wrote with this application in mind).
* Right now, the goal is to make a command line app for creating these pedals, pedalboards, and presets and serializing them to local storage for later recall.