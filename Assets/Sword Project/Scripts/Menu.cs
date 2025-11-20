using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadLevel(string levelName)
    {
        FindObjectOfType<GameManager>().ResetTimeScale();
        FindObjectOfType<AudioManager>().StopBackgroundMusic();

        SceneManager.LoadScene(levelName);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
