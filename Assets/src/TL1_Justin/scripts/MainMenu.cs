using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuScreen;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/Scenes/Level666.unity");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/Scenes/EasterLevel.unity");
        }
    }

    public void PlayButton()
    {
        // Play button has been pressed, initialize first game level
        // Debug.Log("Play button pressed");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel1");
        // Assets/Scenes/Level1.unity
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/src/TL2_Malik/Assets/Scenes/SampleScene.unity");
        SoundManager.GetInstance().buttonSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/Scenes/Level1.unity");
    }

    public void HelpButton()
    {
        // Help button has been pressed, show help menu
        Debug.Log("Help button pressed");
        SoundManager.GetInstance().buttonSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HelpMenu");
    }

    public void QuitButton()
    {
        // Debug.Log("Quit button pressed");
        SoundManager.GetInstance().buttonSound();
        Application.Quit();
    }
}
