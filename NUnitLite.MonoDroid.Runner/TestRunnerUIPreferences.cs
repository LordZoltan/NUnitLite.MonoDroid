using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
	public static class TestRunnerUIPreferences
	{
		public const string PREF_ALWAYS_RUN_TESTS_KEY = "PREF_NUNIT_ALWAYS_RUN_TESTS";

		/// <summary>
		/// Controls whether unit tests are always re-run on the main screen when 
		/// </summary>
		public static bool AlwaysRunTests
		{
			get
			{
				var p = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				return p.GetBoolean(PREF_ALWAYS_RUN_TESTS_KEY, false);
			}
			set
			{
				var p = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				var editor = p.Edit();
				editor.PutBoolean(PREF_ALWAYS_RUN_TESTS_KEY, value);
				editor.Commit();
			}
		}

	}
}