using UnityEngine;
using System.Text.RegularExpressions;
using Unity.Mathematics;

//I give credit to Com-3 Interactive's youtube video https://www.youtube.com/watch?v=-6ww4MMi7C0&ab_channel=Comp-3Interactive
//for the below lines of codes, and special thanks to my friend Ivory for sending me this link
public class TextToMap : MonoBehaviour
{
    
    public TextMapping[] mappingData;//Used to add different tiles
    public TextAsset mapText;//Importing external text files to convert into maps.
    private Vector2 currentPosition = new Vector2(0, 0);//Position to generate the next tile
    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        string[] rows = Regex.Split(mapText.text, "\r\n|\r|\n");//Detect and split every character on your text file.
        
        //Lines
        foreach (string row in rows)
        {   //Each characters
            foreach (char c in row)
            {   //Check if the character is in the list I created
                foreach (TextMapping tm in mappingData)
                {
                    if (c == tm.character)
                    {
                        Instantiate(tm.prefab, currentPosition, quaternion.identity, transform);
                    }
                }
                //Move to the next in the same line
                currentPosition = new Vector2(++currentPosition.x, currentPosition.y);
            }
            //Go to the next line
            currentPosition = new Vector2(0, --currentPosition.y);
        }
        
    }
}
