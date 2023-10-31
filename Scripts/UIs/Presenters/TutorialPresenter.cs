using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using UnityEngine.SceneManagement;

public class TutorialPresenter : MonoBehaviour
{
    [SerializeField] private TutorialView tutorialView;
    TutorialMasterData tutorialMasterData;
    UserDataModel userDataModel;
    private int tutorialIndex = 0;

    [Inject]
    public void Inject(TutorialMasterData tutorialMasterData,UserDataModel userDataModel)
    {
        this.tutorialMasterData = tutorialMasterData;
        this.userDataModel = userDataModel;
    }
    void Start()
    {
        SoundManager.instance.StopBGM();
        StartCoroutine(start());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && tutorialView.isEndWord)
        {
            Next();
        }
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1f);
        Next();
    }

    void Next()
    {
        tutorialView.isEndWord = false;
        tutorialView.ResetWord();
        var story = tutorialMasterData.story[tutorialIndex];
        switch (story.id)
        {
            case 0:
                Debug.Log(0);
                StartCoroutine(tutorialView.SetWord(story.words));
                break;
            case 1:
                Debug.Log(1);
                StartCoroutine(tutorialView.SetName(story.words));
                break;
            case 2:
                Debug.Log(2);
                userDataModel.FinishTutorial();
                SceneManager.LoadScene("MainScene");
                break;
        }
        tutorialIndex++;
    }
}
