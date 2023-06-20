using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class Image_gallery : MonoBehaviour, IPointerClickHandler
{
    UnityWebRequest www;
    Texture2D myTexture2D;

    [HideInInspector] public bool finish;
    [HideInInspector] public bool down;
    [HideInInspector] public bool busy;
    [HideInInspector] public string uri;
    [HideInInspector] public int max_up = 10;
    [HideInInspector] public int current_up;
    public static Sprite open_image;

    public void set_uri(string uri)
    {
        this.uri = uri;
        StartCoroutine(request(this.uri));
    }
    public void up()
    {
        StartCoroutine(request(uri));
    }

    IEnumerator request(string uri)
    {
        busy = true;
        www = UnityWebRequestTexture.GetTexture(uri);
        yield return www.SendWebRequest();
        try
        {
            if (www.isDone)
            {
                myTexture2D = DownloadHandlerTexture.GetContent(www);
                get_texture();
            }
        }
        catch
        {
            down = true;
            busy = false;
        }
    }

    void get_texture()
    {
        try
        {
            GetComponent<Image>().sprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f));
            finish = true;
            down = false;
            busy = false;
        }
        catch
        {
            down = true;
            busy = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        open_image = GetComponent<Image>().sprite;
    }
}
