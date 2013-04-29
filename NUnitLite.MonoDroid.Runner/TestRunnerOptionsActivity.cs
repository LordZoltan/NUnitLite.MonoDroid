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
	[Activity(Label = "NUnitLite Runner Settings")]
	public class TestRunnerOptionsActivity : PreferenceActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			
			var alwaysRunPref = new CheckBoxPreference(this.ApplicationContext);
			alwaysRunPref.Key = TestRunnerUIPreferences.PREF_ALWAYS_RUN_TESTS_KEY;
			alwaysRunPref.TitleFormatted = new Java.Lang.String("Always run tests");
			alwaysRunPref.SummaryFormatted = new Java.Lang.String("Controls whether tests are always re-run when the main page is shown");
			alwaysRunPref.SetDefaultValue(new Java.Lang.Boolean(false));
			this.PreferenceScreen = PreferenceManager.CreatePreferenceScreen(Application.Context);
			this.PreferenceScreen.AddPreference(alwaysRunPref);
		}
	}
}