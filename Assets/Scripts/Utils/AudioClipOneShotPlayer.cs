using UnityEngine;

public static class AudioClipOneShotPlayer
{
    public static void SpawnOneShot(AudioClip audioClip)
    {
        GameObject spawnedObject = new GameObject(nameof(AudioClipOneShotPlayer));
        AudioSource audioSource = spawnedObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.Play();

        GameObject.Destroy(spawnedObject, audioClip.length + 0.01f);
    }
}
