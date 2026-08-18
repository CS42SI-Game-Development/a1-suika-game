using UnityEngine;

//////////////////////////////////////////////////////////////////////////
// TODO: This file works for merging logos, but it's not finished!      //
// Write code in this file to make the logos increase in scale          //
// depending on their stage. Stage 2 logos are larger than Stage 1      //
// logos, and Stage 3 are the largest. Read the file to find a good     //
// place to put your code.                                              //
//////////////////////////////////////////////////////////////////////////

// Put this script on the logo object. When two logos showing the same sprite
// touch each other, both disappear and one bigger logo takes their place.
public class MergeHandler : MonoBehaviour
{

    public Sprite LevelOneSprite;    // The starting logo (Berkeley).
    public Sprite LevelTwoSprite;    // What two Level One logos turn into (MIT).
    public Sprite LevelThreeSprite;  // What two Level Two logos turn into (Stanford).

    private bool hasMerged = false;  // True once this logo has been used up in a merge.

    // Runs the moment this logo is created, before any other code gets a turn.
    private void Awake()
    {
        // Every logo begins at the first level.
        SetSprite(LevelOneSprite);
    }
    
    // When this object starts touching another 2D object, this function is called.
    // (Both objects must have a collider, and one must have a Rigidbody2D.)
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherObject = other.gameObject;

        // Only logos have a MergeHandler, so this ignores walls and the floor.
        MergeHandler otherLogo = otherObject.GetComponent<MergeHandler>();
        if (otherLogo == null)
        {
            return;
        }

        // Both logos in a collision run this function, and a logo can touch several
        // logos in the same frame. Skipping used-up logos keeps one merge from
        // accidentally creating two new logos.
        if (hasMerged || otherLogo.hasMerged)
        {
            return;
        }

        // Logos can only merge if they are showing the same sprite.
        if (GetSprite() != otherLogo.GetSprite())
        {
            return;
        }

        // The biggest logo has nothing to upgrade into, so let the two logos bounce.
        Sprite upgradedSprite = GetUpgradedSprite();
        if (upgradedSprite == null)
        {
            return;
        }

        // Put the new logo halfway between the two old ones, so it shows up
        // right where they touched.
        Vector3 mergePosition = (transform.position + otherObject.transform.position) / 2;

        // Instantiate the upgraded version of the logo, and destroy the two copies
        // that made it. The new logo starts at level one (see Awake above), so we
        // hand it the bigger sprite right away.
        GameObject upgradedLogo = Instantiate(gameObject, mergePosition, Quaternion.identity);
        upgradedLogo.GetComponent<MergeHandler>().SetSprite(upgradedSprite);
        Destroy(gameObject);
        Destroy(otherObject);

        // Both old logos are on their way out, so mark them as used up. Unity waits
        // until the end of the frame to actually delete them, and this stops them
        // from merging again in the meantime.
        hasMerged = true;
        otherLogo.hasMerged = true;
    }

    // Returns the sprite this logo is currently showing.
    // The sprite lives on the child object ("Circle"), not on the logo itself.
    private Sprite GetSprite()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite;
    }

    // Changes the picture this logo is showing.
    private void SetSprite(Sprite newSprite)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
    }

    // Returns the next sprite up from the one this logo is showing,
    // or null if this logo is already as big as it gets.
    private Sprite GetUpgradedSprite()
    {
        Sprite currentSprite = GetSprite();
        
        if (currentSprite == LevelOneSprite)
        {
            return LevelTwoSprite;
        }

        if (currentSprite == LevelTwoSprite)
        {
            return LevelThreeSprite;
        }

        return null;  // Level Three is the biggest logo, so it cannot upgrade.
    }

}
