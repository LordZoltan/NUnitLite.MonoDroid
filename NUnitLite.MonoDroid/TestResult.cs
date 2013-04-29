// ***********************************************************************
// Copyright (c) 2007 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace NUnitLite
{
	public enum ResultState
	{
		NotRun,
		Success,
		Failure,
		Error
	}

	public class TestResult
	{
		private ITest test;

		private ResultState resultState = ResultState.NotRun;

		private string message;
#if !NETCF_1_0
		private string stackTrace;
#endif

		private ArrayList results;

		public TestResult(ITest test)
		{
			this.test = test;
		}

		public int TotalTestCount
		{
			get
			{
				if (Test is TestCase)
					return 1;
				else
				{
					//can only go by the number of results in the results collection
					int accumulator = 0;
					if (Results != null)
					{
						foreach (TestResult result in Results)
						{
							accumulator += result.TotalTestCount;
						}
					}
					return accumulator;
				}
			}
		}

		public int TotalPassed
		{
			get
			{
				if (Test is TestCase)
					return ResultState == NUnitLite.ResultState.Success ? 1 : 0;
				else
				{
					int accumulator = 0;
					if (Results != null)
					{
						foreach (TestResult result in Results)
						{
							accumulator += result.TotalPassed;
						}
					}
					return accumulator;
				}
			}
		}

		public ITest Test
		{
			get { return test; }
		}

		public ResultState ResultState
		{
			get { return resultState; }
		}

		public IList Results
		{
			get
			{
				if (results == null)
					results = new ArrayList();

				return results;
			}
		}

		public bool Executed
		{
			get { return resultState != ResultState.NotRun; }
		}

		public bool IsSuccess
		{
			get { return resultState == ResultState.Success; }
		}

		public bool IsFailure
		{
			get { return resultState == ResultState.Failure; }
		}

		public bool IsError
		{
			get { return resultState == ResultState.Error; }
		}

		public string Message
		{
			get
			{
				if (Test != null && (Test.Output ?? String.Empty).Length != 0)
				{
					return String.Format(@"===== Start of output: =====

{0}

===== End of output =====

{1}", Test.Output, message);
				}
				return message;
			}
		}

#if !NETCF_1_0
		public string StackTrace
		{
			get { return stackTrace; }
		}
#endif

		public void AddResult(TestResult result)
		{
			if (results == null)
				results = new ArrayList();

			results.Add(result);

			switch (result.ResultState)
			{
				case ResultState.Error:
				case ResultState.Failure:
					this.Failure("Component test failure");
					break;
				default:
					break;
			}
		}

		private void SetMessage(string msg)
		{
			StringBuilder tempBuilder = new StringBuilder(message ?? string.Empty);
			if (tempBuilder.Length != 0)
				tempBuilder.Append(Env.NewLine);
			tempBuilder.Append(msg ?? string.Empty);

			message = tempBuilder.ToString();
		}

		public void Success()
		{
			this.resultState = ResultState.Success;
			SetMessage(null);
		}


		public void Failure(string message)
		{
			this.resultState = ResultState.Failure;
			SetMessage(message);
		}

		public void Error(string message)
		{
			this.resultState = ResultState.Error;
			SetMessage(message);
		}

#if !NETCF_1_0
		public void Failure(string message, string stackTrace)
		{
			this.Failure(message);
			this.stackTrace = stackTrace;
		}
#endif

		public void Error(Exception ex)
		{
			this.resultState = ResultState.Error;
			//added by Andras to make debugging task-based tests easier
			if (ex is AggregateException)
			{
				AggregateException aex = ((AggregateException)ex).Flatten();
				StringBuilder tempBuilder = new StringBuilder();
				tempBuilder.AppendFormat("{0} : {1}", ex.GetType().ToString(), ex.Message);
				tempBuilder.AppendLine();
				tempBuilder.AppendFormat("Inner exceptions: {0}", aex.InnerExceptions.Count);
				tempBuilder.AppendLine();
				tempBuilder.AppendLine("=====================");
				foreach (var innerEx in aex.InnerExceptions)
				{
					tempBuilder.AppendFormat("{0} : {1}", innerEx.GetType().ToString(), innerEx.Message);
					tempBuilder.AppendLine("Stack Trace:");
					tempBuilder.AppendLine(innerEx.StackTrace);
				}
				SetMessage(tempBuilder.ToString());
			}
			else
				SetMessage(ex.GetType().ToString() + " : " + ex.Message);
#if !NETCF_1_0
			this.stackTrace = ex.StackTrace;
#endif
		}

		public void NotRun(string message)
		{
			this.resultState = ResultState.NotRun;
			SetMessage(message);
		}

		public void RecordException(Exception ex)
		{
			if (ex is AssertionException)
#if NETCF_1_0
		this.Failure(ex.Message);
#else
				this.Failure(ex.Message, StackFilter.Filter(ex.StackTrace));
#endif
			else
				this.Error(ex);
		}

		public void Write(string format, params object[] args)
		{
			if (Test != null)
				Test.Write(format, args);
		}

		public void WriteLine(string format, params object[] args)
		{
			Write(format, args);
			Write(Env.NewLine);
		}
	}
}
