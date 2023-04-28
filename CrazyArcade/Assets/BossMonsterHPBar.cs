using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHPBar : MonoBehaviour
{
    [SerializeField] private Image _progressbar;
    [SerializeField] private Sprite _progressbarRed;
    [SerializeField] private Collider2D _boss;

    private void Update()
    {
        Vector3 pos = _boss.bounds.center;
        pos.y = _boss.bounds.max.y;

        transform.position = pos + Vector3.up;
    }

    public void UpdateHPBar(float ratio)
    {
        if(ratio <= 0.3f)
        {
            _progressbar.sprite = _progressbarRed;
        }

        _progressbar.fillAmount = ratio;
    }
}
