
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;


public class TileMapLoader
{

    // {"Name":"Apple","Color":"Red","Price":3.99,"Sizes":["Small","Medium","Large"]}
    //"{\"Name\":\"Apple\",\"Color\":\"Red\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}"


    //"{\"Name\":\"Apple\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}"
    //{"Name":"Apple","Price":3.99,"Sizes":["Small","Medium","Large"]}
    class TiledMapData
    {
        public string Name;
      // public string Color;
        //        public DateTime ExpiryDate { get; internal set; }
        public decimal Price;
        public string[] Sizes;
    }



    public void LoadTileMap()
    {
        TiledMapData Map = new TiledMapData(); 
        //maptest.json
        Map.Name = "Apple";
       // Map.Color = "Red";
        //       Map.ExpiryDate = new DateTime(2008, 12, 28);
        Map.Price = 3.99M;
        Map.Sizes = new string[] { "Small", "Medium", "Large" };
     
        string output = JsonConvert.SerializeObject(Map);

        
        Console.WriteLine(output);

        TiledMapData deserializedMap = JsonConvert.DeserializeObject<TiledMapData>(output);

        Console.WriteLine("Salut");
    }
}

