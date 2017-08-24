using System.Collections.Generic;

public class TileMapLoader
{
    // {"Name":"Apple","Color":"Red","Price":3.99,"Sizes":["Small","Medium","Large"]}
    //"{\"Name\":\"Apple\",\"Color\":\"Red\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}"


    //"{\"Name\":\"Apple\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}"
    //{"Name":"Apple","Price":3.99,"Sizes":["Small","Medium","Large"]}
    class Product
    {
        public string Name;
        // public string Color;
        //        public DateTime ExpiryDate { get; internal set; }
        public decimal Price;
        public string[] Sizes;
    }

    public class Layer
    {
        public List<int> data { get; set; }
        public int height { get; set; }
        public string name { get; set; }
        public int opacity { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string draworder { get; set; }
        public List<object> objects { get; set; }
    }

    public class Tileset
    {
        public int firstgid { get; set; }
        public string source { get; set; }
    }

    public class TileMapData
    {
        public int height { get; set; }
        public List<Layer> layers { get; set; }
        public int nextobjectid { get; set; }
        public string orientation { get; set; }
        public string renderorder { get; set; }
        public string tiledversion { get; set; }
        public int tileheight { get; set; }
        public List<Tileset> tilesets { get; set; }
        public int tilewidth { get; set; }
        public string type { get; set; }
        public int version { get; set; }
        public int width { get; set; }

        public int getTile(int layer, int row, int col)
        {
            if (layers[layer] != null)
            {
                int index = row * width + col;
                int val = -1;
                val = layers[layer].data[index];
                return val;
            }
            else
            {
                return -2;
            }

        }
        
        public int[,] getMap()
        {
            int[,] list = new int[11, 11];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    list[i, j] = getTile(0, i, j);
                }
            }
            return list;
        }
    }

    public TileMapData LoadTileMap(string file)
    {
        return null;
    }
}