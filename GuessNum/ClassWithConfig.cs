using Microsoft.Extensions.Configuration;

namespace Hmw.GuessNum;

abstract class ClassWithConfig
{
	protected IConfigurationRoot config;
	
	public ClassWithConfig (IConfigurationRoot config)
	{
		this.config = config;
	}
}
