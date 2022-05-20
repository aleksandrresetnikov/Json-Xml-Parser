using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Newtonsoft.Json;

namespace JsonToXmlParser
{
    public partial class Form1 : Form
    {
        private bool Converting_ActiveStatys = true;
        private bool Json_RTB_TextChanged_ActiveStatys = true;
        private bool Xml_RTB_TextChanged_ActiveStatys = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Json_RTB_TextChanged(object sender, EventArgs e)
        {
            if (!Json_RTB_TextChanged_ActiveStatys | !Converting_ActiveStatys) return;

            try
            {
                Xml_RTB_TextChanged_ActiveStatys = false;
                Xml_RTB.Text = GetXml(Json_RTB.Text);
                Xml_RTB_TextChanged_ActiveStatys = true;
            }
            catch (Exception ex) { }
        }

        private void Xml_RTB_TextChanged(object sender, EventArgs e)
        {
            if (!Xml_RTB_TextChanged_ActiveStatys | !Converting_ActiveStatys) return;

            try
            {
                Json_RTB_TextChanged_ActiveStatys = false;
                Json_RTB.Text = GetJson(Xml_RTB.Text);
                Json_RTB_TextChanged_ActiveStatys = true;
            }
            catch (Exception ex) { }
        }

        private string GetXml(string JsonText)
        {
            // To convert JSON text contained in string json into an XML node
            XmlDocument doc = JsonConvert.DeserializeXmlNode(JsonText);
            if (doc == null) throw new Exception();
            return doc.OuterXml;
        }

        private string GetJson(string XmlText)
        {
            // To convert an XML node contained in string xml into a JSON string   
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlText);
            if (doc == null) throw new Exception();
            return JsonConvert.SerializeXmlNode(doc);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Json_RTB.WordWrap = checkBox1.Checked;
            Xml_RTB.WordWrap = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Converting_ActiveStatys = !checkBox2.Checked;
        }
    }
}
