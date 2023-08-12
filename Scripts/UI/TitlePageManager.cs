using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// タイトルページのボタンなどのUI操作を管理するモジュール
/// </summary>
public class TitlePageManager : MonoBehaviour
{
    //スタートボタン
    [SerializeField] Button startButton;
    //ローディングページ
    [SerializeField] GameObject loadingPage;
    [SerializeField] GameObject titlePage;
    [SerializeField] private Slider loadSlider;
    [SerializeField] private TMP_Text loadText;
    /// <summary>
    /// ゲーム起動時の処理を行う
    /// </summary>
    void Start()
    {
        //スタートボタンが押された時の処理
        startButton.onClick.AddListener(() =>
        {
            //ローディング画面のオンにする
            loadingPage.SetActive(true);
            titlePage.SetActive(false);
            StartCoroutine(LoadScene());
        });
    }

    /// <summary>
    /// ローディング処理を行う
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
        while (!async.isDone)
        {
            loadSlider.value = async.progress;
            loadText.text =((int)(async.progress * 100)).ToString() + "%";
            yield return null;
        }
        loadingPage.SetActive(false);
    }
}