using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using VContainer;

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
    UserDataModel userDataModel;

    [Inject]
    public void Inject(UserDataModel userDataModel)
    {
        this.userDataModel = userDataModel;
    }
    /// <summary>
    /// ゲーム起動時の処理を行う
    /// </summary>
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundName.タイトルbgm);
        //スタートボタンが押された時の処理
        startButton.onClick.AddListener(() =>
        {
            //ローディング画面のオンにする
            loadingPage.SetActive(true);
            titlePage.SetActive(false);
            if (userDataModel.isFinishTutorial)
                StartCoroutine(LoadMainScene());
            else
                StartCoroutine(LoadTutorialScene());
        });
    }

    /// <summary>
    /// ローディング処理を行う
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadMainScene()
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

    /// <summary>
    /// ローディング処理を行う
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadTutorialScene()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync("TutorialScene");
        while (!async.isDone)
        {
            loadSlider.value = async.progress;
            loadText.text = ((int)(async.progress * 100)).ToString() + "%";
            yield return null;
        }
        loadingPage.SetActive(false);
    }
}