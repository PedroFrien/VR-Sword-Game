using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarImage : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    
    [SerializeField] private float lerpSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        lerpSpeed = lerpSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeValue(float value)
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, value, lerpSpeed);
    }
}
