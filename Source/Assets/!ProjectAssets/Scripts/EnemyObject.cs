using UnityEngine;
using System.Collections;

public class EnemyObject : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject thisEnemyInstance;
    public GameObject healthBar;
    EnemyStats stats;
    private GameObject healthBarInstance;
    private int previousHp = 0;
    public int enemyType;
    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        thisEnemyInstance = Instantiate(prefabs[enemyType], gameObject.transform.position, Quaternion.identity) as GameObject;
        thisEnemyInstance.transform.parent = gameObject.transform;
        startPos = gameObject.transform.position;
        stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
 
        healthBarInstance = Instantiate(healthBar, transform.position, Quaternion.identity) as GameObject;

        float hpPercent = (float)(stats.HealthPoints / 100.0);
        float hpBarWidth = (hpPercent * 50);
        healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

        healthBarInstance.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (thisEnemyInstance == null)
        {
            //create a new enemy if this one is dead
            thisEnemyInstance = Instantiate(prefabs[enemyType],
                                   gameObject.transform.position,
                                   Quaternion.identity) as GameObject;
            thisEnemyInstance.transform.parent = gameObject.transform;
            stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
            thisEnemyInstance.AddComponent("BoxCollider");
            stats.HealthPoints = 100;
        }

        //destroy if hp is zero
        if (stats.HealthPoints == 0)
        {
            Destroy(thisEnemyInstance);
        }

        if(Time.frameCount % 400 == 0 && enemyType == 0)
        {
            //try to create a new one every 100 frames
            Instantiate(this, startPos, Quaternion.identity);
        }

        GameObject player = GameObject.Find("Capsule");

        if (player != null && thisEnemyInstance != null)
        {
            //adjust the health bar based on the hp
            if (healthBarInstance != null)
            {
                healthBarInstance.transform.position = Camera.main.WorldToViewportPoint(thisEnemyInstance.transform.position);
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

            transform.LookAt(player.transform.position);

            //for different enemy tpyes

            if (Vector3.Distance(transform.position, player.transform.position) >= 5)
               transform.position += transform.forward * Time.deltaTime;//pursue player
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

            //if(previousHp > stats.HealthPoints)
            //{
            //    //move you are getting shot
            //    transform.position += transform.right * Time.deltaTime;
            //    transform.position += transform.right * Time.deltaTime;
            //    transform.position += transform.right * Time.deltaTime;
            //    transform.position += transform.right * Time.deltaTime;
            //}
            //Debug.Log("yay");
        }

        GameObject[] shots = GameObject.FindGameObjectsWithTag("PlayerShot");
        previousHp = stats.HealthPoints;

    }

    private void Instantiate(EnemyObject enemyObject, GameObject gameObject, Vector3 startPos, Quaternion quaternion)
    {
        throw new System.NotImplementedException();
    }

    public void spawnEnemy()
    {
        thisEnemyInstance = Instantiate(prefabs[Random.Range(0, prefabs.Length)],
                                           gameObject.transform.position,
                                           Quaternion.identity) as GameObject;
        thisEnemyInstance.transform.parent = gameObject.transform;
        stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
    }
}
