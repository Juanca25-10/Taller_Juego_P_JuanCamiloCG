using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Efectos_Botones : MonoBehaviour

{
    //public AudioSource source { get { return GetComponent<AudioSource>(); } }
    //public Button btn { get { return GetComponent<Button>(); } }
    //public AudioClip clip;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    gameObject.AddComponent<AudioSource>();
    //    btn.onClick.AddListener(PlaySound);
    //}

    //public void PlaySound()
    //{
    //    source.PlayOneShot(clip);
    //}

    public AudioSource sound;
    public AudioClip SoundMenu;

    public void SoundButton()
    {
        sound.clip = SoundMenu;

        sound.enabled = false;
        sound.enabled = true;
    }
}
