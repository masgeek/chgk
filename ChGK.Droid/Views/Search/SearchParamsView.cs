﻿using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using ChGK.Core.ViewModels.Search;
using ChGK.Droid.Controls;
using System;

namespace ChGK.Droid.Views.Search
{
    public class SearchParamsView : MenuItemView, ITextWatcher
    {
        protected override int LayoutId
        {
            get
            {
                return Resource.Layout.SearchParamsView;
            }
        }

        public new SearchParamsViewModel ViewModel
        {
            get
            {
                return base.ViewModel as SearchParamsViewModel;
            }
            set
            {
                base.ViewModel = value;
            }
        }

        EditText searchQueryEditText;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
           
            searchQueryEditText = view.FindViewById<EditText>(Resource.Id.searchQueryEditText);
            searchQueryEditText.AddTextChangedListener(this);

            var searchButton = view.FindViewById<Button>(Resource.Id.searchButton);
            searchButton.Click += searchButton_Click;

            var startDateButton = view.FindViewById<Button>(Resource.Id.start_date_button);
            startDateButton.Click += startDateButton_Click;

            var endDateButton = view.FindViewById<Button>(Resource.Id.end_date_button);
            endDateButton.Click += endDateButton_Click;
        }

        void endDateButton_Click(object sender, EventArgs e)
        {
            var dialog = DatePickerFragment.NewInstance((year, month, day) => ViewModel.EndDate = new DateTime(year, month, day),
                ViewModel.EndDate.Year, ViewModel.EndDate.Month, ViewModel.EndDate.Day);
            dialog.Show(ChildFragmentManager, "endDatePicker");
        }

        void startDateButton_Click(object sender, EventArgs e)
        {
            var dialog = DatePickerFragment.NewInstance((year, month, day) => ViewModel.StartDate = new DateTime(year, month, day),
                ViewModel.StartDate.Year, ViewModel.StartDate.Month, ViewModel.StartDate.Day);
            dialog.Show(ChildFragmentManager, "startDatePicker");
        }

        void searchButton_Click(object sender, EventArgs e)
        {
            if (!(ViewModel as SearchParamsViewModel).CanSearchWithThisParams())
            {
                Toast.MakeText(Activity, Resource.String.empty_search_params_error, ToastLength.Short).Show();
            }
            else if (string.IsNullOrEmpty(searchQueryEditText.Text))
            {
                searchQueryEditText.RequestFocus();
                AfterTextChanged(null);
            }
        }

        public void AfterTextChanged(IEditable s)
        {
            searchQueryEditText.SetError((s == null || s.Length() == 0 ? Resources.GetString(Resource.String.empty_search_error) : null), null);            
        }

        public void BeforeTextChanged(Java.Lang.ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(Java.Lang.ICharSequence s, int start, int before, int count)
        {
        }
    }
}
