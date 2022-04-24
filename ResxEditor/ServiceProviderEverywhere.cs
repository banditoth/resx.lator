using System;
namespace ResxEditor
{
	public static class ServiceProviderEverywhere
	{
		public static IServiceProvider Instance { get; set; }

		public static void SetProvider(IServiceProvider provider)
        {
			Instance = provider;
        }
	}
}

