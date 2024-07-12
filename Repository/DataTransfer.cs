using DinkToPdf;
using DinkToPdf.Contracts;
using System.Xml.Xsl;
using System.Xml;
using WebApp03.Contacts;
using System;

namespace WebApp03.Repository
{
    public class DataTransfer: IDataTransfer
    {
        private readonly IConverter _converter;

        public DataTransfer(IConverter converter)
        {
            _converter = converter;
        }
        public string TransformXml(string xmlPath, string xsltPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (StringWriter sw = new StringWriter())
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                {
                    xslt.Transform(xmlReader, null, sw);
                }
                return sw.ToString();
            }
        }

        public string TransformXmlToHtml(string xmlPath, string xsltPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (StringWriter sw = new StringWriter())
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                {
                    xslt.Transform(xmlReader, null, sw);
                }
                return sw.ToString();
            }
        }

        


    }
}
