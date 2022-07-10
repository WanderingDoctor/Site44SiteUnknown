using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : SiteScript<OptionsMenu>
{
    #region InputHandling
    public InputActionAsset inputs;

    Player player;
    #endregion
    #region OptionsStructs
    [Serializable]
    public struct AudioProps
    {
        public AudioMixer mixer;
        public Slider Volume_Master, Volume_Effects, Volume_Music;
    }
    [Serializable]
    public struct GraphicalProps
    {
        public TMP_Dropdown FpsCap, ResolutionOptions, QualityOptions;
        public TMP_Text Fps;
        public Toggle VSyncToggle, FpsToggle, FullScreen;
        public Slider ParticleSlider;
    }
    [Serializable]public struct GameplayProps
    {
        public TMP_Text Timer;
    }
    #endregion
    #region TabHandling
    public ScrollRect TabHandler;

    public List<RectTransform> Tabs;
    #endregion
    #region GeneralSettings
    int[] FrameLimits = { -1, 15, 24, 30, 50, 60, 100, 120, 144, 240, 360 };
    Resolution[] resolutions;
    public AudioProps audiop;
    public GraphicalProps graphicsp;
    public GameplayProps gameplayp;
    #endregion
    #region ParticleHandling
    List<ParticleSystem> ps;
    #endregion

    public void Initialize()
    {
        gameplayp.Timer.gameObject.SetActive(Utils.GetIntKey("Timer", 0).ToBool());

        ps = FindObjectsOfType<ParticleSystem>(true).ToList();
        graphicsp.ParticleSlider.value = Utils.GetFloatKey("Particle_Limit", 1);
        ForceParticleLimit(graphicsp.ParticleSlider.value);

        graphicsp.Fps.gameObject.SetActive(Utils.GetIntKey("FPS_Counter", 0).ToBool());
        Application.targetFrameRate = FrameLimits[Utils.GetIntKey("FPS_Cap", 0)];

        player = Player.Instance;
        if (player)
        {
            inputs = player.Inputs.actions;
        }

        try
        {
            inputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Bindings"));
        }
        catch
        {
            PlayerPrefs.SetString("Bindings", inputs.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }
        audiop.mixer.SetFloat("MasterVolume", Utils.GetFloatKey("MasterVolume", 0));
        audiop.mixer.SetFloat("EffectsVolume", Utils.GetFloatKey("EffectsVolume", 0));
        audiop.mixer.SetFloat("MusicVolume", Utils.GetFloatKey("MusicVolume", 0));
    }

    void Start()
    {
        graphicsp.FullScreen.isOn = PlayerPrefs.GetInt("FullScreen").ToBool();
        graphicsp.FpsToggle.isOn = PlayerPrefs.GetInt("FPS_Counter").ToBool();
        graphicsp.VSyncToggle.isOn = PlayerPrefs.GetInt("VSync").ToBool();

        graphicsp.FpsCap.value = PlayerPrefs.GetInt("FPS_Cap");
        graphicsp.FpsCap.RefreshShownValue();

        graphicsp.QualityOptions.value = PlayerPrefs.GetInt("Quality");
        graphicsp.QualityOptions.RefreshShownValue();
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        int resolutionindex = 0;
        graphicsp.ResolutionOptions.ClearOptions();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height} {resolutions[i].refreshRate}Hz");
            if (resolutions[i].width == PlayerPrefs.GetInt("Resolution_Width") && resolutions[i].height == PlayerPrefs.GetInt("Resolution_Height"))
            {
                resolutionindex = i;
            }
        }
        graphicsp.ResolutionOptions.AddOptions(options);
        graphicsp.ResolutionOptions.value = resolutionindex;
        graphicsp.ResolutionOptions.RefreshShownValue();
        audiop.Volume_Master.value = PlayerPrefs.GetFloat("MasterVolume");
        audiop.Volume_Effects.value = PlayerPrefs.GetFloat("EffectsVolume");
        audiop.Volume_Music.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    void Update()
    {
    }

    public void ResetKeybinds()
    {
        if (!inputs)
        {
            return;
        }
        inputs.RemoveAllBindingOverrides();
        PlayerPrefs.SetString("Bindings", inputs.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }

    public void ResetKeybind(InputActionReference action)
    {
        action.action.RemoveAllBindingOverrides();
        PlayerPrefs.SetString("Bindings", action.asset.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution_Width", resolution.width);
        PlayerPrefs.SetInt("Resolution_Height", resolution.height);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        audiop.mixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetEffectsVolume(float volume)
    {
        audiop.mixer.SetFloat("EffectsVolume", volume);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float volume)
    {
        audiop.mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
        PlayerPrefs.Save();
    }

    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("FullScreen", value.ToInt());
        PlayerPrefs.Save();
    }

    public void SetFPSCounter(bool value)
    {
        graphicsp.Fps.gameObject.SetActive(value);
        PlayerPrefs.SetInt("FPS_Counter", value.ToInt());
        PlayerPrefs.Save();
    }

    public void SetVSync(bool value)
    {
        QualitySettings.vSyncCount = value.ToInt();
        PlayerPrefs.SetInt("VSync", value.ToInt());
        PlayerPrefs.Save();
    }

    public void SetFPSCap(int value)
    {
        Application.targetFrameRate = FrameLimits[value];
        PlayerPrefs.SetInt("FPS_Cap", value);
        PlayerPrefs.Save();
    }

    public void SetParticleLimit(float value)
    {
        ForceParticleLimit(value);
        PlayerPrefs.SetFloat("Particle_Limit",value);
        PlayerPrefs.Save();
    }

    void ForceParticleLimit(float value)
    {
        ps.ForEach((_ps) =>
        {
            var emission = _ps.emission;
            emission.rateOverTimeMultiplier = value;
        });
    }

    public void SetTimer(bool value)
    {
        gameplayp.Timer.gameObject.SetActive(value);
        PlayerPrefs.SetInt("Timer", value.ToInt());
        PlayerPrefs.Save();
    }

    public void SwitchTab(int tab)
    {
        TabHandler.content.gameObject.SetActive(false);
        TabHandler.content = Tabs[tab];
        Tabs[tab].gameObject.SetActive(true);
    }
}