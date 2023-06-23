using lln.ChuDaDi_MainLogic.cardLogic;
using lln.ChuDaDi_MainLogic.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsToShow : MonoBehaviour
{
    private List<Card> Cards = new List<Card>();
    private int size;
    private string cardGroup;
    private int cardValue;

    public string GetCardGroup
    {
        //set { cardGroup = value; }
        get { return cardGroup; }
    }

    public CardGroup Convert(string ip)
    {
        CardGroup cardG = new CardGroup(size, cardGroup, ip );
        foreach (Card c in Cards)
        {
            cardG.cards.Add(c);
        }
        return cardG;
    }

    public void clearCards(){
        Cards.Clear();
    }
    public void HandleSelect(GameObject obj)
    {
        ViewCard vcard = obj.GetComponent<ViewCard>();
        if (vcard.onSelect == false)
        {
            OnSelect(vcard);
            vcard.onSelect = true;
            return;
        }
        DeSelect(vcard);
        vcard.onSelect = false;
        return;
    }

    private void OnSelect(ViewCard vcard)
    {
        Card card = new Card((int)vcard.GetCardType, vcard.GetCardValue);
        Cards.Add(card);
        Cards.Sort();
        size = Cards.Count;
        GroupType();
        vcard.GetTargetPos += new Vector3(0, 0.5f, 0);
        Debug.Log(card.point + " is onselect.");
    }

    private void DeSelect(ViewCard vcard)
    {
        Card card = new Card((int)vcard.GetCardType, vcard.GetCardValue);
        Cards.Remove(card);
        Cards.Sort();
        size = Cards.Count;
        GroupType();
        vcard.GetTargetPos -= new Vector3(0, 0.5f, 0);
        Debug.Log(card.point + " is deselect.");
    }

    private void GroupType()
    {
        if(size == 1)//����
        {
            cardGroup = "SINGLE";
            return;
        }
        else if(size == 2)//��
        {
            int val = Cards[0].point;
            foreach(Card c in Cards)
            {
                if (c.point != val)
                {
                    cardGroup = null;
                    return;
                }
            }
            cardValue = val;
            cardGroup = "PAIR";
            return;
        }
        else if(size == 3)//����
        {
            int val = Cards[0].point;
            foreach (Card c in Cards)
            {
                if (c.point != val)
                {
                    cardGroup = null;
                    return;
                }
            }
            cardValue = val;
            cardGroup = "THREE";
            return;
        }
        else if(size == 4)
        {
            int val = Cards[0].point;
            foreach (Card c in Cards)
            {
                if (c.point != val)
                {
                    cardGroup = null;
                    return;
                }
            }
            cardValue = val;
            cardGroup = "FOUR";
            return;
        }
        else if(size == 5)//���Ű�������ͬ����˳��ͬ���塢����һ�ԡ��Ĵ�����
        {
            //ͬ����˳�ں��жϣ����ж������������"A2345","23456","10JQKA"
            if(Cards[0].point == 2 && Cards[1].point == 1 && Cards[2].point == 5 && Cards[3].point == 4 && Cards[4].point == 3)
            {
                cardValue = 5;
                if (IsFlush())
                {
                    cardGroup = "TONG_HUA_SHUN";
                    return;
                }
                cardGroup = "SHUN_ZI";
                return;
            }
            else if(Cards[0].point == 2 && Cards[1].point == 6 && Cards[2].point == 5 && Cards[3].point == 4 && Cards[4].point == 3)
            {
                cardValue = 6;
                if (IsFlush())
                {
                    cardGroup = "TONG_HUA_SHUN";
                    return;
                }
                cardGroup = "SHUN_ZI";
                return;
            }
            else if(Cards[0].point == 1 && Cards[1].point == 13 && Cards[2].point == 12 && Cards[3].point == 11 && Cards[4].point == 10)
            {
                cardValue = 1;
                if (IsFlush())
                {
                    cardGroup = "TONG_HUA_SHUN";
                    return;
                }
                cardGroup = "SHUN_ZI";
                return;
            }

            //���������˳�����жϳ���˳
            if (IsStraight())
            {
                cardValue = Cards[0].point;
                if (IsFlush())
                {
                    cardGroup = "TONG_HUA_SHUN";
                    return;
                }
                cardGroup = "SHUN_ZI";
                return;
            }

            //��˳�ж��Ƿ�ͬ����
            if (IsFlush())
            {
                cardValue = Cards[0].point;
                cardGroup = "TONG_HUA";
                return;
            }

            //��ͬ����˳���ж������Ժ��Ĵ�һ
            if (IsThreeWithPair())
            {
                cardGroup = "3WITH2";
                return;
            }
            if (IsFourWithSingle())
            {
                cardGroup = "4WITH1";
                return;
            }
        }
    }

    //�ж��Ƿ�Ϊͬ��
    private bool IsFlush()
    {
        int firstSuit = Cards[0].suit;

        for(int i = 1; i < Cards.Count; i++)
        {
            if(Cards[i].suit != firstSuit)
            {
                return false;
            }
        }
        return true;
    }

    //�ж��Ƿ�Ϊ����˳
    private bool IsStraight()
    {
        for(int i = 0; i < Cards.Count - 1; i++)
        {
            if(Cards[i].point - Cards[i+1].point != 1)
            {
                return false;
            }
        }
        return true;
    }

    //�ж��Ƿ�Ϊ����һ��
    private bool IsThreeWithPair()
    {
        //��Ϊǰ�������ǰ�����������
        if(Cards[0].point == Cards[1].point && Cards[1].point == Cards[2].point && Cards[3].point == Cards[4].point)
        {
            cardValue = Cards[0].point;
            return true;
        }
        else if(Cards[0].point == Cards[1].point && Cards[2].point == Cards[3].point && Cards[3].point == Cards[4].point)
        {
            cardValue = Cards[4].point;
            return true;
        }
        return false;
    }

    //�ж��Ƿ�Ϊ�Ĵ�һ
    private bool IsFourWithSingle()
    {
        //��Ϊǰ�ĺ�һ��ǰһ���ĵ����
        if (Cards[0].point == Cards[1].point && Cards[1].point == Cards[2].point && Cards[2].point == Cards[3].point)
        {
            cardValue = Cards[0].point;
            return true;
        }
        else if(Cards[1].point == Cards[2].point && Cards[2].point == Cards[3].point && Cards[3].point == Cards[4].point)
        {
            cardValue = Cards[4].point;
            return true;
        }
        return false;
    }

}
