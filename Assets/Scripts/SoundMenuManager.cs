using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenuManager : MonoBehaviour
{
    public AudioSource transSource;
    public AudioSource clickSound;
    public AudioClip[] panelTransSounds;
    public AudioClip slaySound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickSound.Play();
        }
    }

    public void PlayRandomTrans()
    {
        int index = Random.Range(1, panelTransSounds.Length);
        transSource.clip = panelTransSounds[index];
        transSource.Play();
    }

    public void PlaySlaySound()
    {
        transSource.clip = slaySound;
        transSource.Play();
    }
}
