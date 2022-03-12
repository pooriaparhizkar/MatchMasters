using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private ISession session;
    public Text Coin1;
    public Text Coin2;
    public Text Coin3;
    public Text Coin4;
    public Text Coin5;
    public Text Coin6;
    public Text Count1;
    public Text Count2;
    public Text Count3;
    public Text Count4;
    public Text Count5;
    public Text Count6;
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
    // private ISession session;
    // Start is called before the first frame update
    void Start()
    {
        getShop();
    }

    // Update is called once per frame
    void Update()
    {
        var x = 0;
        if (x != 0)
        {
            x = 0;
        }
        // getShop();
    }

    private async void getShop()
    {
        // Debug.Log("qweeeee77777eeeeeeeeeeeeeeeeeeeeeeeeee");
        var session = Session.Restore(PlayerPrefs.GetString("token"));
        string payload = "";
        string rpcid = "shop";
        Task<IApiRpc> shop;
        shop = client.RpcAsync(session, rpcid, payload);
        // Debug.Log(shop.Status);
        IApiRpc searchResult = await shop;
        //
        // Debug.Log(searchResult);
        shopStruct[] shopres = searchResult.Payload.FromJson<shopStruct[]>();
        Debug.Log(shopres[0].count);
        Coin1.text = shopres[0].count.ToString();
        Coin2.text = shopres[1].count.ToString();
        Coin3.text = shopres[2].count.ToString();
        Coin4.text = shopres[3].count.ToString();
        Coin5.text = shopres[4].count.ToString();
        Coin6.text = shopres[5].count.ToString();
        Count1.text = shopres[0].cost.ToString();
        Count2.text = shopres[1].cost.ToString();
        Count3.text = shopres[2].cost.ToString();
        Count4.text = shopres[3].cost.ToString();
        Count5.text = shopres[4].cost.ToString();
        Count6.text = shopres[5].cost.ToString();

    }

    private struct shopStruct
    {
        public int id;
        public int count;
        public int cost;
    }
}
