using System;
using UnityEngine;

[Serializable]
public class Soundbank
{
    public string name;

    public AudioClip[] soundClips;
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource tempSource;

    public Soundbank[] soundBanks;

    public void PlaySpecifiedSound(int inSoundBank, int inSoundClipIndex)
    {
        tempSource.PlayOneShot(soundBanks[inSoundBank].soundClips[inSoundClipIndex]);
    }

    public void PlayRandomSound(int inSoundBank)
    {
        int chosenClip = UnityEngine.Random.Range(0, soundBanks[inSoundBank].soundClips.Length);

        tempSource.PlayOneShot(soundBanks[inSoundBank].soundClips[chosenClip]);
    }

    public void QueueDestroy()
    {
        Destroy(gameObject, 4); //Destroy after 4 seconds
    }
}
