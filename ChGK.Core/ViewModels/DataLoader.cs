﻿using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using ChGK.Core.DbChGKInfo;
using Cirrious.MvvmCross.ViewModels;

namespace ChGK.Core.ViewModels
{
	public class DataLoader : MvxNotifyPropertyChanged
	{
		public async Task LoadItemsAsync (Func<Task> loadDataTaskFactory,  Action BeforeLoad = null, Action AfterLoad = null)
		{
			if (IsLoading) {
				return;
			}

			HasError = false;
			IsLoading = true;

			try {
                if (BeforeLoad != null)
                {
                    BeforeLoad();
                }

				await loadDataTaskFactory ();
			} catch (NoConnectionException e) {
				Mvx.Trace (e.Message);
				Error = "Проверьте интернет соединение";
			} catch (OperationCanceledException e) {
				Mvx.Trace (e.Message);
			} catch (Exception e) {
				Mvx.Trace (e.Message);
				Error = "Не удалось загрузить данные\n" + e.Message + "\nПопробуйте еще раз";
			} finally {
                IsLoading = false;
                
                if (AfterLoad != null)
                {
                    AfterLoad();
                } 
			}
		}

        public void LoadItems(Action loadDataAction)
        {

            if (IsLoading)
            {
                return;
            }

            HasError = false;
            IsLoading = true;

            try
            {
                loadDataAction();
            }
            catch (Exception e)
            {
                Mvx.Trace(e.Message);
                Error = "Не удалось загрузить данные\n" + e.Message + "\nПопробуйте еще раз";
            }
            finally
            {
                IsLoading = false;
            }
        }

		bool _isLoading;

		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value; 

				RaisePropertyChanged (() => IsLoading);
				RaisePropertyChanged (() => HasData);
			}
		}

		string _error;

		public string Error {
			get {
				return _error;
			}
			set {
				_error = value;
				RaisePropertyChanged (() => Error);
                if (!string.IsNullOrEmpty(value))
                {
                    HasError = true;
                }
			}
		}

		bool _hasError;

		public bool HasError {
			get{ return _hasError; }
			set {
				_hasError = value;
				RaisePropertyChanged (() => HasError);
				RaisePropertyChanged (() => HasData);
			}
		}

		public bool HasData {
			get {
				return !HasError && !IsLoading; 
			}
		}
	}
}

