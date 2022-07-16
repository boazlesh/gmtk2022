using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;

    private void Awake()
    {
        if (_audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            _audioManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
