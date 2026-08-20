using UnityEngine;

public enum AudioType
{
    BGM,
    SE,
}

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData")]
public class AudioData : ScriptableObject
{
   public AudioType auido_type;
    public AudioClip[] audio;
}
