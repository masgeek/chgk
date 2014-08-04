﻿using System;
using System.Linq;
using Android.App;
using Android.Content;
using ChGK.Core;
using ChGK.Core.ViewModels;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace ChGK.Droid.Views
{
	public class EnterResultsView : MvxDialogFragment
	{
		public override Dialog OnCreateDialog (Android.OS.Bundle savedInstanceState)
		{
			var builder = new AlertDialog.Builder (Activity);
			var viewModel = ViewModel as EnterResultsViewModel;

			builder.SetMultiChoiceItems (
				viewModel.Teams.Select (result => result.Name).ToArray (), 
				viewModel.Teams.Select (result => result.AnsweredCorrectly).ToArray (), 
				new EventHandler<DialogMultiChoiceClickEventArgs> ((sender, e) => viewModel.Teams [e.Which].AnsweredCorrectly = e.IsChecked));
			builder.SetCancelable (true);
			builder.SetPositiveButton (StringResources.Save, new EventHandler<DialogClickEventArgs> ((s, e) => viewModel.SubmitResults ()))
				.SetNegativeButton (StringResources.Cancel, (EventHandler<DialogClickEventArgs>)null);

			return builder.Create ();
		}
	}
}

