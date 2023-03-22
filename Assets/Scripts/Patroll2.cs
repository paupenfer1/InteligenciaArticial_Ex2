using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace Panda.Examples.Shooter
{
    public class Patroll2 : MonoBehaviour
    {
        
        [SerializeField]
        private NavMeshAgent agent;

        public float range =10f;
        [SerializeField]
        Transform playerTransform;

        Unit self;
        Vector3 destination = Vector3.zero;

        // Use this for initialization
        void Start()
        {
            self = GetComponent<Unit>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        [Task]
        public bool CanSee(){
            
            RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit)){
                if(hit.collider.gameObject.CompareTag("Player")){
                    return true;
                }

            }

            return false;
        }

        

        [Task]
        bool SetDestination_Random()
        {
            Vector3 randomDirection = Random.insideUnitSphere * range;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, range, 1);
            destination = hit.position;
            return true;
        }

        [Task]
        bool SetDestination_Player()
        {
            destination = playerTransform.position;
            return true;
        }

        [Task]
        bool MoveToDestination()
        {
            agent.SetDestination(destination);
            return true;
        }

        
    }
}