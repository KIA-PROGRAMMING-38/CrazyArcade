using UnityEngine;

public class Status : MonoBehaviour
{

    private int _selectedCharacterId = 0;  // 아직 캐릭터 선택 기능이 없어 정보를 참조할 곳이 명확하지 않으므로 우선 0(배찌)으로 지정

    // 선택 캐릭터 기본 status
    private float _speed;
    private float _maxSpeed;
    private int _power;
    private int _maxPower;
    private int _count;
    private int _maxCount;

    public bool SpeedDebuff { get; set; } = false;
    public bool MoveRestrict { get; set; } = false;
    private const float DEBUFF_SPEED = 0.2f;
    private const float RESTRICTED_SPEED = 0f;
    public float Speed
    {
        get
        {
            if(MoveRestrict == true)
            {
                return RESTRICTED_SPEED;
            }
            else if(SpeedDebuff == true)
            {
                return DEBUFF_SPEED;
            }
            else
            {
                return Mathf.Min(_speed + AdditionalSpeed, _maxSpeed);
            }
        }
    }
    public float AdditionalSpeed { get; set; }
    public float SavedSpeed { get; set; }

    public int Power { get { return Mathf.Min(_power + AdditionalPower, _maxPower); } }
    public int AdditionalPower { get; set; }

    public int Count { get { return Mathf.Min(_count + AdditionalCount, _maxCount); } }
    public int AdditionalCount { get; set; }

    private void Start()
    {
        // TODO: _selectedCharacterId를 참조해오기
        GetStatus(_selectedCharacterId);
    }

    private void GetStatus(int id)
    {
        _speed = DataReader.PlayableCharacters[id].speed / 2;
        _maxSpeed = DataReader.PlayableCharacters[id].maxSpeed / 2;
        _power = DataReader.PlayableCharacters[id].power;
        _maxPower = DataReader.PlayableCharacters[id].maxPower;
        _count = DataReader.PlayableCharacters[id].count;
        _maxCount = DataReader.PlayableCharacters[id].maxCount;
    }
}
