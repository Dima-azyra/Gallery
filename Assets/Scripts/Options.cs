using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] bool orientationPortrait;
    [SerializeField] bool orientationLandscape;
    [SerializeField] bool orientationAutoRotation;

    [Space(20)]
    [SerializeField] string back_button_scene;
    [SerializeField] bool show_load_progress;
    [SerializeField] float load_sec;

    private void Awake()
    {
       Application.targetFrameRate = 60;
       if (orientationPortrait) Screen.orientation = ScreenOrientation.Portrait;
       else if (orientationLandscape) Screen.orientation = ScreenOrientation.LandscapeLeft;
       else if (orientationAutoRotation) Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (back_button_scene != null)
                {
                    Open_scene.start_scene(back_button_scene, show_load_progress, load_sec);
                }
            }
        }
    }
}
