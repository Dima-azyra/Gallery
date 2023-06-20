using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Open_scene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool show_load_progress;
    [SerializeField] string scene_name;
    [SerializeField] float load_sec;
    public static string scene;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!show_load_progress) load_scene_async(scene_name);
        else
        {
            Load_scene.set_load_sec(load_sec);
            scene = scene_name;
            load_scene_async("load_scene");
        }
    }

    public static void start_scene(string scene_name, bool show_load_progress, float load_sec)
    {
        if (!show_load_progress) load_scene_async(scene_name);
        else
        {
            Load_scene.set_load_sec(load_sec);
            scene = scene_name;
            load_scene_async("load_scene");
        }
    }

    static void load_scene_async(string scene_name)
    {
        AsyncOperation load_game = SceneManager.LoadSceneAsync(scene_name);
    }
}
