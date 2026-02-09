using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLogic : MonoBehaviour
{
    [SerializeField] Animator camAnim;
    Animator lightAnim;
    Light spot;
    // Start is called before the first frame update
    void Start()
    {
        spot = GetComponent<Light>();
        lightAnim = GetComponent<Animator>();
        Invoke("SwitchOn",7.8f);
        Invoke("LoadScene_",28.5f);
        StartCoroutine(Stop());
    }

    // Update is called once per frame
    void Update()
    {
        if(spot.enabled)
        {
            camAnim.enabled = true;
            lightAnim.enabled = true;
        }
    }

    void SwitchOn()
    {
        spot.enabled = true;
    }

    void LoadScene_()
    {
        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(26.8f);
        spot.enabled = false;
    }

}
