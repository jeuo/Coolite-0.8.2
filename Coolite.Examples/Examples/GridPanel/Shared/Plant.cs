using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Xml;

namespace Coolite.Examples.Examples.GridPanel.Shared
{
    public class Plant
    {
        public Plant(bool indoor, DateTime availability, decimal price, string light, string zone, string botanical, string common)
        {
            this.Indoor = indoor;
            this.Availability = availability;
            this.Price = price;
            this.Light = light;
            this.Zone = zone;
            this.Botanical = botanical;
            this.Common = common;
        }

        public Plant()
        {
        }

        public string Common { get; set; }

        public string Botanical { get; set; }

        public string Zone { get; set; }

        public string ColorCode { get; set; }

        public string Light { get; set; }

        public decimal Price { get; set; }

        public DateTime Availability { get; set; }

        public bool Indoor { get; set; }

        public static List<Plant> TestData
        {
            get
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HttpContext.Current.Server.MapPath("Plants.xml"));
                List<Plant> data = new List<Plant>();
                IFormatProvider culture = new CultureInfo("en-US", true);

                foreach (XmlNode plantNode in xmlDoc.SelectNodes("catalog/plant"))
                {
                    Plant plant = new Plant();

                    plant.Common = plantNode.SelectSingleNode("common").InnerText;
                    plant.Botanical = plantNode.SelectSingleNode("botanical").InnerText;
                    plant.Zone = plantNode.SelectSingleNode("zone").InnerText;
                    plant.ColorCode = plantNode.SelectSingleNode("colorCode").InnerText;
                    plant.Light = plantNode.SelectSingleNode("light").InnerText;
                    plant.Price = decimal.Parse(plantNode.SelectSingleNode("price").InnerText, culture);
                    plant.Availability = DateTime.Parse(plantNode.SelectSingleNode("availability").InnerText, culture);
                    plant.Indoor = bool.Parse(plantNode.SelectSingleNode("indoor").InnerText);

                    data.Add(plant);
                }

                return data;
            }
        }
    }
}
