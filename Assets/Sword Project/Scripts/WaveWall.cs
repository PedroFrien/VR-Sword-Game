using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WaveWall : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private Vector3 startingPos;
    // Start is called before the first frame update

    private void Start()
    {
        startingPos = transform.position;
    }
    public void SetWall(bool gone)
    {
        if (gone == false)
        {
            StopCoroutine("MoveWall");
            transform.position = startingPos;

        }

        if (gone == true)
        {
            StartCoroutine("MoveWall");
        }

    }

    private IEnumerator MoveWall()
    {
        FindObjectOfType<AudioManager>().PlaySound("SectionUnlock", transform.position, gameObject);

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position += Vector3.up * moveSpeed;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
