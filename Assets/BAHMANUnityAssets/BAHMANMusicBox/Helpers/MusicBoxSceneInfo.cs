using UnityEngine;
[CreateAssetMenu(fileName = "MusicBoxSceneInfo", menuName = "BAHMAN Unity Assets/Musics And Sounds/Music Box Scene Info", order = 1)]

public class MusicBoxSceneInfo : ScriptableObject
{


    [Tooltip("The scene name of the setting")]
    public AllScenes _SceneName;
    [Tooltip(@"All the scene's clips")]
    public AudioClip[] _SceneMusics;
    [Tooltip("The global volume of music clips")]
    [Range(0f, 1f)]
    public float _GlobalMusicVolume = 1;
    [Tooltip("The style of choosing next clip")]
    public bool _ShufflePlay;
    [Tooltip("Should music be stoped gradualy")]
    public bool _StopMusicGradually;
    [Tooltip("The amount of end of music clip to fade to silence")]
    [Range(.1f, 2f)]
    public float _FadeInterval;
    [Tooltip("The amount of silence between music clips")]
    [Range(0f, 10f)]
    public float _SilenceBetweenClips;
    [Tooltip("Only works on non-random paly mode")]
    public bool _StopAfterListEnded;
    [Tooltip("Repeat Selected Song Only")]
    public bool _RepeatSelectedSound;
    [Tooltip("Whether to show message or not")]
    public bool _ShowSoundNameAsMessage ;

}


