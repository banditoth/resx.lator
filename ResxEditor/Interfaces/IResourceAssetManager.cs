using System;
using ResxEditor.Entities;

namespace ResxEditor.Interfaces
{
	public interface IResourceAssetManager
	{
		ResourceAsset GetResourceAssetFromFiles(params string[] paths);

		void SaveResourceAssetToFiles(ResourceAsset toSave);
	}
}

