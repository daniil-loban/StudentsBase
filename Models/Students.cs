using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsBase.Models {

    using Catel.Data;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;


    public class Students {

        [XmlElement("Student")]
        public ObservableCollection<StudentModel> students { get; set; }

        public Students() {
            students = new ObservableCollection<StudentModel>();
        }

        public void ReadFromFile(string filename) {
            using (FileStream filestream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None)) {
                var serializer = new XmlSerializer(typeof(Students));
                this.students = ((Students)serializer.Deserialize(filestream)).students;
            }
        }

        public void WriteToFile(string filename) {

            BuitifyXMLSave(PrepareXML(CleanNamespase), filename);

        }

        private void BuitifyXMLSave(string rawXML, string filename) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rawXML);
            using (XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
            }
        }

        private string PrepareXML(XmlSerializerNamespaces ns) {
            using (MemoryStream ms = new MemoryStream()) {
                var serializer = new XmlSerializer(typeof(Students));
                serializer.Serialize(ms, this, ns);
                ms.Position = 0;
                var sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }

        private XmlSerializerNamespaces CleanNamespase {
            get {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                return ns;
            }
        }
    }

}
