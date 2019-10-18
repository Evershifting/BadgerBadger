
using System;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Sprite _mute, _unmute;
    [SerializeField] private Image _soundIcon;

    private bool _isMute = false;

    public void Mute()
    {
        _soundIcon.sprite = _isMute ? _unmute : _mute;
        _isMute = !_isMute;
        AudioManager.Instance.Mute();
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ToggleMenu(bool value)
    {
        _menu.SetActive(value);
    }
}