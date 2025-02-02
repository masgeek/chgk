﻿using Android.OS;
using Android.Views;
using ChGK.Core.Services;
using ChGK.Core.Utils;
using ChGK.Core.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace ChGK.Droid.Views
{
	public abstract class MenuItemView : MvxFragment
	{
		protected MenuItemView ()
		{
			RetainInstance = true;
		}

        public override void OnStart()
        {
 	         base.OnStart();
    
             Mvx.Resolve<IGAService>().ReportScreenEnter(this.GetType().FullName);            
        }

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			return this.BindingInflate (LayoutId, null);
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);

			HasOptionsMenu = true;
			Activity.Title = (ViewModel as MenuItemViewModel).Title;
		}

		protected abstract int LayoutId 
        {
			get;
		}

        public override void OnDestroy()
        {
            if (ViewModel is IViewLifecycle)
            {
                (ViewModel as IViewLifecycle).OnViewDestroying();
            }

            base.OnDestroy();
        }
	}
}

