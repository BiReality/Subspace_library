using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class AssetBundleBuilder : MonoBehaviour {

	[MenuItem("Build/Build Asset Bundle")]
	static void BuildAssetBundle () {
		File.Copy("Library/ScriptAssemblies/Assembly-CSharp.dll", "Assets/Resources/code.bytes", true);
		// string[] assetNames = System.IO.Directory.GetFiles("Assets/Resources", "*prefab");
		List<string> assetNamesList = new List<string>(Directory.GetFiles("Assets/Resources", "*prefab"));
		assetNamesList.Add("Assets/Resources/code.bytes");
		string[] assetNames = assetNamesList.ToArray();
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
		buildMap[0].assetBundleName = "subspaceassetbundle";
		buildMap[0].assetNames = assetNames;
		string outputPath = "Assets/Resources/AB";
		Directory.CreateDirectory(outputPath);
		BuildPipeline.BuildAssetBundles(
			outputPath, buildMap, 
			BuildAssetBundleOptions.None, 
			BuildTarget.StandaloneWindows64);
	}

}
