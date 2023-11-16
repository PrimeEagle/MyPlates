using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using MyPlates.Tx.Data;
using TxDot.Web.Services;

namespace MyPlates.Tx.Web
{
    public partial class UIPopulate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool flushcache = false;

            if (Request.QueryString["flushcache"] != null)
            {
                flushcache = true;
            }

            XmlDocument doc = new XmlDocument();
            string xml = (string)Cache["UIPopulateXml"];

            if (xml != null && !flushcache)
            {
                doc.LoadXml(xml);
            }
            else
            {
                DataSet ds = new DataSet();

                XslCompiledTransform xslt = new XslCompiledTransform();

                ds = MyPlatesData.GetFlashXMLInfo();
                doc.LoadXml(ds.GetXml().Replace("&", "&amp;"));
                xslt.Load(Server.MapPath(@"\xslt\PopulateFlash.xslt"));

                MemoryStream ms = new MemoryStream();
                xslt.Transform(doc, new XsltArgumentList(), ms);

                ms.Seek(0, SeekOrigin.Begin);
                doc.Load(ms);

                XmlElement root = doc.DocumentElement;
                XmlElement countiesElem = doc.CreateElement("COUNTIES");
                root.AppendChild(countiesElem);

                List<string> counties = TxDotWebServices.GetCountyInfoList(Session.SessionID);

                foreach (string county in counties)
                {
                    XmlElement countyElem = doc.CreateElement("COUNTY");
                    countyElem.InnerText = county;
                    countiesElem.AppendChild(countyElem);
                }

                Cache.Insert("UIPopulateXml", doc.OuterXml, null, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            Response.CacheControl = "No-cache";
            Response.ContentType = "application/xml";
            Response.Write(doc.OuterXml);

        }
    }
}
