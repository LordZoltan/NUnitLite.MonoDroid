If you did not already have a MainActivity.cs file in your project, then the nuget package will have added one, and in it you will find more instructions on how to proceed.

The package also adds a DetailsActivity.cs file to your project as well - this is required by the test runner and is referenced in the MainActivity in its override of
the GetDetailsActivityType virtual method inherited from TestRunnerActivity.

If you do not get either the MainActivity.cs or DetailsActivity.cs files added to your project (typically because you already have them), please find below the default text 
for these, so you can modify your source code to complete the transformation of this project into a unit test project:

-----------------------------------------------------------------------------------------------------------

MainActivity.cs
===============

using System;
using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Widget;
using Android.OS;
using NUnitLite.MonoDroid;

namespace $rootnamespace$
{
		[Activity(Label = "$rootnamespace$", MainLauncher = true, Icon = "@drawable/icon")]
		public class MainActivity : TestRunnerActivity
		{
				protected override IEnumerable<Assembly> GetAssembliesForTest()
				{
					//TODO: Return the assemblies containing the unit tests that are to be run
					//			If you are writing your unit tests inside this application, then the
					//			following line should be enough.
					yield return GetType().Assembly;
				}

				protected override Type GetDetailsActivityType
				{
						//You can alter the activity used to display an individual test's results here.
						get { return typeof(DetailsActivity); }
				}
		}
}

-----------------------------------------------------------------------------------------------------------

DetailsActivity.cs
==================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnitLite.MonoDroid;

namespace $rootnamespace$
{
		[Activity(Label = "Test Details")]
		public class DetailsActivity: TestRunDetailsActivity
		{
		}
}