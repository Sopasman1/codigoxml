using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace codigoxml
{
    public partial class Form1 : Form
    {
        private string URLString;
        private XmlTextReader reader;
        private HashSet<string> uniqueHeaders;

        public Form1()
        {
            InitializeComponent();
            URLString = "https://www.w3schools.com/xml/cd_catalog.xml";
            reader = new XmlTextReader(URLString);
            uniqueHeaders = new HashSet<string>();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && !uniqueHeaders.Contains(reader.LocalName))
                {
                    listViewxml.Columns.Add(reader.LocalName, reader.LocalName, -2);
                    uniqueHeaders.Add(reader.LocalName);
                }
            }

            reader.Close();

            reader = new XmlTextReader(URLString);

            List<string> cellValues = new List<string>();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        cellValues.Add(reader.Value);
                        break;

                    case XmlNodeType.EndElement:
                        if (cellValues.Count > 0)
                        {
                            var item = new ListViewItem(cellValues.ToArray());
                            listViewxml.Items.Add(item);
                            cellValues.Clear();
                        }
                        break;
                }
            }

            reader.Close();
        }
    }
}

