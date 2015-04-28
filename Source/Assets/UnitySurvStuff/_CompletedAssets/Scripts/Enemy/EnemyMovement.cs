using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        GameObject[] players;             // all players in the game
		Transform target;				  // the closest player
		float targetDistance;
		int moveSpeed = 3;
		int rotationSpeed = 3;
		float range1 = 10f;
		float range2 = 10f;
		float stop = 0f;
        //PlayerHealth playerHealth;      // Reference to the player's health.
        //EnemyHealth enemyHealth;        // Reference to this enemy's health.


        void Awake ()
        {
            // Set up the references.
            players = GameObject.FindGameObjectsWithTag("Player");
			InvokeRepeating("AcquireTarget", 0, 3);
            // playerHealth = player.GetComponent <PlayerHealth> ();
            //enemyHealth = GetComponent <EnemyHealth> ();
        }


        void Update ()
        {
			if( target != null ) {
				float distance = Vector3.Distance(transform.position, target.position);
				// rotate to look at the player
				Vector3 tempVec = new Vector3(target.position.x, transform.position.y, target.position.z);
				if( distance <= range2 && distance >= range1 ) {
					transform.rotation = Quaternion.Slerp(transform.rotation,
					                                      Quaternion.LookRotation(tempVec - transform.position), rotationSpeed * Time.deltaTime);
				}
				// move towards the player
				else if( distance <= range1 && distance > stop ) {
					transform.rotation = Quaternion.Slerp(transform.rotation,
					                                      Quaternion.LookRotation(tempVec - transform.position), rotationSpeed * Time.deltaTime);
					                                      //Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
					transform.position += transform.forward * moveSpeed * Time.deltaTime;
				}
				// just turn to player
				else if( distance <= stop ) {
					transform.rotation = Quaternion.Slerp(transform.rotation,
					                                      Quaternion.LookRotation(tempVec - transform.position), rotationSpeed * Time.deltaTime);
					                                      //Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
				}
			}
        }

		void AcquireTarget() { 
			if( players.Length > 0 ) { 
				target = players[0].transform;
				float closestDistance = Vector3.Distance(transform.position, target.position);
				for( int i = 1; i < players.Length; i++ ) {
					float distance = Vector3.Distance(transform.position, players[i].transform.position);
					if( distance < closestDistance ) {
						closestDistance = distance;
						target = players[i].transform;
					}
				}
			}
		}
    }
}