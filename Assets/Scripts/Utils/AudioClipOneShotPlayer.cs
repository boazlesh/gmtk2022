using UnityEngine;

public static class AudioClipOneShotPlayer
{
    public static void SpawnOneShot(AudioClip audioClip, float pitch = 1.0f)
    {
        GameObject spawnedObject = new GameObject(nameof(AudioClipOneShotPlayer));
        AudioSource audioSource = spawnedObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.Play();

        GameObject.Destroy(spawnedObject, audioClip.length + 0.01f);
    }
}
