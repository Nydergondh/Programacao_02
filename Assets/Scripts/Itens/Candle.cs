using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, IDamageable
{
    public GameObject[] itens;
    public LayerMask simonLayer;
    private new BoxCollider2D collider;
    private Animator candleAnim;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        candleAnim = GetComponent<Animator>();
    }
    

    void ThrowItem() {
        int random = Random.Range(0, 13);
        switch (random) {
            //add all control of animations
            case 0:
                GameObject knife = Instantiate(itens[0], transform.position, Quaternion.identity);
                break;
            case 1:
                GameObject holyWater = Instantiate(itens[1], transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject cross = Instantiate(itens[2], transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject axe = Instantiate(itens[3], transform.position, Quaternion.identity);
                break;
            case 4:
                GameObject whip = Instantiate(itens[4], transform.position, Quaternion.identity);
                break;
            case 5:
                GameObject hearth = Instantiate(itens[5], transform.position, Quaternion.identity);
                break;
            case 6:
                GameObject bigHearth = Instantiate(itens[6], transform.position, Quaternion.identity);
                break;
            case 7:
                GameObject whiteBag = Instantiate(itens[7], transform.position, Quaternion.identity);
                break;
            case 8:
                GameObject redBag = Instantiate(itens[8], transform.position, Quaternion.identity);
                break;
            case 9:
                GameObject purpleBag = Instantiate(itens[9], transform.position, Quaternion.identity);
                break;
            case 10:
                GameObject bigAssMoney = Instantiate(itens[10], transform.position, Quaternion.identity);
                break;
            case 11:
                GameObject coxinhaGalinha = Instantiate(itens[11], transform.position, Quaternion.identity);
                break;
            case 12:
                GameObject fYA = Instantiate(itens[12], transform.position, Quaternion.identity);
                break;
            case 13:
                GameObject zawardo = Instantiate(itens[13], transform.position, Quaternion.identity);
                break;

        }
        print(random);
    }

    public void OnDamage(int damage, GameObject gameObject) {
        ThrowItem();
        candleAnim.SetBool("Destroy",true);
    }

    public void EndMe() {
        Destroy(gameObject);
    }

}
