using System;
using UnityEngine;
using UnityEngine.UI;

namespace All.Views
{
    public class CountDownView : MonoBehaviour
    {
        [SerializeField] private Text _countText;
        [SerializeField] private Image _counterImage;

        public void SetCount(int value)
        {
            _countText.text = (value).ToString();
        }

        public void StopCounter()
        {
            _countText.gameObject.SetActive(false);
            _counterImage.gameObject.SetActive(false);
        }
    }
}
