# NUnitLite.MonoDroid #

*Please note this is a fork of the [original NUnitLite project](https://github.com/SpiritMachine/NUnitLite.MonoDroid).  The original readme at the time of forking (April 2013) is below).*

Marvellous though the original NUnitLite was, I wanted to add a few things:

- Nuget package that you can add to a MonoDroid application to turn it into a unit test app.
 - Nuget package that you can add to a MonoDroid class library to write test fixtures etc.
- Make re-running all tests when the main activity is navigated to **optional**
 - Persisting this option in shared prefs, adding a menu option to turn it on/off.
- Along with previous item - adding a menu option to the main activity to re-run all tests
- Added ability to write text output through the TestRunContext.Current
 - You can also write your test method to take a `TestResult`, through which you can also `WriteLine`.

Note that I haven't published the nuget packages anywhere - primarily because I don't 'own' the project.  In my workflow, I have a private nuget feed that is used by many of our projects.

The nuspecs are packaging the binaries into a lib\MonoAndroid2.2 folder in the package - this framework/version moniker is supported in [Nuget 2.5 and above](http://blog.nuget.org/20130425/nuget-2.5-released.html) (it's contribution #1 in the list).

*Original ReadMe*

This solution is a compilation of NUnitLite targeting Mono for Android. 

There now exists a test runner (credit to [wmeints](https://github.com/wmeints)). It presents a summary of passes/failures and allows interrogation of individual test results. The example application demonstrates passing, failing and error throwing tests.

![Fixture Result](https://github.com/LordZoltan/NUnitLite.MonoDroid/raw/master/Images/NUnitLiteDroidFixture.jpg "Fixture Result")
![Failing Test](https://github.com/LordZoltan/NUnitLite.MonoDroid/raw/master/Images/NUnitLiteDroidFail.jpg "Failing Test")

The original NUnitLite readme is in [NUnitLite.README.txt](https://github.com/LordZoltan/NUnitLite.MonoDroid/blob/master/NUnitLite.README.txt). It has notes on features and usage. Despite the text there, the version of NUnitLite used is 0.6.1.
