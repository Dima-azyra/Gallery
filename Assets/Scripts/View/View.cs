using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    float image_x;
    float image_y;
    Image image;
    float scale_i;
    float scale_s;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = Image_gallery.open_image;
        image_x = image.sprite.bounds.size.x;
        image_y = image.sprite.bounds.size.y;

        scale_i = image_x / image_y;
        scale_s = Screen.width / Screen.height;
        start_orientation();
    }

    IEnumerator orientation()
    {


        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            if (scale_i > scale_s)
            {
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width / scale_i);
            }
            else
            {
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * scale_i, Screen.height);
            }
        }
        else
        {
            if (scale_i > scale_s)
            {
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height, Screen.height / scale_i);
            }
            else
            {
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * scale_i, Screen.width);
            }
        }
        yield return new WaitForSecondsRealtime(0.5f);

        start_orientation();
    }

    void start_orientation()
    {
        StartCoroutine(orientation());
    }
}
