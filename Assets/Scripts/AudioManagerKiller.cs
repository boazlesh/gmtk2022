using UnityEngine;

public class AudioManagerKiller : MonoBehaviour
{
    private void Awake()
    {
        // don't use "Instance", maybe alive from previous scene?.. and that throws?.. huh???

        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            return;
        }

        audioManager.FuckOff();
    }
}
