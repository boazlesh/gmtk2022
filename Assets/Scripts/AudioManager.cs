using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;
    private float _originalVolume;

    public static AudioManager Instance
    {
        get
        {
            return _audioManager;
        }
    }

    private AudioSource _audioSource;

    public void Awake()
    {
        if (_audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            _audioManager = this;
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChillOut()
    {
        _originalVolume = _audioSource.volume;
        _audioSource.volume = _originalVolume * 0.7f;
    }

    public void BlastIt()
    {
        _audioSource.volume = _originalVolume;
    }
}
