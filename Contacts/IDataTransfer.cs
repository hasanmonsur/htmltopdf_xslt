namespace WebApp03.Contacts
{
    public interface IDataTransfer
    {
        public string TransformXml(string xmlPath, string xsltPath);

        public string TransformXmlToHtml(string xmlPath, string xsltPath);
    }
}