using Assets.Scripts;
using TMPro;
using UnityEngine;

namespace Assets
{
    public class DieRollWindow : MonoBehaviour
    {
        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Die _diePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _sumLabel;

        private int _sum = 0;

        private void Awake()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }

            _sumLabel.text = "0";
        }

        private void OnValidate()
        {
            foreach (Transform child in _content)
            {
                Die die = child.GetComponent<Die>();

                if (die != null)
                {
                    die.SetColor(_faceColor);
                }
            }
        }

        public void AddDie(DieFace dieFace)
        {
            Die die = Instantiate(_diePrefab, _content);

            die.SetColor(_faceColor);
            die.SetFace(dieFace);

            _sum += DieFaceHelper.GetNumericValue(dieFace);

            _sumLabel.text = _sum.ToString();
        }

        [ContextMenu("Add Die")]
        public void AddDieDebug()
        {
            AddDie(DieFace.Five);
        }
    }
}
