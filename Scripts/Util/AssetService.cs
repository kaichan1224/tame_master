using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アセット(プレハブ)の読みこみを行うクラス
/// </summary>
public class AssetService
{
    /// <summary>
    /// キャッシュしたAssets
    /// key: ファイル名
    /// </summary>
    private readonly IDictionary<string, GameObject> _cachedAssetsDictionary = new Dictionary<string, GameObject>();

    /// <summary>
    /// Prefab格納パス
    /// </summary>
    private static readonly string PrefabPath = "Prefab/";

    /// <summary>
    /// Assetsファイルの読み込み
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>Asset(GameObject)</returns>
    public GameObject LoadAssets(string fileName)
    {
        // ファイル名をキーとしてキャッシュする
        if (!_cachedAssetsDictionary.ContainsKey(fileName))
        {
            var asset = Resources.Load(PrefabPath + fileName) as GameObject;
            _cachedAssetsDictionary.Add(fileName, asset);
        }
        return _cachedAssetsDictionary[fileName];
    }
}
