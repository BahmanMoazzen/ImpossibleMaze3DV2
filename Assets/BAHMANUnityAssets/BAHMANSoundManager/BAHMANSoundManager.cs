/* 
 * BAHMAN Sound Manager V1.1
 * encapsulates all the sound FX of the game 
 * any code use this code by singleton referencing
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// all the game sound FXs. CHANEG TO MATCH THE GAME NEEDS
/// </summary>
public enum GameSounds { Step, GameOver, ButtomClicked, ButtonSlide, SceneTransition, Pickup, Hit, StartGame, EndGame }


public class BAHMANSoundManager : MonoBehaviour
{
    public static event UnityAction<GameSounds, float> OnClipStarted, OnClipEnded;
    public static BAHMANSoundManager _Instance;
    [SerializeField] GameSoundStructure[] _sounds;
    [SerializeField] AudioSource _audioSource;
    private void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);



    }
    private void OnEnable()
    {
        /// listening to the game setting change of sound fx
        GameSettingInfo.OnSoundFXChange += GameSettingInfo_OnSoundFXChange;
    }



    private void OnDisable()
    {
        GameSettingInfo.OnSoundFXChange -= GameSettingInfo_OnSoundFXChange;
    }

    private void GameSettingInfo_OnSoundFXChange(bool iEnable)
    {
        if (iEnable)
        {
            /// should enable/disable the sound fx

            //_audioSource.PlayOneShot(_sounds[(int)GameSounds.FirstMerge].AudioClips[0]);
        }

    }

    public void _PlaySound(GameSounds iSound)
    {
        if (GameSettingInfo.Instance.SoundFX)
        {
            foreach (var sound in _sounds)
            {
                /// check if the clip found

                if (sound.Sound == iSound)
                {
                    /// playes a random variant
                    var clipFX = sound.AudioClips[UnityEngine.Random.Range(0, sound.AudioClips.Count)];
                    _audioSource.PlayOneShot(clipFX);
                    OnClipStarted?.Invoke(iSound, clipFX.length);
                    /// rase the event after clip is done playing
                    StartCoroutine(_alarmTheEndOfClip(clipFX.length, iSound));

                }
            }
        }

    }
    /// <summary>
    /// waits till the end of clip then fires the OnClipEnded event
    /// </summary>
    /// <param name="iTime">the duration of the clip and the amount to wait</param>
    /// <param name="iClip">the GameSound to pass to the event</param>
    /// <returns>waits till the end of the clip</returns>
    IEnumerator _alarmTheEndOfClip(float iTime, GameSounds iClip)
    {
        yield return new WaitForSeconds(iTime);
        OnClipEnded?.Invoke(iClip, iTime);


    }

}

/// <summary>
/// the structure to save the game sound fxs. any sound can have multiple variants
/// </summary>
[Serializable]
public struct GameSoundStructure
{
    public GameSounds Sound;
    public List<AudioClip> AudioClips;
}


