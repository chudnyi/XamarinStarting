using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace StartingPCL
{
	public class ViewModelBase : MvvmHelpers.BaseViewModel
	{
		protected Subject<bool> Active = new Subject<bool>();
		protected IObservable<bool> Activated;
		protected IObservable<bool> Deactivated;

		public ViewModelBase ()
		{
			Activated = Active
				.Where (value => value);

			Deactivated = Active
				.Where (value => !value);
		}

		public void OnAppearing ()
		{
			Active.OnNext (true);
		}

		public void OnDisappearing ()
		{
			Active.OnNext (false);
		}
	}
}

