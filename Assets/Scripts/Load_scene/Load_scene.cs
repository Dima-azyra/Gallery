using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_scene : MonoBehaviour
{
    AsyncOperation load_game;
    static float load_sec;
    [SerializeField] GameObject progress_bar;
    float time_offset;
    Slider progress_line;
    Text progress_percent;

    private void Awake()
    {
        progress_line = progress_bar.transform.GetChild(0).GetComponent<Slider>();
        time_offset = 1 / (load_sec * 100);
        progress_percent = progress_bar.transform.GetChild(1).GetComponent<Text>();
        progress_percent.text = 0 + "%";
        load_game = load(Open_scene.scene);
        StartCoroutine(update_progress());
    }

    AsyncOperation load(string scene_name)
    {
        AsyncOperation load_game = SceneManager.LoadSceneAsync(scene_name);
        load_game.allowSceneActivation = false;
        return load_game;
    }

    public void show_scene()
    {
        if(load_game != null) load_game.allowSceneActivation = true;
    }

    IEnumerator update_progress()
    {
        progress_line.value += time_offset*3.5f;
        progress_percent.text = (int)(progress_line.value * 100) + "%";
        if (progress_line.value >= 1) show_scene();
        yield return new WaitForSecondsRealtime(0.02f);
        repeat_progress();
    }

    void repeat_progress()
    {
        StartCoroutine(update_progress());
    }

    public static void set_load_sec(float sec)
    {
        load_sec = sec;
    }
}
