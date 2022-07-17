using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionBlock : MonoBehaviour
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private FaceColor _faceColor;
    [SerializeField] private Image _actionImage;
    [SerializeField] private Image _outlineImage;
    [SerializeField] private FaceColorMapping _faceColorMapping;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AudioClip _powerSelectedClip;
    [SerializeField] private float _coolTime;

    private ActionModel _actionModel;

    private void OnValidate()
    {
        SetFaceColor(_faceColor);

        var actionModel = ScriptableObject.CreateInstance<ActionModel>();
        actionModel._actionType = ActionType.Cannon;
        SetAction(actionModel);
    }

    public IEnumerator SetButCool(FaceColor color, ActionModel actionModel, int pitchIndex)
    {
        SetFaceColor(color);
        SetAction(actionModel);
        SetText(null);

        AudioClipOneShotPlayer.SpawnOneShot(_powerSelectedClip, 1.0f + (pitchIndex / 10.0f));

        yield return new WaitForSeconds(_coolTime);
    }

    public void SetFaceColor(FaceColor faceColor, bool mute = false)
    {
        Color color = _faceColorMapping.GetColorFromFaceColor(faceColor, mute);

        _actionImage.color = color;
        _outlineImage.color = color;
        _text.color = color;
    }

    public void SetAction(ActionModel actionModel)
    {
        _actionModel = actionModel;

        _actionImage.sprite = actionModel._sprite;
    }

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            _text.gameObject.SetActive(false);

            return;
        }

        _text.text = text;
        _text.gameObject.SetActive(true);
    }

    public ActionModel GetAction() => _actionModel;
}
