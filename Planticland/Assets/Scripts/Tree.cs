using UnityEngine;
using System.Collections;

//The base class for all trees in the project
public abstract class Tree : MonoBehaviour
{
    public int type; //Type of the tree in the [1-7] interval
    public float growthRate; //Indicates how fast a tree grows
    public float maximumScale; //Maximum amount of scale a tree can reach
    public string color; //Color of the tree
    private bool isFullyGrown; //Shows if a tree is reached its maximumScale
    public int lifeTime; //The time (in seconds) of a tree between adulthood and death
    public int deadTime; //The time (in seconds) of a tree between death and destruction
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
                this.grow();
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
        treeRigidbody.AddForce(Vector3.right * 5, ForceMode.Impulse);

        //And then wait until destruction
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void grow() //Ages the tree by its growth rate
    {
        //Makes the tree's scale bigger on all axes
        transform.localScale += new Vector3(this.growthRate, this.growthRate, this.growthRate);
    }

}
