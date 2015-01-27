using UnityEngine;
using System.Collections;

public class EnemyObject : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject thisEnemyInstance;
    public GameObject healthBar;
    EnemyStats stats;
    private GameObject healthBarInstance;

    // Use this for initialization
    void Start()
    {
        thisEnemyInstance = Instantiate(prefabs[Random.Range(0, prefabs.Length)],
                                           gameObject.transform.position,
                                           Quaternion.identity) as GameObject;
        thisEnemyInstance.transform.parent = gameObject.transform;
        stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
 
        healthBarInstance = Instantiate(healthBar, transform.position, Quaternion.identity) as GameObject;

        float hpPercent = (float)(stats.HealthPoints / 100.0);
        float hpBarWidth = (hpPercent * 50);
        healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

        if (stats.HealthPoints < 33)
            healthBarInstance.guiTexture.color = Color.red;
        else if (stats.HealthPoints < 66)
            healthBarInstance.guiTexture.color = Color.yellow;

        healthBarInstance.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //create a new enemy if the one is dead
        if (gameObject == null)
        {
            //thisEnemyInstance = Instantiate(prefabs[Random.Range(0, prefabs.Length)],
            //                       gameObject.transform.position,
            //                       Quaternion.identity) as GameObject;
            //thisEnemyInstance.transform.parent = gameObject.transform;
            //stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;

            //healthBarInstance = Instantiate(healthBar, transform.position, Quaternion.identity) as GameObject;

            //float hpPercent = (float)(stats.HealthPoints / 100.0);
            //float hpBarWidth = (hpPercent * 50);
            //healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

            //if (stats.HealthPoints < 33)
            //    healthBarInstance.guiTexture.color = Color.red;
            //else if (stats.HealthPoints < 66)
            //    healthBarInstance.guiTexture.color = Color.yellow;

            //healthBarInstance.transform.localScale = Vector3.zero;

            Debug.Log("yay");
        }
        //destroy if hp is zero
        if(stats.HealthPoints == 0)
        {
            Destroy(gameObject);
        }

        GameObject player = GameObject.Find("!Player");

        if (player != null)
        {
            transform.LookAt(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) >= 5)
                transform.position += transform.forward * Time.deltaTime;
            else if(Time.frameCount % 5 == 0)//slow down fire rate of enemies
            {
                //create the projectiles the enemy fires
                Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
                proj.position = transform.position + transform.forward;
                proj.rotation = transform.rotation;
                proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
                proj.localScale = new Vector3(.2f, .2f, .2f);
                proj.gameObject.AddComponent<EnemyBasicProjectile>();
            }
            //Debug.Log("yay");
        }

        //adjust the health bar based on the hp
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

    }
}
