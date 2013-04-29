using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnitLite.Runner;

namespace NUnitLite.MonoDroid
{
	/// <summary>
	/// Derive from this activity to create a standard test runner activity in your app.
	/// </summary>
	public abstract class TestRunnerActivity : ListActivity
	{
		protected const int MENU_OPTION_RERUN = TestRunnerMenuHelper.SETTINGS_MENU_OPTION_ID + 1;

		private TestResultsListAdapter _testResultsAdapter;
		private TestRunnerMenuHelper _menuHelper;
		/// <summary>
		/// Handles the creation of the activity
		/// </summary>
		/// <param name="savedInstanceState"></param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_testResultsAdapter = new TestResultsListAdapter(this);
			_menuHelper = new TestRunnerMenuHelper(this);
			ListAdapter = _testResultsAdapter;
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			base.OnCreateOptionsMenu(menu);

			_menuHelper.OnCreateOptionsMenu(menu);

			int groupID = TestRunnerMenuHelper.SETTINGS_MENU_GROUP;
			var optionsMenuItemID = MENU_OPTION_RERUN;
			int menuItemOrder = Android.Views.Menu.None;
			var menuItemText = new Java.Lang.String("Re-run tests now");
			IMenuItem optionsMenuItem = menu.Add(groupID, optionsMenuItemID, menuItemOrder, menuItemText);

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (base.OnOptionsItemSelected(item))
				return true;
			if (_menuHelper.OnOptionsItemSelected(item))
				return true;
			switch (item.ItemId)
			{
				case MENU_OPTION_RERUN:
					{
						RunTests(true);
						return true;
					}
			}
			return false;
		}

		private void RunTests(bool forceRun = false)
		{
			if (forceRun || TestRunnerUIPreferences.AlwaysRunTests)
				TestRunContext.Current.TestResults.Clear();

			if (TestRunContext.Current.TestResults.Count == 0)
			{
				var testAssemblies = GetAssembliesForTest();
				var testAssemblyEnumerator = testAssemblies.GetEnumerator();
				var testRunner = new TestRunner();

				_testResultsAdapter.NotifyDataSetInvalidated();
				_testResultsAdapter.NotifyDataSetChanged();

				var listener = new UITestListener((TestResultsListAdapter)ListAdapter);

				// Add a test listener for the test runner
				testRunner.AddListener(listener);


				// Start the test process in a background task
				Task.Factory.StartNew(() =>
				{
					while (testAssemblyEnumerator.MoveNext())
					{
						try
						{
							var assembly = testAssemblyEnumerator.Current;
							testRunner.Run(assembly);
						}
						catch (Exception ex)
						{
							ShowErrorDialog(ex);
						}
					}
				});
			}
		}

		/// <summary>
		///   <i>Derived classes must call through to the super class's
		/// implementation of this method.  If they do not, an exception will be
		/// thrown.</i>
		/// </summary>
		/// <since version="Added in API level 1" />
		/// <remarks>
		///   <para />Called after <c><see cref="M:Android.App.Activity.OnRestoreInstanceState(Android.OS.Bundle)" /></c>, <c><see cref="M:Android.App.Activity.OnRestart" /></c>, or
		///   <c><see cref="M:Android.App.Activity.OnPause" /></c>, for your activity to start interacting with the user.
		/// This is a good place to begin animations, open exclusive-access devices
		/// (such as the camera), etc.
		///   <para>Keep in mind that onResume is not the best indicator that your activity
		/// is visible to the user; a system window such as the keyguard may be in
		/// front.  Use <c><see cref="M:Android.App.Activity.OnWindowFocusChanged(System.Boolean)" /></c> to know for certain that your
		/// activity is visible to the user (for example, to resume a game).
		///   <para><i>Derived classes must call through to the super class's
		/// implementation of this method.  If they do not, an exception will be
		/// thrown.</i></para></para><para><format type="text/html"><a href="http://developer.android.com/reference/android/app/Activity.html#onResume()" target="_blank">[Android Documentation]</a></format></para>
		/// </remarks>
		protected override void OnResume()
		{
			base.OnResume();

			RunTests();
		}

		/// <summary>
		/// Handles list item click
		/// </summary>
		/// <param name="l"></param>
		/// <param name="v"></param>
		/// <param name="position"></param>
		/// <param name="id"></param>
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var testRunItem = TestRunContext.Current.TestResults[position];

			Intent intent = new Intent(this, GetDetailsActivityType);
			intent.PutExtra("TestCaseName", testRunItem.TestCaseName);

			StartActivity(intent);
		}

		/// <summary>
		/// Retrieves a list of assemblies that contain test cases to execute using the test runner activity.
		/// </summary>
		/// <returns>Returns the list of assemblies to test</returns>
		protected abstract IEnumerable<Assembly> GetAssembliesForTest();

		/// <summary>
		/// Gets the type of activity to use for displaying test details
		/// </summary>
		protected abstract Type GetDetailsActivityType { get; }

		/// <summary>
		/// Displays an error dialog in case a test run fails
		/// </summary>
		/// <param name="exception"></param>
		private void ShowErrorDialog(Exception exception)
		{
			RunOnUiThread(() =>
			{
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetTitle("Failed to execute unit-test suite");
				builder.SetMessage(exception.ToString());

				var dialog = builder.Create();

				dialog.Show();
			});
		}
	}
}