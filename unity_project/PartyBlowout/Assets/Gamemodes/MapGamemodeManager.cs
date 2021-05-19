using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamemodes
{
    public class MapGamemodeManager : MonoBehaviour
    {
        public List<int> idsMapRace;
        public List<int> idsMapShooter;
        public List<int> idsMapGuessWho;

        public void LoadMap(Gamemode.CurrentGamemode gamemode)
        {
            List<int> listMaps;
            switch (gamemode)
            {
                case Gamemode.CurrentGamemode.GuessWho:
                    listMaps = idsMapGuessWho;
                    break;
                case Gamemode.CurrentGamemode.Race:
                    listMaps = idsMapRace;
                    break;
                case Gamemode.CurrentGamemode.Shooter:
                    listMaps = idsMapShooter;
                    break;
                default:
                    listMaps = new List<int>();
                    break;
            }

            int idMap = listMaps[Random.Range(0, listMaps.Count)];
            // RoomController.LoadScene(idMap);
            if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel(idMap);
        }
    }
}