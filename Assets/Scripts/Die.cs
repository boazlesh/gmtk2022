using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private DieFace _dieFace;
        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Image _dieImage;
        [SerializeField] private DieFaceMapping _dieFaceMapping;
        [SerializeField] private FaceColorMapping _faceColorMapping;

        private void OnValidate()
        {
            SetFace(_dieFace);
            SetColor(_faceColor);
        }

        public void SetFace(DieFace dieFace)
        {
            _dieFace = dieFace;
            _dieImage.sprite = _dieFaceMapping.GetSprite(dieFace);
        }

        public void SetColor(FaceColor faceColor)
        {
            _faceColor = faceColor;
            _dieImage.color = _faceColorMapping.GetColorFromFaceColor(faceColor);
        }
    }
}
