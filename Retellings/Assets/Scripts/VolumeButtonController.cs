using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image image;

    int counter = 0;
    #endregion

    #region Main
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void SwitchSound()
    {
        counter++;
        image.sprite = sprites[counter % 3];
        if (counter % 3 == 0)
        {
            audioSource.volume = 1;
        }
        else if (counter % 3 == 1)
        {
            audioSource.volume = 0.5f;
        }
        else if (counter % 3 == 2)
        {
            audioSource.volume = 0.0f;
        }
    }
    #endregion
}
