using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ionic.Zip;

public class AssetBundleBuilder : MonoBehaviour {

	[MenuItem("Build/Build Asset Bundle")]
	static void BuildAssetBundle () {
		string[] assetNames = System.IO.Directory.GetFiles("Assets/Resources", "*prefab");
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
		buildMap[0].assetBundleName = "resource";
		buildMap[0].assetNames = assetNames;
		string outputPath = "Assets/AB";
		System.IO.Directory.CreateDirectory(outputPath);
		BuildPipeline.BuildAssetBundles(
			outputPath, buildMap, 
			BuildAssetBundleOptions.None, 
			BuildTarget.StandaloneWindows64);
		ZipFile zipFile = new ZipFile();
		zipFile.AddDirectory(Application.dataPath + "/AB", "AB");
		zipFile.Save("resource.zip");
		System.IO.Directory.Delete(outputPath, true);
	}

}
