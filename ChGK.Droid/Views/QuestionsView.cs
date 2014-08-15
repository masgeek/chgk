﻿using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using ChGK.Core.Utils;
using ChGK.Core.ViewModels;
using Cirrious.MvvmCross.Droid.Fragging;
using System.Collections.Generic;

namespace ChGK.Droid.Views
{
	[Android.App.Activity (Label = "Вопрос 1", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]			
	public class QuestionsView : MvxFragmentActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);            
			SetContentView (Resource.Layout.QuestionsView);

			var viewPager = FindViewById<ViewPager> (Resource.Id.viewPager);
			viewPager.Adapter = new QuestionsPagerAdapter (SupportFragmentManager, ((QuestionsViewModel)ViewModel).Questions);
			viewPager.PageSelected += (sender, e) => ActionBar.Title = viewPager.Adapter.GetPageTitle (e.Position);
			viewPager.CurrentItem = ((QuestionsViewModel)ViewModel).Index;

			ActionBar.SetDisplayHomeAsUpEnabled (true);
		}

		public override bool OnOptionsItemSelected (Android.Views.IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home) {
				OnBackPressed ();
				return true;
			} else {
				return base.OnOptionsItemSelected (item);
			}
		}

        protected override void OnDestroy()
        {
            if (ViewModel is IViewLifecycle)
            {
                (ViewModel as IViewLifecycle).OnViewDestroying();
            }

            base.OnDestroy();
        }
	}

	class QuestionsPagerAdapter : FragmentStatePagerAdapter
	{
		List<QuestionViewModel> _questions;

		public QuestionsPagerAdapter (FragmentManager fm, List<QuestionViewModel> questions) : base (fm)
		{
			_questions = questions;
		}

		public override int Count {
			get {
				return _questions.Count;
			}
		}

		public override Fragment GetItem (int position)
		{
			var fragment = new QuestionView ();
			fragment.RetainInstance = true;
			fragment.ViewModel = _questions [position];
			return fragment;
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String ("Вопрос " + (position + 1));
		}
	}
}

