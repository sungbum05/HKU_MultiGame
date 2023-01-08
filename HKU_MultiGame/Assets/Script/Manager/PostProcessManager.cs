using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager Instance;

    [SerializeField]
    private Color Red_Color = Color.white;

    [Header("PostProcessing")]
    [SerializeField]
    PostProcessVolume Volume;
    [SerializeField]
    Vignette Vignette;
    [SerializeField]
    float MinValue, MaxValue;
    [SerializeField]
    float Speed;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Volume.profile.TryGetSettings(out Vignette);

        StartCoroutine(CameraFade("Up"));
    }

    public void HpMax()
    {
        ColorParameter ColorParameter = new ColorParameter();
        ColorParameter.value = Color.black;

        Vignette.color = ColorParameter;
    }

    public void NoHp()
    {
        ColorParameter ColorParameter = new ColorParameter();
        ColorParameter.value = Red_Color;

        Vignette.color = ColorParameter;
    }

    IEnumerator CameraFade(string Type)
    {
        yield return null;

        switch (Type)
        {
            case "Up":
                while (MaxValue > Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value += Time.deltaTime * Speed;
                }

                StartCoroutine(CameraFade("Down"));
                break;
            case "Down":
                while (MinValue < Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value -= Time.deltaTime * Speed;
                }
                StartCoroutine(CameraFade("Up"));
                break;
        }

        yield break;
    }
}
