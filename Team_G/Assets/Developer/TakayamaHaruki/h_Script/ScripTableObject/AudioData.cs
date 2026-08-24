using UnityEngine;

public enum AudioType
{
    BGM,
    SE,
}

[CreateAssetMenu(fileName = "AudioGroup", menuName = "ScriptableObjects/AudioGroup")]
public class AudioGroup : ScriptableObject
{
   public AudioType AuidoType;
    public AudioData[] AudioDatas;
}

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioType AuidoType;
    public AudioClip[] Audio;
}