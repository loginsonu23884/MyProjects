using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.Configuration;

/// <summary>
/// Summary description for XMLService
/// </summary>
public class XMLService
{
    public string XMLFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ProjectName"].Trim()+"\\PreTradeXMLDescriptor.xml").ToString();
    
    public List<XElement> GetNodeElement(XDocument xmlDoc,string NodeName,string Selectiontype,string SelectionValue)
    {
        
        var items = xmlDoc.Descendants(NodeName)
                  .Where(item => item.Element(Selectiontype).Value == SelectionValue)
                  .ToList();
        return items;
    }

    public List<XElement> GetNodeElementMultipleCondition(XDocument xmlDoc, string NodeName, string Selectiontype, string SelectionValue, string Selectiontype2, string SelectionValue2)
    {

        var items = xmlDoc.Descendants(NodeName)
                  .Where(item => item.Element(Selectiontype).Value == SelectionValue &  item.Element(Selectiontype2).Value == SelectionValue2)
                  .ToList();
        return items;
    }
    public void UpdateNodeElement(DataTable dtSetting, List<XElement> items)
    {
        XDocument xmlDoc = XDocument.Load(XMLFilePath);
        if (dtSetting != null && dtSetting.Rows.Count > 0)
        {
            foreach (DataRow dr in dtSetting.Rows)
            {
                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue(dtSetting.Columns[itemElement.Name.ToString()].ToString(), dr[itemElement.Name.ToString()].ToString());
                }
               
            }
            xmlDoc.Save(XMLFilePath);



        }
    }
    public void CreateNode(XmlElement DocRoot, XmlDocument Doc, string ColumnName, DataTable dtColumnSetting)
    {
        XmlNode Node;
      
        if (dtColumnSetting != null && dtColumnSetting.Rows.Count > 0)
        {
            foreach (DataRow dr in dtColumnSetting.Rows)
            {
                Node = Doc.CreateElement(ColumnName);
                foreach (DataColumn dc in dtColumnSetting.Columns)
                {
                    // Create the xml element "Column"
                    Node.Attributes.Append(GenerateXMLAttribute(Doc, dr[dc].ToString(), dc.ToString()));
                }
                DocRoot.AppendChild(Node);
            }
        }
    }
    public DataSet LoadXml()
    {
        DataSet dsXML=new DataSet();
        dsXML.ReadXml(XMLFilePath);
        return dsXML;
    }
    //Function for building the XML Element Attribute
    public XmlAttribute GenerateXMLAttribute(XmlDocument objXMLDoc, string attributeValue, string attributeType)
    {
        XmlAttribute objXmlAttribute = default(XmlAttribute);
        objXmlAttribute = objXMLDoc.CreateAttribute(attributeType);
        objXmlAttribute.Value = attributeValue;
        return objXmlAttribute;
    }
}