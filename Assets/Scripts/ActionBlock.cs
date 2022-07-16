using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBlock : MonoBehaviour
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private Color _color;
    [SerializeField] private Image _actionImage;
    [SerializeField] private Image _outlineImage;

    private void OnValidate()
    {
        _actionImage.sprite = _actionSprite;
        _outlineImage.color = _color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
