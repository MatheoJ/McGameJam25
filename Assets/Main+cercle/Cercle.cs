using UnityEngine;
using UnityEngine.AI;

public class Cercle : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    public GameObject map;
    private float mapSize;

    public Transform player;

    public float moyenneDistanceNormale=3;//(distance moyenne au joueur)
    public float ecartTypeDistanceNormale=2;
    public float ecartTypeNormale=2;//respect de la ditance moyenne

    public bool deplacementAvance = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        mapSize = map.transform.localScale.x/2* map.transform.localScale.x-2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(destination, transform.position) < 0.5f)
        {
            if(deplacementAvance) MoveToGaussianPoint(player, agent, 0, ecartTypeNormale);
            else destination = new Vector3(Random.Range(-mapSize,mapSize), transform.position.y, Random.Range(-mapSize,mapSize));

            Debug.Log(destination);
        }
        agent.SetDestination(destination);
    }



    float RandomNormal(float mean, float stdDev)
    {
        float u1 = Random.Range(0f, 1f); // Premier nombre aléatoire
        float u2 = Random.Range(0f, 1f); // Deuxième nombre aléatoire

        // Transformation de Box-Muller
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);

        // Appliquer la moyenne et l'écart type
        return mean + stdDev * randStdNormal;
    }

    Vector3 GetRandomPointAroundPlayer(Vector3 playerPosition, float meanDistance, float stdDevDistance)
    {
        /*// Générer des offsets selon une loi normale
        float offsetX = RandomNormal(0f, stdDevDistance);
        float offsetZ = RandomNormal(0f, stdDevDistance);*/

        float angle = Random.Range(0f,2*Mathf.PI);
        float offset2 = RandomNormal(moyenneDistanceNormale, ecartTypeDistanceNormale);

        return new Vector3(playerPosition.x + Mathf.Cos(angle)*offset2, playerPosition.y, playerPosition.z+ Mathf.Sin(angle)*offset2);
        /*
        // Ajouter la moyenne (distance centrale)
        Vector3 offset = new Vector3(offsetX + meanDistance, 0, offsetZ + meanDistance);

        // Calculer la position finale
        return playerPosition + offset;*/
    }

    void MoveToGaussianPoint(Transform player, NavMeshAgent agent, float meanDistance, float stdDevDistance)
    {
        Vector3 randomPoint = GetRandomPointAroundPlayer(player.position, meanDistance, stdDevDistance);

        // Vérifier si le point est valide sur le NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, meanDistance + stdDevDistance, NavMesh.AllAreas))
        {
            destination = hit.position;
            //agent.SetDestination(hit.position);
        }
    }
}
