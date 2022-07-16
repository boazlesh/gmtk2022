using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private DieFace _dieFace;
        [SerializeField] private Image _dieImage;
        [SerializeField] private DieFaceMapping _dieFaceMapping;

        private void OnValidate()
        {
            SetFace(_dieFace);
        }

        public void SetFace(DieFace dieFace)
        {
            _dieFace = dieFace;
            _dieImage.sprite = _dieFaceMapping.GetSprite(dieFace);
        }
    }
}
