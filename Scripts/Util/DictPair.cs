using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

/// <summary>
/// List<DictPair<Key,Value>>の形で作成する
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[System.Serializable]
public class DictPair<TKey, TValue>
{
    [SerializeField] private TKey key;
    [SerializeField] private TValue value;

    public TKey Key => key;
    public TValue Value => value;

    //SearchKeyがkeyと一致するか判定するメソッド
    public bool IsEquelKey(TKey searchKey)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, searchKey))
        {
            return true;
        }
        // Keyが一致しない場合、デフォルト値を返すか例外をスローするなどのエラーハンドリングを行うこともできます。
        // ここではデフォルト値を返す例を示しています。
        return false;
    }
}
