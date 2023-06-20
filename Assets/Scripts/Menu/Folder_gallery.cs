using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Folder_gallery : MonoBehaviour, IPointerClickHandler
{
    public static string current_uri;
    [SerializeField] string uri;
    public void OnPointerClick(PointerEventData eventData)
    {
        current_uri = uri;
    }
}
