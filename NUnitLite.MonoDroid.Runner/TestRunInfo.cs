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
	/// <summary>
	/// Container to keep track of test runs
	/// </summary>
	public class TestRunInfo : Java.Lang.Object
	{
		/// <summary>
		/// Gets or sets whether the test run is passed
		/// </summary>
		public bool Passed { get; set; }

		/// <summary>
		/// Gets or sets whether the test case is running
		/// </summary>
		public bool Running { get; set; }

		/// <summary>
		/// Gets or sets whether this is a suite of tests
		/// </summary>
		public bool IsTestSuite { get; set; }

		/// <summary>
		/// Gets the description for the test run
		/// </summary>
		public string Description { get; set; }

		public string TestCountDescription
		{
			get
			{
				if (Test != null && IsTestSuite && TestResult != null)
				{
					int totalTestCount = TestResult.TotalTestCount;
					int totalPassed = TestResult.TotalPassed;
					int totalFailed = totalTestCount - totalPassed;
					return string.Format("{0} test{1}, {2} passed, {3} failed", totalTestCount, totalTestCount == 1 ? "" : "s",
						totalPassed, totalFailed);
				}
				else
					return string.Empty;
			}
		}

		/// <summary>
		/// Gets the full name for the test case
		/// </summary>
		public string TestCaseName { get; set; }

		public ITest Test { get; set; }

		/// <summary>
		/// Gets the test result for this test run
		/// </summary>
		public NUnitLite.TestResult TestResult { get; set; }	
	}
}