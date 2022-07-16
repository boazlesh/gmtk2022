using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class DieRollWindow : MonoBehaviour
    {
        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Die _diePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _sumLabel;
        [SerializeField] private int _sum = 0;
        [SerializeField] private Button _button;
        [SerializeField] private int _bustThreshold;

        private void Awake()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }

            _sumLabel.text = "0";
            _sum = 0;
            _button.interactable = true;
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

        public void Roll()
        {
            AddDie(DieFaceHelper.Roll());
        }

        public void AddDie(DieFace dieFace)
        {
            Die die = Instantiate(_diePrefab, _content);

            die.SetColor(_faceColor);
            die.SetFace(dieFace);

            _sum += DieFaceHelper.GetNumericValue(dieFace);

            if (IsBust())
            {
                _button.interactable = false;
                _sumLabel.text = "BUST!";

                return;
            }

            _sumLabel.text = _sum.ToString();
        }

        public bool IsBust() => _sum > _bustThreshold;

        public int GetSum() => _sum;

        [ContextMenu("Add Die")]
        public void AddDieDebug()
        {
            AddDie(DieFace.Five);
        }
    }
}
