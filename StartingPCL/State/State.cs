using System;

namespace StartingPCL
{
	/// <summary>
	/// Application state.
	/// </summary>
	public class State
	{
		public StateNews StateNews = new StateNews();

		public override string ToString ()
		{
			return string.Format ("[State: StateNews={0}]", StateNews);
		}
		
	}
}

