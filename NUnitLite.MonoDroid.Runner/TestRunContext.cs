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
	/// Contains context based information about the current test run.
	/// This context is used by the runner to have a central place where to find
	/// (non-serializable) test information for the various activities that make up the 
	/// test runner.
	/// </summary>
	public class TestRunContext : System.IO.TextWriter
	{
		private static readonly NullListener NullListenerInstance = new NullListener();
		private static TestRunContext _current;
		private static object _lockHandle = new object();

		private List<TestRunInfo> _testResults;
		private ITest _currentTest;

		public ITest CurrentTest
		{
			get
			{
				return _currentTest;
			}
			set
			{
				_currentTest = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of <see cref="TestRunContext"/>
		/// </summary>
		private TestRunContext()
		{
			_testResults = new List<TestRunInfo>();
		}

		/// <summary>
		/// Gets the current test run context
		/// </summary>
		public static TestRunContext Current
		{
			get
			{
				if (_current == null)
				{
					lock (_lockHandle)
					{
						if (_current == null)
						{
							_current = new TestRunContext();
						}
					}
				}

				return _current;
			}
		}

		/// <summary>
		/// Gets the test results for the current test run
		/// </summary>
		public List<TestRunInfo> TestResults
		{
			get { return _testResults; }
		}

		

		public override Encoding Encoding
		{
			get { return Encoding.Default; }
		}

		public override void Write(char value)
		{
			if(CurrentTest != null)
				CurrentTest.Write(new string(new[] { value }));
		}

		public override void Write(string value)
		{
			if (CurrentTest != null)
				CurrentTest.Write(value);
		}

		public override void Write(string format, params object[] arg)
		{
			if (CurrentTest != null)
				CurrentTest.Write(format, arg);
		}

		public override void WriteLine()
		{
			if (CurrentTest != null)
				CurrentTest.WriteLine();
		}

		public override void WriteLine(string value)
		{
			if (CurrentTest != null)
				CurrentTest.WriteLine(value);
		}

		public override void WriteLine(string format, params object[] arg)
		{
			if (CurrentTest != null)
				CurrentTest.WriteLine(format, arg);
		}
	}
}