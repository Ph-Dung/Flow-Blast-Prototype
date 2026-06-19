using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SoundManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    instance = go.AddComponent<SoundManager>();
                }
            }
            // Đảm bảo sfxSource tồn tại kể cả khi tạo qua code (Awake chưa chạy kịp)
            if (instance.sfxSource == null)
                instance.sfxSource = instance.gameObject.AddComponent<AudioSource>();
            return instance;
        }
    }

    [Header("Audio Clips")]
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip suckSound;

    private AudioSource sfxSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        LoadClipsFromResources();
    }

    public void LoadClipsFromResources()
    {
        // BUG FIX: các file nằm thẳng trong Resources/ (không có subfolder Audio/)
        if (clickSound == null) clickSound = Resources.Load<AudioClip>("click");
        if (winSound == null) winSound = Resources.Load<AudioClip>("win");
        if (loseSound == null) loseSound = Resources.Load<AudioClip>("defeat");
        if (suckSound == null) suckSound = Resources.Load<AudioClip>("addpixel");
    }

    public void PlayClick()
    {
        // Đảm bảo field được load trước khi truyền vào (tránh bug by-value parameter)
        if (clickSound == null) LoadClipsFromResources();
        PlayClipWithPitch(clickSound, 1f, 0.05f);
    }

    public void PlayWin()
    {
        if (winSound == null) LoadClipsFromResources();
        PlayClipWithPitch(winSound, 1f, 0f);
    }

    public void PlayLose()
    {
        if (loseSound == null) LoadClipsFromResources();
        PlayClipWithPitch(loseSound, 1f, 0f);
    }

    public void PlaySuck()
    {
        if (suckSound == null) LoadClipsFromResources();
        PlayClipWithPitch(suckSound, 0.8f, 0.1f);
    }

    private void PlayClipWithPitch(AudioClip clip, float volume = 1f, float pitchRange = 0f)
    {
        if (sfxSource == null)
        {
            Debug.LogWarning("SoundManager: sfxSource is missing!");
            return;
        }

        if (clip == null)
        {
            Debug.LogWarning("SoundManager: AudioClip is null. Kiểm tra tên file trong Resources/");
            return;
        }

        sfxSource.pitch = pitchRange > 0f
            ? Random.Range(1f - pitchRange, 1f + pitchRange)
            : 1f;

        sfxSource.PlayOneShot(clip, volume);
    }
}
