using UnityEngine;
using System.Collections;

public class EnemyObject : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject enemyInstance;
    public GameObject healthBar;
    public EnemyStats stats;
    private GameObject healthBarInstance;
    int count;

    // Use this for initialization
    void Start()
    {
        GameObject o = Instantiate(prefabs[Random.Range(0, prefabs.Length)],
                                           enemyInstance.transform.position,
                                           Quaternion.identity) as GameObject;
        o.transform.parent = enemyInstance.transform;

        healthBarInstance = Instantiate(healthBar, transform.position, Quaternion.identity) as GameObject;

        float hpPercent = (float)(stats.HealthPoints / 100.0);
        float hpBarWidth = (hpPercent * 50);
        healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

        if (stats.HealthPoints < 33)
            healthBarInstance.guiTexture.color = Color.red;
        else if (stats.HealthPoints < 66)
            healthBarInstance.guiTexture.color = Color.yellow;

        healthBarInstance.transform.localScale = Vector3.zero;

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (stats.Agility < 25)
            transform.Translate((count > 500) ? Vector3.left * Time.deltaTime : Vector3.right * Time.deltaTime);
        else if (stats.Agility < 50)
            transform.Translate((count > 500) ? Vector3.forward * Time.deltaTime : Vector3.back * Time.deltaTime);
        else if (stats.Agility < 75)
            transform.Translate((count > 500) ? Vector3.back * Time.deltaTime : Vector3.forward * Time.deltaTime);
        else
            transform.Translate((count > 500) ? Vector3.right * Time.deltaTime : Vector3.left * Time.deltaTime);

        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = Camera.main.WorldToViewportPoint(transform.position);
            float hpPercent = (float)(stats.HealthPoints / 100.0);
            float hpBarWidth = (hpPercent * 50);
            healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

            if (stats.HealthPoints < 33)
                healthBarInstance.guiTexture.color = Color.red;
            else if (stats.HealthPoints < 66)
                healthBarInstance.guiTexture.color = Color.yellow;
            else
                healthBarInstance.guiTexture.color = Color.green;
        }

        count++;
        if (count > 1000)
            count = 0;
    }
}
