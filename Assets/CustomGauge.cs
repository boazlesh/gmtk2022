using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class CustomGauge : MonoBehaviour
    {
        public event Action OnCustomGaugeComplete;

        [SerializeField] private float _customGaugeDurationSeconds = 10f;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _label;

        private void Awake()
        {
            _slider.value = 0;
            _label.enabled = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void RunCustomGauge()
        {
            StartCoroutine(RunCustomGaugeRoutine(_customGaugeDurationSeconds));
        }

        private IEnumerator RunCustomGaugeRoutine(float timeSeconds)
        {
            _label.enabled = false;
            float elapsedTime = 0f;

            while (elapsedTime < timeSeconds)
            {
                _slider.value = Mathf.Lerp(0, _slider.maxValue, (elapsedTime / timeSeconds));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _slider.value = _slider.maxValue;
            _label.enabled = enabled;
            OnCustomGaugeComplete?.Invoke();
        }
    }
}
