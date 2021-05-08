using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AddHeartbeat : MonoBehaviour
{
    private float currentHeartrate;

    private float scalePeriod;

    private float maxScaleX, maxScaleY, maxScaleZ;
    private float initialScaleX, initialScaleY, initialScaleZ;
    private float scaleIncreaseX, scaleIncreaseY, scaleIncreaseZ;

    //Audio Settings
    public AudioSource audioSource;
    public AudioClip heartBeatClip;
    private float beatPitch;

    //ECG Settings
    private float[] waveformArray;
    public Image ecgMonitorImage;
    private Texture2D thisECGImage;

    // Start is called before the first frame update
    void Start()
    {
        maxScaleX = 0.1f;
        maxScaleY = 0.0f;
        maxScaleZ = 0.1f;

        //Get Heart objects initial scale values
        initialScaleX = this.transform.localScale.x;
        initialScaleY = this.transform.localScale.y;
        initialScaleZ = this.transform.localScale.z;

        currentHeartrate = GetComponent<HeartDetails>().getCurrent();
        audioSource = GetComponent<AudioSource>();
        //thisWaveForm = new waveForms();
    }

    // Update is called once per frame
    void Update()
    {
        currentHeartrate = GetComponent<HeartDetails>().getCurrent();

        // Scale of 1 = 60 beats per second
        scalePeriod = currentHeartrate / 60.0f;
        // Convert Scale to Pitch (standard clip is 1 beat per minute)
        beatPitch = scalePeriod;
        audioSource.pitch = beatPitch;

        scaleIncreaseX = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleX;
        scaleIncreaseY = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleY;
        scaleIncreaseZ = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleZ;

        waveformArray = GenerateWaveFromAudio(heartBeatClip, 500, 1f);
        thisECGImage = CreateTextureFromWaveForm(waveformArray, 50, new Color32(51, 0, 0, 255));

        ecgMonitorImage.GetComponent<Image>().overrideSprite = Sprite.Create(thisECGImage, new Rect(0f, 0f, thisECGImage.width, thisECGImage.height), new Vector2(0.5f, 0.5f));

        /*
        if(scaleIncreaseX == 0)
        {
            audioSource.PlayOneShot(heartBeatClip, 1.0f);
        }
        */

        //Calculate Scale
        this.transform.localScale = new Vector3(initialScaleX + scaleIncreaseX,
                                                initialScaleY + scaleIncreaseY,
                                                initialScaleZ + scaleIncreaseZ);
    }

    public static float[] GenerateWaveFromAudio(AudioClip audio, int size, float saturation)
    {
        //This method takes an audio file and created a waveform image
        float[] samples = new float[audio.channels * audio.samples];
        float[] waveform = new float[size];
        audio.GetData(samples, 0);
        int packSize = audio.samples * audio.channels / size;
        float max = 0f;
        int c = 0;
        int s = 0;
        for (int i = 0; i < audio.channels * audio.samples; i++)
        {
            waveform[c] += Mathf.Abs(samples[i]);
            s++;
            if (s > packSize)
            {
                if (max < waveform[c])
                    max = waveform[c];
                c++;
                s = 0;
            }
        }
        for (int i = 0; i < size; i++)
        {
            waveform[i] /= (max * saturation);
            if (waveform[i] > 1f)
                waveform[i] = 1f;
        }

        return waveform;
    }

    public static Texture2D CreateTextureFromWaveForm(float[] waveform, int height, Color c)
    {
        Texture2D waveFormTexture = new Texture2D(waveform.Length, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < waveform.Length; x++)
        {
            for (int y = 0; y <= waveform[x] * (float)height / 2f; y++)
            {
                waveFormTexture.SetPixel(x, (height / 2) + y, c);
                waveFormTexture.SetPixel(x, (height / 2) - y, c);
            }
        }
        waveFormTexture.Apply();

        return waveFormTexture;
    }

}
