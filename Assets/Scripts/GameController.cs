using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour 
{
	int dealersFirstCard = -1;

	public CardStack player;
	public CardStack dealer;
	public CardStack deck;
	public Button hitButton;
	public Button stickButton;
	public Button playAgainButton;
	public Text winnerText;

#region Public methods

public void Hit()
{
	player.Push(deck.Pop());
	if (player.HandValue() >21)
	{
		hitButton.interactable = false;
		stickButton.interactable = false;
		StartCoroutine(DealersTurn());
	}
}

public void Stick()
{
			hitButton.interactable = false;
		stickButton.interactable = false;

StartCoroutine(DealersTurn()); 
}

public void PlayAgain()
{
	playAgainButton.interactable = false;

	player.GetComponent<CardStackView>().Clear();
	dealer.GetComponent<CardStackView>().Clear();
	deck.GetComponent<CardStackView>().Clear();
	deck.CreateDeck();

	hitButton.interactable = true;
	stickButton.interactable = true;
	dealersFirstCard = -1;

	StartGame();

}

#endregion

#region Unity messages
	
	void Start()
	{
		StartGame();
	}

#endregion

	void StartGame()
	{
		for (int i = 0; i < 2; i++)
		{
			player.Push(deck.Pop());
			HitDealer();
		}
	winnerText.text = "";
	}

	void HitDealer()
	{
		int card = deck.Pop();

		if (dealersFirstCard <0)
		{
			dealersFirstCard = card;
		}
		   
		dealer.Push(card);
		if (dealer.CardCount >=2)
		{
			CardStackView view = dealer.GetComponent<CardStackView>();
			view.Toggle(card, true);
		}
	}
	IEnumerator DealersTurn()
	{
			CardStackView view = dealer.GetComponent<CardStackView>();
			view.Toggle(dealersFirstCard, true);
			view.ShowCards();
			yield return new WaitForSeconds(1f);

			while (dealer.HandValue() <17)
			{
				HitDealer();
				yield return new WaitForSeconds(1f);
			}
	
	if (player.HandValue() >21 || (dealer.HandValue() > player.HandValue() && dealer.HandValue() <= 21))
	{
		winnerText.text = "Sorry! You lose!";
	}
	else if (dealer.HandValue() >21 || (player.HandValue() <=21 && player.HandValue() > dealer.HandValue()))
	{
		winnerText.text = "You win!";
	}
	else
	{
		winnerText.text = "The house wins!";
	}

	yield return new WaitForSeconds(1f);
	playAgainButton.interactable = true;

	}

}
