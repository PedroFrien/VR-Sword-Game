using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private float originalTimeScale;
    private float fixedTime = 0f;
    private float maxFixedTime = 0f;


    private bool isBulletTimeActive = false;
    private float bulletTimeEndTime = 0f;





    // Start is called before the first frame update
    void Start()
    {
        originalTimeScale = Time.timeScale;
        fixedTime = Time.fixedDeltaTime;
        maxFixedTime = Time.fixedDeltaTime;

        FindObjectOfType<AudioManager>().PlayBackgroundMusic("Ambiance");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBulletTime(float slowAmount, float slowDuration)
    {
        if (isBulletTimeActive)
        {
            bulletTimeEndTime = Time.realtimeSinceStartup + slowDuration;
            Debug.Log("Extending bullet time");
        }

        else
        {
            StartCoroutine(BulletTime(slowAmount, slowDuration));
        }
    }

    public IEnumerator BulletTime(float slowAmount, float slowDuration)
    {
        isBulletTimeActive = true;
        Debug.Log("Starting bullet time");
        Time.timeScale = slowAmount;
        Time.fixedDeltaTime = Mathf.Clamp(fixedTime * Time.timeScale, 0f, maxFixedTime);

        bulletTimeEndTime = Time.realtimeSinceStartup + slowDuration;

        while (Time.realtimeSinceStartup < bulletTimeEndTime)
        {
            yield return null;
        }


        Debug.Log("Ending bullet time");
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = maxFixedTime;
        isBulletTimeActive = false;

    }

    public void ChangeTimeScale(float timeValue)
    {
        Time.timeScale = timeValue;


    }

    public void LoadLevel(string levelName)
    {
        ChangeTimeScale(originalTimeScale);
        SceneManager.LoadScene(levelName);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
