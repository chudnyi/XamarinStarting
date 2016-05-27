using System;
using System.Threading.Tasks;

namespace StartingShared
{
	public interface INetworkService
	{
		Task<Task> RequestResource<T> (string srg);
	}
}

