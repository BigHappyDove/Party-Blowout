using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class CarManager : MonoBehaviour
{
    private PhotonView PV;

    [NonSerialized] public List<SimpleCarController> listCars;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        listCars = new List<SimpleCarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            InstantiateCar();
        }
    }

    void InstantiateCar()
    {
        GameObject g = null;
        Random rand = new Random();
        int vehiclechoice = rand.Next(4);
        
        switch (vehiclechoice)
        {
            case 0:
                g = PhotonNetwork.Instantiate(Path.Combine("Entities", "MotoCerise"), Vector3.zero, Quaternion.identity);
                break;
            case 1:
                g = PhotonNetwork.Instantiate(Path.Combine("Entities", "AvocatTuning"), Vector3.zero, Quaternion.identity);
                break;
            case 2:
                g = PhotonNetwork.Instantiate(Path.Combine("Entities", "FormulaBanane"), Vector3.zero, Quaternion.identity);
                break;
            default:
                g = PhotonNetwork.Instantiate(Path.Combine("Entities", "ScooterMelon"), Vector3.zero, Quaternion.identity);
                break;
        }

        if (g != null)
        {
            listCars.Add(g.GetComponent<SimpleCarController>());
        }
        
    }
    
    //Regarder le systeme de spawn de titouan pour modif les strings de pathspour varier les voiture.
}
