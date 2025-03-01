using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;
    private void Start()
    {
        if (!SaveManager.instance.HasSavedData())
        {
            continueButton.SetActive(false);
        }
    }


    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.75f));
   }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1.75f));
    } 
    public void ExitGame()
    {
        Debug.Log("Exit");
        //Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
