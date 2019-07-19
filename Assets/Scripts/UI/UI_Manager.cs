using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager ui_Manager;

    public float points = 0f;
    public float hearts = 0f;
    public float lives = 0f;
    public float time = 0f;
    public float totalWidth = 126f;
    public float totalHeight = 21f;
    public float currentWidth;
    public float currentHigth;
    // Health Bar Size = 7,90625‬

    public Text point_data;
    public Text hearts_data;
    public Text lives_data;
    public Text time_data;
    public Image subweapon_data;
    public Image health;
    public Image enemyHealth;

    // Start is called before the first frame update
    void Awake()
    {
        if (ui_Manager == null) {
            ui_Manager = this;
        }
        else {
            Destroy(ui_Manager);
            ui_Manager = this;
        }
        currentWidth = totalWidth;
        currentHigth = totalHeight;
    }

    // Update is called once per frame
    void Update()
    {
        CheckUI();
        if (points >= 0 && points <= 9) { point_data.text = "00000" + points.ToString(); }
        if (points >= 10 && points <= 99) { point_data.text = "0000" + points.ToString(); }
        if (points >= 100 && points <= 999) { point_data.text = "000" + points.ToString(); }
        if (points >= 1000 && points <= 9999) { point_data.text = "00" + points.ToString(); }
        if (points >= 10000 && points <= 99999) { point_data.text = "0" + points.ToString(); }
        if (points >= 100000) { point_data.text = points.ToString(); }

        if (time >= 0 && time <= 9) { time_data.text = "00" + time.ToString(); }
        if (time >= 10 && time <= 99) { time_data.text = "0" + time.ToString(); }
        if (time >= 100) { time_data.text = time.ToString(); }

        if (lives >= 0 && lives <= 9) { lives_data.text = "0" + lives.ToString(); }
        if (lives >= 10 && lives <= 99) { lives_data.text = lives.ToString(); }

        if (hearts >= 0 && hearts <= 9) { hearts_data.text = "0" + hearts.ToString(); }
        if (hearts >= 10 && hearts <= 99) { hearts_data.text = hearts.ToString(); }

    }

    void CheckUI() {
        hearts_data.text = hearts.ToString();
        point_data.text = points.ToString();
        health.rectTransform.sizeDelta =
        new Vector2(currentWidth, currentHigth);
    }

}
