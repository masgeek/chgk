﻿using ChGK.Core.Models;
using ChGK.Core.Services;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChGK.Core.ViewModels.Search
{
    public class SearchQuestionsResultsViewModel : MenuItemViewModel
    {
        readonly IChGKWebService _service;

        public SearchParams _searchParams;

        public DataLoader DataLoader { get; private set; }

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public SearchQuestionsResultsViewModel(IChGKWebService service)
        {
            Title = StringResources.SearchResults;

            _service = service;

            DataLoader = new DataLoader();
        }

        public async void Init(string searchParams)
        {
            _searchParams = JsonConvert.DeserializeObject<SearchParams>(searchParams);

            await LoadMore();
        }

        async Task LoadItems()
        {
            Questions = null;

            Questions = await _service.SearchQuestions(_searchParams, _cancellationTokenSource.Token);
        }

        public async Task LoadMore()
        {
            await DataLoader.LoadItemsAsync(LoadItems);

            _searchParams.Page++;
        }

        List<ISearchResult> _questions;

        public List<ISearchResult> Questions
        {
            get
            {
                return _questions;
            }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }
    }
}