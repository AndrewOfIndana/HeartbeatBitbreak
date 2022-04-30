using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    //Properties
    public AudioSource audioPlayer;
    public enum SoundEffectTags  {CORRECT_HIT, MISSED_INPUT, PLAYER_HIT, ENEMY_HIT,PLAYER_DEATH, ENEMY_DEATH};

    //clips
    public AudioClip CORRECT_HIT;
    public AudioClip MISSED_INPUT;
    public AudioClip PLAYER_HIT;
    public AudioClip ENEMY_HIT;
    public AudioClip PLAYER_DEATH;
    public AudioClip ENEMY_DEATH;

    private Dictionary<SoundEffectTags, AudioClip> SFXDict = new Dictionary<SoundEffectTags, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        SFXDict.Add(SoundEffectTags.CORRECT_HIT, CORRECT_HIT);
        SFXDict.Add(SoundEffectTags.MISSED_INPUT, MISSED_INPUT);
        SFXDict.Add(SoundEffectTags.PLAYER_HIT,PLAYER_HIT);
        SFXDict.Add(SoundEffectTags.ENEMY_HIT, ENEMY_HIT);
        SFXDict.Add(SoundEffectTags.PLAYER_DEATH, PLAYER_DEATH);
        SFXDict.Add(SoundEffectTags.ENEMY_DEATH, PLAYER_DEATH);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(SoundEffectTags tag) 
    {
        AudioClip clip = null;

        clip = SFXDict[tag];

        audioPlayer.PlayOneShot(clip);
    }
}
