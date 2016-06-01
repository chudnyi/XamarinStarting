using System;

namespace StartingPCL
{
	public class EntityId
	{
		private readonly string value;

		public EntityId (string aValue)
		{
			value = aValue;
		}

		public static implicit operator EntityId(string b)
		{
			return new EntityId(b);
		}
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(EntityId))
				return false;
			EntityId other = (EntityId)obj;
			return value == other.value;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return value?.GetHashCode () ?? 0;
			}
		}

		public override string ToString ()
		{
			return value;
		}
		
	}
}

