using UnityEngine;
using System.Collections;

public class DebugDealer : MonoBehaviour {

public CardStack dealer;
public CardStack player;

int count = 0;
int[] cards = new int[] { 8, 0, 13};

// void OnGUI()
// {
// 	if (GUI.Button(new Rect(10,10,256,28), "Hit me!"))
// 	{
// 		player.Push(dealer.Pop());
// 	}

// }

void OnGUI()
{
	if (GUI.Button(new Rect(10,10,256,28), "Hit me!"))
	{
		player.Push(cards[count++]);
	}

}

}
