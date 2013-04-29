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

