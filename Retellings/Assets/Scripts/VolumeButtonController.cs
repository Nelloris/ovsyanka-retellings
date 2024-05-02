using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private AudioSource[] _audioSource;
    [SerializeField] private Image[] _image;

    private int _counter = 0;
    #endregion

    #region Main
    public void SwitchSound()
    {
        _counter++;
        for (int i = 0; i < _image.Length; i++) 
        {
            _image[i].sprite = _sprites[_counter % 3];
            if (_counter % 3 == 0)
            {
                _audioSource[i].volume = 1;
            }
            else if (_counter % 3 == 1)
            {
                _audioSource[i].volume = 0.66f;
            }
            else if (_counter % 3 == 2)
            {
                _audioSource[i].volume = 0.33f;
            }
        }
    }
    #endregion
}
