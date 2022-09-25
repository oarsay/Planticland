public class Tree1 : Tree
{
    public Tree1()//Constructor
    {
        //Initialize the tree type-spesific attributes
        type = 1;
        color = "Black";
        growthRate = 0.005f; //0.002
        maximumScale = 1.5f;
        lifeTime = 2;
        deadTime = 5;
    }
}
