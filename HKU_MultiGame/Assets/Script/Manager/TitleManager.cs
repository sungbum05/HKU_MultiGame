using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class TutorialData
{
    public Sprite sprites;
    public string Texts;
}

public class TitleManager : MonoBehaviour
{
    public int StoryIndex = 0;
    public int TutorialIndex = 0;

    public List<TutorialData> StoryData;
    public List<TutorialData> TutotialData;

    public GameObject StoryPan;
    public Image StoryPanImage;
    public Text StoryPanText;

    public GameObject TutorialPan;
    public Image TutorialPanImage;

    //게임 시작
    public void GameStart()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    //스토리
    public void OnStoryPan()
    {
        StoryPan.SetActive(true);
        StoryIndex = 0;

        StoryPanImage.sprite = StoryData[StoryIndex].sprites;
        StoryPanText.text = StoryData[StoryIndex].Texts;
    }

    public void OffStoryPan()
    {
        StoryPan.SetActive(false);
    }

    public void NextStory()
    {
        if(StoryIndex + 1 < StoryData.Count)
        {
            StoryIndex++;
            StoryPanImage.sprite = StoryData[StoryIndex].sprites;
            StoryPanText.text = StoryData[StoryIndex].Texts;
        }
    }

    //튜토리얼
    public void OnTutorialPan()
    {
        TutorialPan.SetActive(true);
        TutorialIndex = 0;

        TutorialPanImage.sprite = TutotialData[TutorialIndex].sprites;
    }

    public void OffTutorialPan()
    {
        TutorialPan.SetActive(false);
    }

    public void NextTutorial()
    {
        if (TutorialIndex + 1 < TutotialData.Count)
        {
            TutorialIndex++;
            TutorialPanImage.sprite = TutotialData[TutorialIndex].sprites;
        }
    }

    //나가기
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
