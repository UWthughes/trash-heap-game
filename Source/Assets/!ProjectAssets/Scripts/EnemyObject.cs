using UnityEngine;
using System.Collections;

public class EnemyObject : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject thisEnemyInstance;
    public GameObject healthBar;
    public EnemyStats stats;
    private GameObject healthBarInstance;
    private int previousHp = 0;
    public int enemyType;
    private Vector3 startPos;
    public int count = 0;
    //public float hpPercent;

    // Use this for initialization
    void Start()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Component[] colliders = controller.GetComponents(typeof(Collider));

        foreach (Collider collider in colliders)
        {
            foreach (Collider existingCollider in existingCCColliders)
            {
                Physics.IgnoreCollision(collider, existingCollider);
            }
        }

        thisEnemyInstance = Instantiate(prefabs[enemyType], gameObject.transform.position, Quaternion.identity) as GameObject;
        thisEnemyInstance.transform.parent = gameObject.transform;
        startPos = gameObject.transform.position;
        stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
        stats.HealthPoints = 100;
        stats.InitialHealthPoints = 100;
 
        healthBarInstance = Instantiate(healthBar, transform.position, Quaternion.identity) as GameObject;

        float hpPercent = (float)(stats.HealthPoints / 100.0);//stats.InitialHealthPoints);
        float hpBarWidth = (hpPercent * 50);
        healthBarInstance.guiTexture.pixelInset = new Rect(-25, 25, hpBarWidth, 5);

        healthBarInstance.transform.localScale = Vector3.zero;

        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        //destroy if hp is zero
        if (stats.HealthPoints == 0)
        {
            Destroy(thisEnemyInstance);
        }

        //timer respawn
        if (Time.frameCount % 400 == 0 && count < 50)
        {
            GameObject newEnemyInstance = (GameObject)Instantiate(this, startPos, Quaternion.identity);

            //thisEnemyInstance = Instantiate(prefabs[enemyType],
            //                       gameObject.transform.position,
            //                       Quaternion.identity) as GameObject;
            newEnemyInstance.transform.parent = gameObject.transform;
            stats = newEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
            newEnemyInstance.AddComponent("BoxCollider");
            stats.HealthPoints = 100 + count;
            stats.InitialHealthPoints = 100 + count;
            count += 5;

        }

        GameObject player = GameObject.Find("Capsule");

        if (player != null && thisEnemyInstance != null)
        {
            //adjust the health bar based on the hp
            if (healthBarInstance != null)
            {
                healthBarInstance.transform.position = Camera.main.WorldToViewportPoint(thisEnemyInstance.transform.position);
                float hpPercent = (stats.HealthPoints / (float)stats.InitialHealthPoints);
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

            UnityEngine.CharacterController controller = GetComponent<UnityEngine.CharacterController>();

            //for different enemy tpyes
            switch (enemyType)
            {
                case 0:
                    //standard mob - dodges left or right randomly
                    if (Vector3.Distance(transform.position, player.transform.position) >= 5 && controller != null)
                    {
                        controller.SimpleMove((transform.forward * 10.0F) * Time.deltaTime);//pursue player
                        //transform.position += (transform.forward * 10.0F) * Time.deltaTime;//pursue player
                    }
                    else if (Time.frameCount % 5 == 0)//slow down fire rate of enemies
                    {
                        //create the projectiles the enemy fires
                        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
                        proj.position = transform.position + transform.forward;
                        proj.rotation = transform.rotation;
                        proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
                        proj.localScale = new Vector3(.2f, .2f, .2f);
                        proj.gameObject.AddComponent<EnemyBasicProjectile>();
                    }
                    if (previousHp > stats.HealthPoints)
                    {
                        var random = Random.value;
                        //move - you are getting shot
                        transform.position += (random > 0.5 ? transform.right * -200.0F : transform.right * 200.0F) * Time.deltaTime;
                        Debug.Log(random.ToString());

                    }
                    break;
                case 1:
                    //standard mob - pursues player and fires when within range
                    if (Vector3.Distance(transform.position, player.transform.position) >= 15 && controller != null)
                    {
                        controller.SimpleMove(transform.forward * 10.0F * Time.deltaTime);//pursue player
                        //transform.position += transform.forward * Time.deltaTime;//pursue player
                    }
                    else if (Time.frameCount % 15 == 0)//slow down fire rate of enemies
                    {
                        //create the projectiles the enemy fires
                        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
                        proj.position = transform.position + transform.forward;
                        proj.rotation = transform.rotation;
                        proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
                        proj.localScale = new Vector3(.2f, .2f, .2f);
                        proj.gameObject.AddComponent<EnemyBasicProjectile>();
                    }
                    break;
                case 2:
                    //standard mob - teleports around player when shot
                    if (Vector3.Distance(transform.position, player.transform.position) >= 5 && controller != null)
                    {
                        controller.SimpleMove((transform.forward * 10.5F) * Time.deltaTime);//pursue player
                        //transform.position += (transform.forward * 10.5F) * Time.deltaTime;//pursue player
                    }
                    else if (Time.frameCount % 35 == 0)//slow down fire rate of enemies
                    {
                        //create the projectiles the enemy fires
                        Transform proj = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<Transform>();
                        proj.position = transform.position + transform.forward;
                        proj.rotation = transform.rotation;
                        proj.Rotate(Vector3.up, (Random.value - .5f) * 7.5f);
                        proj.localScale = new Vector3(.2f, .2f, .2f);
                        proj.gameObject.AddComponent<EnemyBasicProjectile>();
                    }
                    else if (previousHp > stats.HealthPoints)
                    {
                        //move - you are getting shot
                        transform.position += transform.forward * 10.0F;

                    }
                    break;
            }
        }

        if (Time.frameCount % 550 == 0 && thisEnemyInstance == null)
        {
            //create a new enemy if this one is dead
            thisEnemyInstance = Instantiate(prefabs[enemyType],
                                   gameObject.transform.position,
                                   Quaternion.identity) as GameObject;
            thisEnemyInstance.transform.parent = gameObject.transform;
            stats = thisEnemyInstance.AddComponent("EnemyStats") as EnemyStats;
            thisEnemyInstance.AddComponent("BoxCollider");
            stats.HealthPoints = 100 + count;
            stats.InitialHealthPoints = 100 + count;
            count += 5;
        }

        //GameObject[] shots = GameObject.FindGameObjectsWithTag("PlayerShot");
        //foreach(GameObject shot in shots)
        //{
        //    RaycastHit hit;
        //    Physics.Linecast(shot.transform.position, transform.position, out hit);
        //    //Debug.Log(hit.collider.name);
        //    if(hit.collider.name == "Enemy2(Clone)");
        //    transform.position += transform.right * 10.0F;
        //        //Debug.Log("INC!!");
        //}



        previousHp = stats.HealthPoints;

        //proximity respawn
        //if (Vector3.Distance(startPos, player.transform.position) >= 1)
            //Instantiate(this, startPos, Quaternion.identity);

    }
}
