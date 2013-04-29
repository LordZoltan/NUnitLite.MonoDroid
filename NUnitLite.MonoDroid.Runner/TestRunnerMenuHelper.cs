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

namespace NUnitLite.MonoDroid
{
	public class TestRunnerMenuHelper
	{
		public const int SETTINGS_MENU_GROUP = 30;
		public const int SETTINGS_MENU_OPTION_ID = 1001;

		private Activity _activity;
		public TestRunnerMenuHelper(Activity activity)
		{
			if (activity == null)
				throw new ArgumentNullException("activity");

			_activity = activity;
		}

		/// <summary>
		/// Delegating method to add the NUnitLite runner common menu options
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public bool OnCreateOptionsMenu(IMenu menu)
		{
			int groupID = SETTINGS_MENU_GROUP;
			var optionsMenuItemID = SETTINGS_MENU_OPTION_ID;
			int menuItemOrder = Android.Views.Menu.None;
			var menuItemText = new Java.Lang.String("NUnitLite Runner Settings");
			IMenuItem optionsMenuItem = menu.Add(groupID, optionsMenuItemID, menuItemOrder, menuItemText);

			return true;
		}

		public bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case SETTINGS_MENU_OPTION_ID:
					{
						var i = new Intent(_activity,typeof(TestRunnerOptionsActivity));
						_activity.StartActivity(i);
						
						return true;
					}
			}
			return false;
		}
	}
}