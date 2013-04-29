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
    /// Keeps track of various elements of the item view
    /// that is used in the test results list adapter
    /// </summary>
    public class TestResultViewHolder: Java.Lang.Object
    {
        private TextView _descriptionTextView;
				private TextView _countTextView;
        private View _indicatorView;

        /// <summary>
        /// Initializes a new instance of <see cref="TestResultViewHolder"/>
        /// </summary>
        /// <param name="indicatorView"></param>
        /// <param name="descriptionTextView"></param>
        public TestResultViewHolder(View indicatorView, TextView descriptionTextView, TextView countTextView)
        {
            _descriptionTextView = descriptionTextView;
            _indicatorView = indicatorView;
						_countTextView = countTextView;
        }

        /// <summary>
        /// Gets the indicator view
        /// </summary>
        public View IndicatorView
        {
            get { return _indicatorView; }
        }

        /// <summary>
        /// Gets the view for displaying the description
        /// </summary>
        public TextView DescriptionTextView
        {
            get { return _descriptionTextView; }
        }
			/// <summary>
			/// Gets the view for displaying the result counts
			/// </summary>
				public TextView CountTextView
				{
					get { return _countTextView; }
				}
    }
}