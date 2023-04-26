using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private int _currentIndex;
    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }

            return;
        }

        Instance = this;

        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        Instance.PlayBGM("lobby_bgm");
    }

    public void PlayBGM(string p_bgmName)
    {
        for(int i = 0; i < bgm.Length; ++i)
        {
            if(p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; ++i)
        {
            if(p_sfxName == sfx[i].name)
            {
                for(int j = 0; j < sfxPlayer.Length; ++j)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        _currentIndex = j;
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }

        Debug.Log(p_sfxName + "이름의 효과음이 없습니다.");
        return;
    }

    public void Stop()
    {
        sfxPlayer[_currentIndex].Stop();
    }
}
