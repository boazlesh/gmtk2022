using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class CustomGauge : MonoBehaviour
    {
        public event Action OnCustomGaugeComplete;

        [SerializeField] private float _customGaugeDurationSeconds = 10f;
        [SerializeField] private Slider _slider;

        public void RunCustomGauge()
        {
            StartCoroutine(RunCustomGaugeRoutine(_customGaugeDurationSeconds));
        }

        private IEnumerator RunCustomGaugeRoutine(float timeSeconds)
        {
            float elapsedTime = 0f;

            while (elapsedTime < timeSeconds)
            {
                _slider.value = Mathf.Lerp(0, _slider.maxValue, (elapsedTime / timeSeconds));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            OnCustomGaugeComplete?.Invoke();
        }
    }
}
