using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager_Script : MonoBehaviour
{
    public GameObject crystal;
    public int xPos;
    public int zpos;
    public int ypos = 145;
    public int crystalCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CrystalDrop());
    }

    // Update is called once per frame
    IEnumerator CrystalDrop()
    {
        while(crystalCount < 24)
        {
            xPos = Random.Range(425,850);
            zpos = Random.Range(390,880);
            Instantiate(crystal,new Vector3(xPos,ypos,zpos),Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
            crystalCount++;
        }
    }
}
