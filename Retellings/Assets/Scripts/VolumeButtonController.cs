using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Image _image;

    private int _counter = 0;
    #endregion

    #region Main
    private void Start()
    {
        _image = GetComponent<Image>();
    }
    public void SwitchSound()
    {
        _counter++;
        _image.sprite = _sprites[_counter % 3];
        if (_counter % 3 == 0)
        {
            _audioSource.volume = 1;
        }
        else if (_counter % 3 == 1)
        {
            _audioSource.volume = 0.5f;
        }
        else if (_counter % 3 == 2)
        {
            _audioSource.volume = 0.0f;
        }
    }
    #endregion
}
