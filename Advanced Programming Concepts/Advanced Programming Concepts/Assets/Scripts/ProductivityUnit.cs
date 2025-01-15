using UnityEngine;

public class ProductivityUnit: Unit
{
    ResourcePile currentPile = null;
    public float productivityMultiplier = 2;
         
    protected override void BuildingInRange()
    {
         if(currentPile == null)
         {
             // TRYING TO FIND A RESOURCE PILE
             // m_Target is a protected Building, the parent class of the ResourcePile class
             // casting a ResourcePile on m_Target to check if that object is a resource pile or not. If it is a ResourcePile, it will now be pile.
             ResourcePile pile = m_Target as ResourcePile;
             
             // IF WE DO FIND A PLE, WE ARE MULTIPLYING THE PRODUCTION SPEED
             if(pile != null)
             {
                 // settng the current pile to the one we found above
                 currentPile = pile;
                 // adding multiplier to the production speed
                 currentPile.ProductionSpeed *= productivityMultiplier;
             }
         }
    }
    
    // resets productivity speed when the unit leaves the resource pile 
    void ResetProductivity()
    {
        if(currentPile != null)
        {
            currentPile.ProductionSpeed *= productivityMultiplier;
            currentPile = null;
        }
    }

    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target);   
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
    