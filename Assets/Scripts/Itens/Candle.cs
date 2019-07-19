using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, IDamageable
{
    public GameObject[] itens;
    public LayerMask simonLayer;
    private new BoxCollider2D collider;
    private Animator candleAnim;
    public bool garantiedDrop;
    public int dropID;
    private int countSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        candleAnim = GetComponent<Animator>();
    }

    void ThrowItem() {
        int random = Random.Range(0, 12);
        if (!garantiedDrop) {
            switch (random) {
                //add all control of animations

                case 0:
                    GameObject knife = Instantiate(itens[0], transform.position, Quaternion.identity);
                    Consumables consKni = knife.GetComponent<Consumables>();
                    consKni.idConsumable = 0;
                    break;
                case 1:
                    GameObject holyWater = Instantiate(itens[1], transform.position, Quaternion.identity);
                    Consumables consHoly = holyWater.GetComponent<Consumables>();
                    consHoly.idConsumable = 1;
                    break;
                case 2:
                    GameObject cross = Instantiate(itens[2], transform.position, Quaternion.identity);
                    Consumables consCros = cross.GetComponent<Consumables>();
                    consCros.idConsumable = 2;
                    break;
                case 3:
                    GameObject axe = Instantiate(itens[3], transform.position, Quaternion.identity);
                    Consumables consAxe = axe.GetComponent<Consumables>();
                    consAxe.idConsumable = 3;
                    break;
                case 4:
                    GameObject hearthAss = Instantiate(itens[5], transform.position, Quaternion.identity);
                    Consumables consHearthAss = hearthAss.GetComponent<Consumables>();
                    consHearthAss.idConsumable = 5;
                    break;
                    
                case 5:
                    GameObject hearth = Instantiate(itens[5], transform.position, Quaternion.identity);
                    Consumables consHearth = hearth.GetComponent<Consumables>();
                    consHearth.idConsumable = 5;
                    break;
                case 6:
                    GameObject bigHearth = Instantiate(itens[6], transform.position, Quaternion.identity);
                    Consumables consBHearth = bigHearth.GetComponent<Consumables>();
                    consBHearth.idConsumable = 6;
                    break;
                case 7:
                    GameObject whiteBag = Instantiate(itens[7], transform.position, Quaternion.identity);
                    Consumables consWBag = whiteBag.GetComponent<Consumables>();
                    consWBag.idConsumable = 7;
                    break;
                case 8:
                    GameObject redBag = Instantiate(itens[8], transform.position, Quaternion.identity);
                    Consumables consRBag = redBag.GetComponent<Consumables>();
                    consRBag.idConsumable = 8;
                    break;
                case 9:
                    GameObject purpleBag = Instantiate(itens[9], transform.position, Quaternion.identity);
                    Consumables consPBag = purpleBag.GetComponent<Consumables>();
                    consPBag.idConsumable = 9;
                    break;
                case 10:
                    GameObject bigAssMoney = Instantiate(itens[10], transform.position, Quaternion.identity);
                    Consumables consBAM = bigAssMoney.GetComponent<Consumables>();
                    consBAM.idConsumable = 10;
                    break;
                case 11:
                    GameObject coxinhaGalinha = Instantiate(itens[11], transform.position, Quaternion.identity);
                    Consumables consCGal = coxinhaGalinha.GetComponent<Consumables>();
                    consCGal.idConsumable = 11;
                    break;
                case 12:
                    GameObject fYA = Instantiate(itens[12], transform.position, Quaternion.identity);
                    Consumables consfYA = fYA.GetComponent<Consumables>();
                    consfYA.idConsumable = 12;
                    break;
            }
        }
        else {
            if (garantiedDrop == true && dropID == -1) {

            }
            else {
                GameObject obj = Instantiate(itens[dropID], transform.position, Quaternion.identity);
                Consumables cons = obj.GetComponent<Consumables>();
                cons.idConsumable = dropID;
            }
        }
        
    }

    public void OnDamage(int damage, GameObject gameObject) {

        ThrowItem();
        
        candleAnim.SetBool("Destroy",true);
    }
    //called has a event in an animation
    public void EndMe() {
        Destroy(gameObject);
    }

}
