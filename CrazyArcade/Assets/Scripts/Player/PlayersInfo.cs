using System.Collections.Generic;

public static class PlayersInfo
{
    public struct PlayerInfo
    {
        public RESULT result;
        public string name;
        public string selectedCharacterId;

        public PlayerInfo(RESULT _result, string _name, string _characterId)
        {
            result = _result;
            name = _name;
            selectedCharacterId = _characterId;
        }
    }

    public static PlayerInfo Player1Info = new PlayerInfo();
    public static PlayerInfo Player2Info = new PlayerInfo();
    public static List<PlayerInfo> AllPlayersInfo = new List<PlayerInfo>();
    // public static PlayerInfo[] AllPlayersInfo = new PlayerInfo[2] { Player1Info, Player2Info };

    /// <summary>
    /// 어떤 플레이어의 정보를 수정할 것인지 지정하여 저장
    /// </summary>
    /// <param name="playerResult"></param>
    /// <param name="playerName"></param>
    /// <param name="player"></param>
    public static void SavePlayerInfo(RESULT playerResult, string playerName, string cid, out PlayerInfo player)
    {
        player.result = playerResult;
        player.name = playerName;
        player.selectedCharacterId = cid;

        AllPlayersInfo.Clear();
        AllPlayersInfo.Add(Player1Info);
        AllPlayersInfo.Add(Player2Info);
    }

    /// <summary>
    /// 두 플레이어의 승패 결과를 저장
    /// </summary>
    /// <param name="player1Result"></param>
    /// <param name="player2Result"></param>
    public static void SavePlayersResult(RESULT player1Result, RESULT player2Result)
    {
        Player1Info.result = player1Result;
        Player2Info.result = player2Result;

        AllPlayersInfo.Clear();
        AllPlayersInfo.Add(Player1Info);
        AllPlayersInfo.Add(Player2Info);
    }

    /// <summary>
    /// 두 플레이어의 이름을 저장
    /// </summary>
    /// <param name="player1Name"></param>
    /// <param name="player2Name"></param>
    public static void SavePlayersName(string player1Name, string player2Name)
    {
        Player1Info.name = player1Name;
        Player2Info.name = player2Name;

        AllPlayersInfo.Clear();
        AllPlayersInfo.Add(Player1Info);
        AllPlayersInfo.Add(Player2Info);
    }

    /// <summary>
    /// 두 플레이어의 캐릭터를 저장
    /// </summary>
    public static void SavePlayerCharacter(string player1CID, string player2CID)
    {
        Player1Info.selectedCharacterId = player1CID;
        Player2Info.selectedCharacterId = player2CID;

        AllPlayersInfo.Clear();
        AllPlayersInfo.Add(Player1Info);
        AllPlayersInfo.Add(Player2Info);
    }
}
