using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleBuilder : MonoBehaviour {

	[MenuItem("Build/Build Asset Bundle")]
	static void BuildAssetBundle () {
		string[] assetNames = System.IO.Directory.GetFiles("Assets/Resources", "*prefab");
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
		buildMap[0].assetBundleName = "subspaceassetbundle";
		buildMap[0].assetNames = assetNames;
		string outputPath = "Assets/Resources/AB";
		System.IO.Directory.CreateDirectory(outputPath);
		BuildPipeline.BuildAssetBundles(
			outputPath, buildMap, 
			BuildAssetBundleOptions.None, 
			BuildTarget.StandaloneWindows64);
	}

}
