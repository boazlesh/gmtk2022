using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Instructions : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private AudioSource _audioSource;

        private Input _input;

        private void Awake()
        {
            _input = new Input();

            _input.BattleActionMap.Continue.performed += ContinuePerformed;

            _input.Enable();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void ContinuePerformed(InputAction.CallbackContext obj)
        {
            StartCoroutine(FadeOut(1f));
            _levelLoader.LoadNextLevel();
        }

        public IEnumerator FadeOut(float fadeTimeSeconds)
        {
            float startVolume = _audioSource.volume;

            while (_audioSource.volume > 0)
            {
                _audioSource.volume -= startVolume * Time.deltaTime / fadeTimeSeconds;

                yield return null;
            }

            _audioSource.Stop();
            _audioSource.volume = startVolume;
        }
    }
}
