﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DevDaysSpeakers.Model;
using DevDaysSpeakers.Services;
using ReactiveUI;
using Xamarin.Forms;
using Xamvvm;

namespace DevDaysSpeakers.Features.Speakers
{
	public class SpeakersViewModel
		: BasePageModelRxUI
	{
		public IReactiveList<Speaker> Speakers { get; } = new ReactiveList<Speaker>();

		Speaker speaker;
		public Speaker Speaker
		{
			get { return speaker; }
			set { this.RaiseAndSetIfChanged(ref speaker, value); }
		}

		public ReactiveCommand<Unit, IEnumerable<Speaker>> GetSpeakers { get; }

		readonly ObservableAsPropertyHelper<bool> busy;
		public bool IsBusy => busy.Value;

        public Interaction<Exception, Unit> ConnectionError { get; } = new Interaction<Exception, Unit>();

        public SpeakersViewModel()
			: this(null)
		{

		}

		public SpeakersViewModel(AzureService azureService = null)
		{
			var service = azureService ?? DependencyService.Get<AzureService>();

			GetSpeakers = ReactiveCommand.CreateFromObservable(() => service.GetSpeakers());

			GetSpeakers
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(speakers =>
				{
					Speakers.Clear();
					Speakers.AddRange(speakers);
				});

			GetSpeakers.IsExecuting
				.ToProperty(this, vm => vm.IsBusy, out busy);

            GetSpeakers.ThrownExceptions
                .SelectMany(error => ConnectionError.Handle(error))
                .Subscribe();

			// go to details page when Speaker is set
			this.WhenAnyValue(vm => vm.Speaker)
				.Where(speaker => speaker != null)
				.Subscribe(speaker => this.PushPageFromCacheAsync<Details.DetailsViewModel>(vm => vm.Speaker = speaker));

			this.ThrownExceptions
				.Subscribe(ex =>
			    {
				    Debug.WriteLine(ex.Message);
			    });
		}
	}
}