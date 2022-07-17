using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Die : MonoBehaviour
    {
        private const float _secondsBetweenChecks = 0.1f;

        [SerializeField] private DieFace _dieFace;
        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Image _dieImage;
        [SerializeField] private DieFaceMapping _dieFaceMapping;
        [SerializeField] private FaceColorMapping _faceColorMapping;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void OnValidate()
        {
            SetFace(_dieFace);
            SetColor(_faceColor, mute: false);
        }

        public IEnumerator RollRandomFacesRoutine(float timeSeconds)
        {
            //timeSeconds = Mathf.Min(timeSeconds, _audioClip.length); // boaz: how about no

            _audioSource.PlayOneShot(_audioClip);

            while (timeSeconds > 0)
            {
                yield return new WaitForSeconds(_secondsBetweenChecks);

                SetFace(DieFaceHelper.Roll());

                timeSeconds -= _secondsBetweenChecks;
            }
        }

        public void SetFace(DieFace dieFace)
        {
            _dieFace = dieFace;
            _dieImage.sprite = _dieFaceMapping.GetSprite(dieFace);
        }

        public void SetColor(FaceColor faceColor, bool mute = false)
        {
            _faceColor = faceColor;
            _dieImage.color = _faceColorMapping.GetColorFromFaceColor(faceColor, mute);
        }
    }
}
