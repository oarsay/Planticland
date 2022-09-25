using UnityEngine;
using System.Collections;

//The base class for all trees in the project
public abstract class Tree : MonoBehaviour
// ABSTRACTION
{

    // ENCAPSULATION
    protected int type; //Type of the tree in the [1-7] interval
    protected float growthRate; //Indicates how fast a tree grows
    protected float maximumScale; //Maximum amount of scale a tree can reach
    protected string color; //Color of the tree
    private bool isFullyGrown; //Shows if a tree is reached its maximumScale
    protected int lifeTime; //The time (in seconds) of a tree between adulthood and death
    protected int deadTime; //The time (in seconds) of a tree between death and destruction
    private bool isAlive;
    private Rigidbody treeRigidbody;

    public Tree() //Constructor
    {
        //Initialize the common attributes of all trees
        this.isAlive = true;
        this.isFullyGrown = false;
    }

    void Start()
    {
        treeRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.TreeManager();
    }

    public void TreeManager()
    {
        if(this.isAlive)
        {
            if(!this.isFullyGrown)
            {
                //The tree is alive and still growing
                this.Grow();
                if(transform.localScale.x >= this.maximumScale) isFullyGrown = true;
            }
            else
            {   //The tree is fully grown, start life span countdown
                StartCoroutine(Adulthood(this.lifeTime));
            }
        }
        else
        {
            StartCoroutine(Death(this.deadTime));
        }
    }

    IEnumerator Adulthood(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.isAlive = false;
    }
    IEnumerator Death(int seconds)
    {
        //The tree is dead now, add some force to the rigidbody to make it fall on the ground
        treeRigidbody.useGravity = true;
        treeRigidbody.constraints = RigidbodyConstraints.None;
        treeRigidbody.AddForce(GetRandomForceVector() * 7, ForceMode.Impulse);

        //And then wait until destruction
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void Grow() //Ages the tree by its growth rate
    {
        //Makes the tree's scale bigger on all axes
        transform.localScale += new Vector3(this.growthRate, this.growthRate, this.growthRate);
    }

    private Vector3 GetRandomForceVector()
    {
        float y = 0;
        float x, z, force;

        force = Random.Range(0.3f, 1f); // Random magnitude
        force *= ((Random.Range(0,2) == 0)? 1: -1); // Random direction
        x = force;

        force = Random.Range(0.3f, 1f); // Random magnitude
        force *= ((Random.Range(0,2) == 0)? 1: -1); // Random direction
        z = force;

        return new Vector3(x, y, z);
    }
}
