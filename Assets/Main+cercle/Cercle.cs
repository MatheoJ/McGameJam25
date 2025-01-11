using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.Windows.WebCam.VideoCapture;

public class Cercle : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 destination;
    //public GameObject map;
    private float mapSize;

    public Transform player;

    public float moyenneDistanceNormale=3;//(distance moyenne au joueur)
    public float ecartTypeDistanceNormale=2;
    public float ecartTypeNormale=2;//respect de la ditance moyenne

    public bool deplacementAvance = false;

    public GameObject main;

    public bool recherche=true;


    public Transform baitPoint;

    private AudioSource audioMain;
    [Range(0.0f, 5.0f)]
    public float audioStart = 0;

    public Image cercleRouge;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("start cercle");
        agent = GetComponent<NavMeshAgent>();
        //mapSize = map.transform.localScale.x/2* map.transform.localScale.x-2;
        audioMain = main.GetComponent<AudioSource>();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(destination, transform.position) < 0.5f && recherche)
        {
            if(deplacementAvance) MoveToGaussianPoint(player, agent, 0, ecartTypeNormale);
            else destination = new Vector3(Random.Range(-mapSize,mapSize), transform.position.y, Random.Range(-mapSize,mapSize));

            //Debug.Log(destination);
        }
        agent.SetDestination(destination);
        //Debug.Log("dest updated");
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && recherche)
        {
            recherche = false;
            agent.isStopped = true;
            
            StartCoroutine(main.GetComponent<Main>().Appuyer());
            StartCoroutine(CercleRouge());

            audioMain.Stop();
            audioMain.time = audioStart;
            audioMain.Play();
        }
    }

    IEnumerator CercleRouge()
    {
        while (5.5f-cercleRouge.transform.localScale.x > 0.1)
        {
            cercleRouge.transform.localScale = Mathf.Lerp(cercleRouge.transform.localScale.x, 5.5f, Time.deltaTime * (main.GetComponent<Main>().vitesse*0.9f)) * Vector3.one;
            yield return null;
        }
        while (cercleRouge.transform.localScale.x > 0.1)
        {
            cercleRouge.transform.localScale = Mathf.Lerp(cercleRouge.transform.localScale.x, 0f, Time.deltaTime * (main.GetComponent<Main>().vitesse * 1.1f)) * Vector3.one;
            yield return null;
        }
        cercleRouge.transform.localScale = Vector3.zero;
    }

    public void StunHand()
    {
        Debug.Log("starting coroutine");
        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        //agent.SetDestination(transform.position);
        //agent.Stop();
        agent.isStopped = true;
        recherche = false;
        
        yield return new WaitForSeconds(2f);
        recherche = true;
        agent.isStopped = false;
    }

    public void baitHand()
    {
        Debug.Log("baiting Hand");
        // agent.isStopped = true;
        destination =baitPoint.position;
        //agent.isStopped = false;
        //agent.ResetPath();
    }

}
