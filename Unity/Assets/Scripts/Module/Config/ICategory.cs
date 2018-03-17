using System;
using System.ComponentModel;

namespace ETModel
{
	public interface ICategory: ISupportInitialize
	{
		Type ConfigType { get; }
	}
}