using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class EggsSpawner : MonoBehaviour
{
    [SerializeField] PhotonView pV;
    [SerializeField] int numberToSpawn;
    [SerializeField] GameObject[] eggs;
    [SerializeField] GameObject box;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        SpawnEggs();
    }

    public void SpawnEggs()
    {
        int randomEgg = 0;
        GameObject toSpawn;
        BoxCollider c = box.GetComponent<BoxCollider>();

        float screenZ, screenY, screenX;
        Vector3 pos;

        for(int i = 0; i < numberToSpawn; i++)
        {
            randomEgg = Random.Range(0, eggs.Length);
            toSpawn = eggs[randomEgg];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = c.bounds.center.y;
            screenZ = Random.Range(c.bounds.min.z, c.bounds.max.z);

            pos = new Vector3(screenX, screenY, screenZ);

            //PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "Egg"), pos, toSpawn.transform.rotation, 0);
        }
    }
}
