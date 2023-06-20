using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gallery : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] Transform image;
    [SerializeField] Transform content;
    int max_image;
    [SerializeField] Scrollbar scroll;
    public static List<string> uri_names = new List<string>();
    List<Transform> image_gallery = new List<Transform>();
    List<Transform> down = new List<Transform>();
    GridLayoutGroup contentGridLayout;
    float size_image;
    int start_count_image;
    int current_image_count;
    bool busy;

    private void Start()
    {
        uri_names = Resources.get_file_mames(Folder_gallery.current_uri);
        max_image = uri_names.Count;
        contentGridLayout = content.GetComponent<GridLayoutGroup>();
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        size_image = (Screen.width - contentGridLayout.padding.left - contentGridLayout.padding.right - contentGridLayout.spacing.x) / 2;
        start_count_image = (Screen.height - contentGridLayout.padding.top - contentGridLayout.padding.bottom) / (int)(size_image + contentGridLayout.spacing.y) * 2 + 2;
        contentGridLayout.cellSize = new Vector2(size_image, size_image);
        add_image(start_count_image);
        start_up();
    }

    void add_image(int count)
    {
        for (int i = 0; count > i; i++)
        {
           if(current_image_count < max_image)
            {

                image_gallery.Add(set_image(Instantiate(image, content), Folder_gallery.current_uri + uri_names[current_image_count]));
                current_image_count++;
                unparent();
                start_check(count);
            }
        }
    }

    Transform set_image(Transform image, string uri)
    {
        image.GetComponent<Image_gallery>().set_uri(uri);
        return image;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (scroll.value <= 0.5)
        {
          if(!busy) add_image(6);
        }
    }

    void unparent()
    {
        busy = true;
        foreach (Transform image in image_gallery)
        {
            image.parent = null;
        }
    }
    void set_parent()
    {

        foreach (Transform image in image_gallery)
        {
            image.parent = content;
        }
        image_gallery.Clear();
        busy = false;
    }
    IEnumerator check_busy_or_down(int count)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        bool check = true;

        foreach (Transform image in image_gallery)
        {
            var current_image = image.GetComponent<Image_gallery>();
            if (!current_image.finish && !current_image.down) check = false;
            if (current_image.down) down.Add(image);
        }
        if (check) set_parent();
        start_check(count);
    }

    void start_check(int count)
    {
        StartCoroutine(check_busy_or_down(count));
    }

    IEnumerator up()
    {
        yield return new WaitForSecondsRealtime(1);
        List<Transform> down = new List<Transform>(this.down);
        foreach (Transform image in down)
        {
            var current_image = image.GetComponent<Image_gallery>();
            if (current_image.down && !current_image.busy)
            {
                current_image.up();
                current_image.current_up++;
                print(this.down.Count);
            }
            else this.down.Remove(image);
        }
        start_up();
    }

    void start_up()
    {
        StartCoroutine(up());
    }
}
