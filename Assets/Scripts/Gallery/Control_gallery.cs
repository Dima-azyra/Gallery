using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_gallery : MonoBehaviour
{
    static List<GameObject> gallery = new List<GameObject>();

    private void Awake()
    {
        if (gallery.Count == 0)
        {
            gallery.Add(gameObject);
            DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().name.Equals("gallery")) gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
           Destroy(gameObject);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (gallery.Count > 0)
        {
            if (SceneManager.GetActiveScene().name.Equals("gallery"))
            {
                gallery[0].GetComponent<Canvas>().enabled = true;
            }
            else gallery[0].GetComponent<Canvas>().enabled = false;
        } 
    }
}
